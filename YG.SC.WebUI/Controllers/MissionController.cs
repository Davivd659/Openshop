using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Service;
using YG.SC.Service.IService;

namespace YG.SC.WebUI.Controllers
{
	public class MissionController : WebBaseController
	{
		private readonly IMissionService _IMissionService;

		private readonly IFileService _IFileService;

		private readonly ICustomerService _ICustomerService;

		private readonly IOpenShopService _IOpenShopService;


		public MissionController(IMissionService IMissionService, IFileService IFileService, ICustomerService ICustomerService, IOpenShopService IOpenShopService)
		{
			_IMissionService = IMissionService;
			_IFileService = IFileService;
			_ICustomerService = ICustomerService;
			_IOpenShopService = IOpenShopService;
		}
		[HttpGet]
		public ActionResult Edit(int Mid = 0)
		{
			var model = this._IMissionService.GetById(Mid);
			Dictionary<int, string> attrsTypes = this._IMissionService.GetMissionTypeList();
			var typelist = (from m in attrsTypes
							select new SelectListItem
							{
								Text = m.Value,
								Value = m.Key.ToString()
							}).ToList();
			ViewBag.MissionType = typelist;
			return View(model);
		}
		[HttpPost]
		[ActionName("Edit")]
		public ActionResult Edit(M_Mission Mission)
		{
			var model = this._IMissionService.GetById(Mission.Id);
			model.TotalPrice = Mission.TotalPrice;
			model.Rate = Mission.Rate;
			model.Description = Mission.Description;

			this._IMissionService.Edit(model);
			return RedirectToAction("/List");
		}
		public ActionResult List(int pg = 1, string Title = "", int MissionType = 0)
		{
			var Status = this._IMissionService.GetMissionStatusList();
			var Statuslist = (from m in Status
							  select new SelectListItem
							  {
								  Text = m.Value,
								  Value = m.Key.ToString()
							  }).ToList();
			ViewBag.MissionStatus = Statuslist;
			var attrsTypes = this._IMissionService.GetMissionTypeList();
			var typelist = (from m in attrsTypes
							select new SelectListItem
							{
								Text = m.Value,
								Value = m.Key.ToString()
							}).ToList();
			ViewBag.MissionType = typelist;
			MissionSearchCriteria SearchCriteria = new MissionSearchCriteria();
			SearchCriteria.pg = pg;
			SearchCriteria.PageSize = Define.PAGE_SIZE;
			SearchCriteria.Keys = Title;
			SearchCriteria.MissionType = MissionType;
			SearchCriteria.Role = (CommonEnum.GroupOfCustomer)(_ICustomerService.GetEntityById(UserId)).GroupId.Value;
			SearchCriteria.UserId = UserId;
			var model = this._IMissionService.GetEntitsList(SearchCriteria);
			return View(model);
		}

		public ViewResult Item(int id)
		{
			M_Mission m = _IMissionService.GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			if (m.FkMissionType == null)
			{
				throw new Exception("未知任务类型。");
			}
			ViewBag.ContractSrc = string.Empty;
			C_File f;
			if (_IMissionService.ExistContract(m.Id, out f))
			{
				ViewBag.ContractSrc = CommonContorllers.FileUploadMissionContractImgPath + f.FileName;
			}
			ViewBag.pList = _IMissionService.GetPeriodList(m.Id);
			ViewBag.IsExistContractAndPeriod = _IMissionService.GetContractByMission(m.Id) != null && _IMissionService.GetPeriodList(m.Id).Count > 0;
			return View(m);
		}

		public PartialViewResult StepPartial(M_Mission m)
		{
			return PartialView(m);
		}
		public RedirectToRouteResult Connecting(int id)
		{
			_IMissionService.Connecting(id, _ICustomerService.GetEntityById(UserId));
			return RedirectToAction("List");
		}

		public PartialViewResult ItemPartial(M_Mission m)
		{
			return PartialView(m);
		}

		public ViewResult Decide(int id)
		{
			M_Mission m = _IMissionService.GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			if ((CommonEnum.StatusOfMission)m.Status != CommonEnum.StatusOfMission.Appeal)
			{
				throw new Exception("只能裁决已申诉任务。");
			}
			return View(m);
		}

		[HttpPost]
		public RedirectToRouteResult Decide(M_Mission m)
		{
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			M_Mission mBd = _IMissionService.GetById(m.Id);
			if (mBd == null)
			{
				throw new Exception("任务不存在。");
			}
			if ((CommonEnum.StatusOfMission)mBd.Status != CommonEnum.StatusOfMission.Appeal)
			{
				throw new Exception("只能裁决已申诉任务。");
			}
			mBd.DecideResult = m.DecideResult;
			mBd.DecidePay = m.DecidePay;
			mBd.DecideRefund = m.DecideRefund;
			mBd.Status = (int)CommonEnum.StatusOfMission.End;
			mBd.LastTime = DateTime.Now;
			_IMissionService.Edit(mBd);
			return RedirectToAction("List");
		}

		public ViewResult Close(int id)
		{
			M_Mission m = _IMissionService.GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			return View(m);
		}

