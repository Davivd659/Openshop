using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Service;
using YG.SC.Service.IService;

namespace YG.SC.OpenShop.Controllers
{
	public class MissionController : Controller
	{
		private IMissionService _missionService;
		private ICustomerService _customerService;
		private IFileService _fileService;

		public MissionController(IMissionService missionService, ICustomerService customerService, IFileService fileService)
		{
			_missionService = missionService;
			_customerService = customerService;
			_fileService = fileService;
		}

		[UserAuthorize]
		public ActionResult List(MissionSearchCriteria filter = null)
		{
			if (filter == null)
			{
				filter = new MissionSearchCriteria();
				filter.pg = 1;
			}
			filter.PageSize = Define.PAGE_SIZE;
			Customer c = _customerService.GetEntityById(UserContext.Current.Id);
			filter.UserId = UserContext.Current.Id;
			filter.Role = (CommonEnum.GroupOfCustomer)c.GroupId;
			ViewBag.UserGroup = (CommonEnum.GroupOfCustomer)c.GroupId;
			ViewBag.StatusList = _missionService.GetMissionStatusList();
			ViewBag.MissionList = _missionService.GetEntitsList(filter);
			return View(filter);
		}

		[UserAuthorize]
		public ActionResult Edit(int id, string boxName = "", string message = "")
		{
			var model = _missionService.GetById(id);
			if (model == null)
			{
				throw new Exception("未知任务。");
			}
			ViewBag.BoxName = boxName;
			ViewBag.Message = message;
			ViewBag.UserGroup = (CommonEnum.GroupOfCustomer)_customerService.GetEntityById(UserContext.Current.Id).GroupId;
			ViewBag.TypeOfPayment = _missionService.GetPaymentTypeList();
			if (model.Status >= (int)CommonEnum.StatusOfMission.Contract)
			{
				M_Contract c = _missionService.GetContractByMission(model.Id);
				if (c != null)
				{
					C_File f = _fileService.GetById(c.FileId);
					if (f != null)
					{
						ViewBag.ContractUrl = CommonContorllers.FILE_UPLOAD_MISSION_CONTRACT_PATH + f.FileName;
					}
				}
			}
			if (model.Publisher != UserContext.Current.Id && model.Receiver != UserContext.Current.Id)
			{
				throw new Exception("未知的任务。");
			}
			return View(model);
		}

		/// <summary>
		/// 发布。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorize]
		public RedirectToRouteResult Issue(int id)
		{
			_missionService.Issue(id, _customerService.GetEntityById(UserContext.Current.Id));
			return RedirectToAction("Edit", new { id = id });
		}

		/// <summary>
		/// 确认合同。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorize]
		public RedirectToRouteResult ConfirmContract(int id)
		{
			_missionService.ConfirmContract(id);
			if (!_missionService.GetById(id).IsOffLinePayment)//在线支付。
			{
				throw new Exception("线上付款功能暂时不可用");
				bool isSucceedPay = true;
				if (isSucceedPay)
				{
					return ConfirmPayment(id);
				}
			}
			return RedirectToAction("Edit", new { id = id });
		}

		/// <summary>
		/// 会员确认并托管资金。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorize]
		public RedirectToRouteResult ConfirmPayment(int id)
		{
			_missionService.ConfirmPayment(id);
			return RedirectToAction("Edit", new { id = id });
		}

		/// <summary>
		/// 申请验收。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorize]
		public RedirectToRouteResult Check(int id)
		{
			_missionService.Check(id);
			return RedirectToAction("Edit", new { id = id });
		}

		/// <summary>
		/// 验收通过。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorize]
		public RedirectToRouteResult Pass(int id)
		{
			_missionService.Pass(id);
			if (!_missionService.GetById(id).IsOffLinePayment)
			{
				throw new Exception("线上付款功能暂时不可用");
				_missionService.ConfirmPaymentPeriod(id);
				return null;
			}
			return RedirectToAction("Edit", new { id = id });
		}

		/// <summary>
		/// 支付本期项目款。
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[UserAuthorize]
		public RedirectToRouteResult Payment(int id)
		{
			throw new Exception("线上付款功能暂时不可用。");
		}

		[UserAuthorize]
		public RedirectToRouteResult ConfirmPaymentPeriod(int id)
		{
			_missionService.ConfirmPaymentPeriod(id);
			return RedirectToAction("Edit", new { id = id });
		}

		/// <summary>
		/// 验收不合格。
		/// </summary>
		/// <param name="id"></param>
		/// <param name="failedReason"></param>
		/// <returns></returns>
		[UserAuthorize]
		public RedirectToRouteResult Failed(int id, string failedReason)
		{
			_missionService.Failed(id, failedReason);
			return RedirectToAction("Edit", new { id = id });
		}