		[HttpPost]
		public RedirectToRouteResult Close(M_Mission m)
		{
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			M_Mission mBd = _IMissionService.GetById(m.Id);
			if (mBd == null)
			{
				throw new Exception("任务不存在。");
			}
			mBd.CloseReason = m.CloseReason;
			mBd.Status = (int)CommonEnum.StatusOfMission.End;
			mBd.LastTime = DateTime.Now;
			_IMissionService.Edit(mBd);
			return RedirectToAction("List");
		}

		public ViewResult Contract(int missionId)
		{
			M_Mission m = _IMissionService.GetById(missionId);//获取任务。
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			M_Contract c = _IMissionService.GetContractByMission(m.Id);//获取合同。
			C_File f = null;
			if (c != null)
			{
				f = _IFileService.GetById(c.FileId);
				if (f == null)
				{
					throw new Exception("未知文件。");//合同即文件。共存。严谨判断。
				}
				ViewBag.ContractSrc = CommonContorllers.FileUploadMissionContractImgPath + f.FileName;
			}
			Tuple<OpenShop[], PagerEntity> openShopList = _IOpenShopService.SearchOpenShop(new OpenShopSearchCriteria() { TypeId = m.MissionType });
			Dictionary<int, string> dicOpenShop = null;
			if (openShopList != null && openShopList.Item1 != null)
			{
				dicOpenShop = new Dictionary<int, string>();
				foreach (OpenShop item in openShopList.Item1)
				{
					dicOpenShop.Add(item.Id, item.Name);
				}
			}
			ViewBag.OpenShopList = dicOpenShop;
			ViewBag.MissionId = m.Id;
			return View(f);
		}

		[HttpPost]
		public RedirectToRouteResult Contract(C_File model, int missionId, int openshop, string contact, string mobile)
		{
			M_Mission m = _IMissionService.GetById(missionId);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			string fileName = UploadImgUtility.UploadImage(Request.Files["FileName"], Server.MapPath(CommonContorllers.FileUploadMissionContractImgPath));//上传图片，返回文件名。
			C_File fDb;
			if (_IMissionService.ExistContract(missionId, out fDb))//确认合同存在，更新文件名。
			{
				fDb.FileName = fileName;
				_IFileService.Update(fDb);
			}
			else//合同不存在，新建合同及合同文件。
			{
				_IMissionService.CreateContract(missionId, fileName);
			}
			Customer c = _ICustomerService.GetEntitsByGroupAndCompanyId(CommonEnum.GroupOfCustomer.OpenShop, openshop);
			if (c == null)
			{
				throw new Exception("未知联系人。");
			}
			m.Receiver = c.Id;
			m.ReceiverContact = contact;
			m.ReceiverMobile = mobile;
			_IMissionService.Edit(m);
			return RedirectToAction("Item", "Mission", new { id = missionId });
		}

		public ViewResult Period(int missionId, int? id = null)
		{
			ViewBag.MissionId = missionId;//任务必须有，直接记录。
			if (!id.HasValue)//新建没有id。
			{
				ViewBag.PeriodNumber = _IMissionService.GetPeriodNumber(missionId);
				return View(new M_Period());
			}
			M_Period model = _IMissionService.GetPeriodById(id.Value);//有id必定有model。
			if (model == null)
			{
				throw new Exception("不存在的任务期。");
			}
			ViewBag.PeriodNumber = model.Period;
			return View(model);//编辑填充model值。
		}

		[HttpPost]
		public RedirectToRouteResult Period(M_Period model)
		{
			if (Request["MissionId"] == null)
			{
				throw new Exception("任务异常。");
			}
			int missionId = 0;
			try
			{
				missionId = int.Parse(Request["MissionId"]);
			}
			catch (Exception ex)
			{
			}
			if (missionId <= 0)
			{
				throw new Exception("任务编号异常。");
			}
			if (model == null)
			{
				throw new Exception("不存在的任务期。");
			}
			bool isCreate = model.Id <= 0;
			M_Period p = null;
			if (isCreate)//新建。new、设定任务、设定期号。
			{
				p = new M_Period();
				p.MissionId = missionId;
				p.Period = _IMissionService.GetPeriodNumber(missionId);
			}
			else//编辑。按编号从数据库获取实体。
			{
				p = _IMissionService.GetPeriodById(model.Id);
				if (p == null)
				{
					throw new Exception("任务期不存在。");
				}
			}
			p.Standard = model.Standard;
			p.Price = model.Price;
			if (isCreate)
			{
				_IMissionService.CreatePeriod(p);
			}
			else
			{
				_IMissionService.UpdatePeriod(p);
			}
			return RedirectToAction("Item", "Mission", new { id = model.MissionId });
		}

		public RedirectToRouteResult FinishContract(int id)
		{
			M_Mission m = _IMissionService.GetById(id);
			if (m == null)
			{
				throw new Exception("任务异常。");
			}
			_IMissionService.Contract(m.Id);
			return RedirectToAction("Item", "Mission", new { id = m.Id });
		}

		public RedirectToRouteResult DeletePeriod(int missionId, int id)
		{
			_IMissionService.DeletePeriod(id);
			return RedirectToAction("item", new { id = missionId });
		}

		public RedirectToRouteResult ConfirmPayment(int id)
		{
			_IMissionService.ConfirmPayment(id);
			return RedirectToAction("item", new { id = id });
		}
	}
}