		/// <summary>
		/// 申诉。
		/// </summary>
		/// <param name="id"></param>
		/// <param name="appealReason"></param>
		/// <returns></returns>
		[UserAuthorize]
		public RedirectToRouteResult Appeal(int id, string appealReason)
		{
			_missionService.Appeal(id, appealReason);
			return RedirectToAction("Edit", new { id = id });
		}

		/// <summary>
		/// 取消。
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancelReason"></param>
		/// <returns></returns>
		[UserAuthorize]
		public ActionResult Cancel(int id, string cancelReason)
		{
			_missionService.Cancel(id);
			return RedirectToAction("Edit", new { id = id });
		}

		public ViewResult Index(string contact, string mobile, DateTime? limitDate, int? missionType, decimal? totalPrice, string title, string description)
		{
			Customer member = _customerService.GetEntityById(UserContext.Current.Id);
			Mission m = new Mission();
			if (member != null && member.GroupId == (int)CommonEnum.GroupOfCustomer.Member)
			{
				m.PublisherContact = member.Name;
				m.PublisherMobile = member.Mobile;
			}
			if (!string.IsNullOrEmpty(contact))
			{
				m.PublisherContact = contact;
			}
			if (!string.IsNullOrEmpty(mobile))
			{
				m.PublisherMobile = mobile;
			}
			if (limitDate.HasValue)
			{
				m.LimitDate = limitDate.Value.ToString(Define.DATE_FORMAT_DAY);
			}
			if (missionType.HasValue)
			{
				m.MissionType = missionType.Value;
			}
			if (totalPrice.HasValue)
			{
				m.TotalPrice = totalPrice.Value;
			}
			if (!string.IsNullOrEmpty(title))
			{
				m.Title = title;
			}
			if (!string.IsNullOrEmpty(description))
			{
				m.Description = description;
			}
			ViewBag.TypeList = _missionService.GetMissionTypeList();
			ViewBag.MissionTop5 = _missionService.GetMissionListTopN(5);
			return View(m);
		}

		[UserAuthorize]
		public ActionResult New(Mission model)
		{
			ViewBag.TypeList = _missionService.GetMissionTypeList();
			if (!ModelState.IsValid)
			{
				return View("Index");
			}
			if (model == null)
			{
				throw new Exception("未知任务。");
			}
			if (UserContext.Current.Id == 2)//没登录。
			{
				return RedirectToAction("Index", "Login", new { returnUrl = HttpUtility.UrlEncode("/Mission/Index?contact=" + model.PublisherContact + "&mobile=" + model.PublisherMobile + "&limitDate=" + model.LimitDate + "&missionType=" + model.MissionType + "&totalPrice=" + model.TotalPrice + "&title=" + model.Title + "&description=" + model.Description) });
			}
			M_Mission m = new M_Mission();
			m.Publisher = UserContext.Current.Id;
			m.PublisherContact = model.PublisherContact;
			m.PublisherMobile = model.PublisherMobile;
			m.LimitDate = model.LimitDate.ToDateTime();
			m.MissionType = model.MissionType;
			m.TotalPrice = model.TotalPrice.Value;
			m.Title = model.Title;
			m.Description = model.Description;
			_missionService.Create(m);
			_missionService.Issue(m.Id, _customerService.GetEntityById(UserContext.Current.Id));
			return RedirectToAction("Edit", new { id = m.Id });
		}

		[UserAuthorize]
		public ActionResult ConfirmAndPay(int id, int paymentType)
		{
			M_Mission m = _missionService.GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			_missionService.ConfirmContract(id);
			bool paymentResult = false;
			switch ((CommonEnum.TypeOfPayment)paymentType)
			{
				case CommonEnum.TypeOfPayment.WeiXin:
					throw new Exception("微信支付暂时不可用。");
					if (paymentResult)
					{
						_missionService.ConfirmPayment(id);
					}
					break;
				case CommonEnum.TypeOfPayment.ZhiFuBao:
					throw new Exception("支付宝支付暂时不可用。");
					if (paymentResult)
					{
						_missionService.ConfirmPayment(id);
					}
					break;
				case CommonEnum.TypeOfPayment.OffLine:
					break;
				default:
					break;
			}
			string message = string.Empty;
			if ((CommonEnum.TypeOfPayment)paymentType == CommonEnum.TypeOfPayment.OffLine)
			{
				message = @"请尽快线下转账，您的对接客服【" + m.FkBd.Name + " " + m.FkBd.Mobile + "】将在48小时内确认是否到账！";
			}
			return RedirectToAction("Edit", new { id = m.Id, message = message });
		}
	}
}
