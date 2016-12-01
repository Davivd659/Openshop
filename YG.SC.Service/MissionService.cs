using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Service.IService;
using YG.SC.DataAccess;
using YG.SC.Repository;
using YG.SC.Model;
using System.Linq.Expressions;
using YG.SC.Common;

namespace YG.SC.Service
{
	public class MissionService : IMissionService
	{

		private readonly IRepository<M_Mission> _MissionRepository;
		private readonly IRepository<C_Object> _ObjectRepository;
		private readonly IRepository<M_Period> _PeriodRepository;
		private readonly IRepository<M_Contract> _ContractRepository;
		private readonly IRepository<C_File> _FileRepository;
		private readonly IRepository<S_Message> _MessageRepository;
		private readonly IMessageService _messageService;
		private readonly IShopAttributesService _shopAttributesService;
		public MissionService(IRepository<M_Mission> MissionRepository, IRepository<C_Object> ObjectRepository, IRepository<M_Period> PeriodRepository, IRepository<M_Contract> ContractRepository, IRepository<C_File> FileRepository, IRepository<S_Message> MessageRepository, IRepository<Customer> CustomerRepository, IMessageService messageService, IShopAttributesService shopAttributesService)
		{
			_MissionRepository = MissionRepository;
			_ObjectRepository = ObjectRepository;
			_PeriodRepository = PeriodRepository;
			_ContractRepository = ContractRepository;
			_FileRepository = FileRepository;
			_MessageRepository = MessageRepository;
			_messageService = messageService;
			_shopAttributesService = shopAttributesService;
		}
		public void Create(M_Mission mission)
		{
			DateTime dt = DateTime.Now;
			mission.PublisheTime = dt;
			mission.IsOffLinePayment = true;
			mission.Status = (int)CommonEnum.StatusOfMission.Issue;
			mission.LastTime = DateTime.Now;
			mission.IsDelete = false;
			if (string.IsNullOrEmpty(mission.Description))
			{
				mission.Description = mission.Title;
			}
			this._MissionRepository.Insert(mission);
			this._MissionRepository.SaveChanges();
		}

		public void Edit(M_Mission mission)
		{
			this._MissionRepository.Update(mission);
			this._MissionRepository.SaveChanges();
		}

		public Tuple<M_Mission[], PagerEntity> GetEntitsList(MissionSearchCriteria criteria)
		{
			if (criteria == null)
			{
				throw new Exception("参数为空。");
			}
			DateTime? right = !string.IsNullOrEmpty(criteria.IssueDateRight) ? (DateTime?)criteria.IssueDateRight.ToDateTimeNullable().Value.AddDays(1) : null;
			var query = _MissionRepository.Table;
			if (criteria.MissionType != null && criteria.MissionType.Value > 0)
			{
				query = query.Where(m => m.MissionType == criteria.MissionType);
			}
			if (!string.IsNullOrEmpty(criteria.Keys))
			{
				query = query.Where(m => m.Title.Contains(criteria.Keys) || m.FkReceiver.Name.Contains(criteria.Keys));
			}
			if (criteria.Role == CommonEnum.GroupOfCustomer.Admin)//管理员。
			{
				//查看所有任务。
			}
			else if (criteria.Role == CommonEnum.GroupOfCustomer.BD)//BD。
			{
				query = query.Where(m => m.Status < (int)CommonEnum.StatusOfMission.Connecting || m.Bd == criteria.UserId);//查看尚未接洽的任务和自己接洽的任务。
			}
			else if (criteria.Role == (CommonEnum.GroupOfCustomer.Member))//会员可以看自己发布的任务。
			{
				query = query.Where(m => m.Publisher == criteria.UserId);
			}
			else if (criteria.Role == (CommonEnum.GroupOfCustomer.OpenShop))//商家可以看自己接收的任务。
			{
				query = query.Where(m => m.Receiver == criteria.UserId);//自己发布的或自己接收的任务。
			}
			else//不能查看任务。
			{
				query = query.Where(m => false);
			}
			if (!string.IsNullOrEmpty(criteria.IssueDateLeft))
			{
				query = query.Where(m => m.PublisheTime >= criteria.IssueDateLeft.ToDateTime());
			}
			if (right.HasValue)
			{
				query = query.Where(m => m.PublisheTime < right);
			}
			if (criteria.MissionStatus.HasValue)
			{
				if (criteria.MissionStatus == 1)
				{
					query = query.Where(m => m.Status == (int)CommonEnum.StatusOfMission.End);
				}
				else if (criteria.MissionStatus == 2)
				{
					query = query.Where(m => m.Status == (int)CommonEnum.StatusOfMission.Cancel);
				}
				else
				{
					query = query.Where(m => m.Status != (int)CommonEnum.StatusOfMission.Cancel && m.Status != (int)CommonEnum.StatusOfMission.End);
				}
			}
			if (criteria.IsPending.HasValue && criteria.IsPending.Value)
			{
				switch (criteria.Role)
				{
					case CommonEnum.GroupOfCustomer.Member:
						query = query.Where(m => m.Status < (int)CommonEnum.StatusOfMission.ConfirmPayment && (CommonEnum.StatusOfMission)m.Status != CommonEnum.StatusOfMission.Cancel//托管资金未确认。
							|| (CommonEnum.StatusOfMission)m.Status == CommonEnum.StatusOfMission.Contract//待确认合同。
							|| (CommonEnum.StatusOfMission)m.Status == CommonEnum.StatusOfMission.ConfirmContract && !m.IsOffLinePayment//待支付（线上支付）。
							|| (CommonEnum.StatusOfMission)m.Status == CommonEnum.StatusOfMission.ConfirmPayment//资金已托管，已经有任务周期在执行。
								&& (from p in _PeriodRepository.Table
									where p.MissionId == m.Id
									&& ((CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Check//待验收。
									   || (CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Payment)//待支付。
									select p).FirstOrDefault() != null
								);
						break;
					case CommonEnum.GroupOfCustomer.OpenShop:
						query = query.Where(m => (CommonEnum.StatusOfMission)m.Status == CommonEnum.StatusOfMission.ConfirmPayment//资金已托管，已经有任务周期在执行。
							&& (from p in _PeriodRepository.Table
								where p.MissionId == m.Id
								&& ((CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Run//进行中。
								   || (CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Failed)//验收失败。
								   || (CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Payment && m.IsOffLinePayment//支付中，且线下支付。
								select p).FirstOrDefault() != null
							);
						break;
					case CommonEnum.GroupOfCustomer.BD:
						break;
					default:
						query = query.Where(m => false);//任务流程只有会员、开店帮和BD参与，其它角色没有待处理任务。
						break;
				}
			}
			query = query.OrderByDescending(m => m.Id);
			return criteria.GetPagerData(query);
		}
		public void Dispose()
		{
			_MissionRepository.Dispose();
			_ObjectRepository.Dispose();
			_PeriodRepository.Dispose();
			_FileRepository.Dispose();
			_ContractRepository.Dispose();
		}
		public M_Mission GetById(int id)
		{
			return this._MissionRepository.GetById(id);
		}


		public Dictionary<int, string> GetMissionTypeList()
		{
			List<ShopAttributeValues> list = _shopAttributesService.GetListByAttributeId(Define.OPEN_SHOP_TYPE_GROUP_ID);
			if (list == null)
			{
				return null;
			}
			Dictionary<int, string> result = new Dictionary<int, string>();
			foreach (ShopAttributeValues item in list)
			{
				result.Add(item.Id, item.ValueStr.TrimEnd('帮'));
			}
			return result;
		}


		public Dictionary<int, string> GetMissionStatusList()
		{
			return CommonEnum.GetDictionaryFromEnum(typeof(CommonEnum.StatusOfMission));
		}


		public M_Period GetPeriodById(int id)
		{
			return _PeriodRepository.GetById(id);
		}


		public void CreatePeriod(M_Period p)
		{
			_PeriodRepository.Insert(p);
			_PeriodRepository.SaveChanges();
		}


		public int GetPeriodNumber(int missionId)
		{
			List<M_Period> periodListOfMission = _PeriodRepository.Get(p => p.MissionId == missionId).ToList();
			if (periodListOfMission == null || periodListOfMission.Count <= 0)
			{
				return 1;
			}
			return periodListOfMission.Max(p => p.Period) + 1;
		}

		public void UpdatePeriod(M_Period p)
		{
			_PeriodRepository.Update(p);
			_PeriodRepository.SaveChanges();
		}


		public List<M_Period> GetPeriodList(int missionId)
		{
			return _PeriodRepository.Get(p => p.MissionId == missionId).OrderBy(p => p.Period).ToList();
		}


		public M_Contract GetContractByMission(int missionId)
		{
			List<M_Contract> cList = _ContractRepository.Get(m => m.MissionId == missionId).ToList();
			if (cList == null || cList.Count != 1)
			{
				return null;
			}
			return cList[0];
		}


		public void CreateContract(int missionId, string fileName)
		{
			C_File f = new C_File();
			f.FileName = fileName;
			_FileRepository.Insert(f);
			_FileRepository.SaveChanges();
			M_Contract c = new M_Contract();
			c.MissionId = missionId;
			c.FileId = f.Id;
			_ContractRepository.Insert(c);
			_ContractRepository.SaveChanges();
		}

		public bool ExistContract(int missionId, out C_File fDb)
		{
			fDb = null;
			List<M_Contract> cList = _ContractRepository.Get(c => c.MissionId == missionId).ToList();
			if (cList == null || cList.Count != 1)
			{
				return false;
			}
			fDb = _FileRepository.GetById(cList[0].FileId);
			return true;
		}


		public void DeletePeriod(int id)
		{
			_PeriodRepository.Delete(id);
			_PeriodRepository.SaveChanges();
		}

		/// <summary>
		/// 发布。
		/// </summary>
		/// <param name="id"></param>
		public void Issue(int id, Customer publisher)
		{
			if (publisher == null)
			{
				throw new Exception("未知发布者。");
			}
			M_Mission m = _MissionRepository.GetById(id);
			if (m == null)
			{
				return;
			}
			m.Status = (int)CommonEnum.StatusOfMission.Issue;
			m.LastTime =
			m.PublisheTime = DateTime.Now;
			_MissionRepository.SaveChanges();
			S_Message msg = new S_Message()
			{
				Receiver = publisher.Id,
				ReceiverNumber = publisher.Mobile,
				Message = "任务【" + m.Title + "】发布成功！",
				HasShortMessage = false,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
		}

		/// <summary>
		/// 确认合同。
		/// </summary>
		/// <param name="id"></param>
		public void ConfirmContract(int id)
		{
			M_Mission m = _MissionRepository.GetById(id);
			if (m == null)
			{
				return;
			}
			m.Status = (int)CommonEnum.StatusOfMission.ConfirmContract;
			m.ConfirmContractTime =
			m.LastTime = DateTime.Now;
			_MissionRepository.SaveChanges();
		}

		/// <summary>
		/// 会员确认并托管资金（支付在Controller层完成，这里仅更改状态）。
		/// </summary>
		/// <param name="id"></param>
		public void ConfirmPayment(int id)
		{
			M_Mission m = _MissionRepository.GetById(id);
			if (m == null)
			{
				return;
			}
			M_Period period = _PeriodRepository.Get(p => p.MissionId == id && p.Period == 1).FirstOrDefault();
			if (period == null)
			{
				throw new Exception("任务期不存在。");
			}
			m.Status = (int)CommonEnum.StatusOfMission.ConfirmPayment;
			m.ConfirmPaymentTime =
			m.LastTime = DateTime.Now;
			_MissionRepository.SaveChanges();
			RunPeriod(period);
			S_Message msg = new S_Message()
			{
				Receiver = m.Publisher,
				ReceiverNumber = m.PublisherMobile,
				Message = "您已确认并托管任务【" + m.Title + "】的相关款项，商家【" + m.ReceiverContact + " " + m.ReceiverMobile + "】将为您服务！",
				HasShortMessage = true,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
			msg = new S_Message()
			{
				Receiver = m.Receiver,
				ReceiverNumber = m.ReceiverMobile,
				Message = "会员【" + m.Publisher + " " + m.PublisherMobile + "】发布的任务【" + m.Title + "】已支付相关款项，请尽快落实任务！完成后可在“会员中心-我的任务”申请完工确认。",
				HasShortMessage = true,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
		}

		/// <summary>
		/// 申请验收。
		/// </summary>
		/// <param name="id"></param>
		public void Check(int id)
		{
			M_Period period = _PeriodRepository.Get(p => p.MissionId == id && ((CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Run || (CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Failed)).FirstOrDefault();
			if (period == null)
			{
				throw new Exception("未知任务周期。");
			}
			M_Mission m = _MissionRepository.GetById(period.MissionId);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			period.Status = (int)CommonEnum.StatusOfPeriod.Check;
			period.FirstApplyCheckTime = DateTime.Now;
			_PeriodRepository.SaveChanges();
			UpdateMissionLastTimeByPeriod(period, period.FirstApplyCheckTime.Value);
			S_Message msg = new S_Message()
			{
				Receiver = m.Publisher,
				ReceiverNumber = m.PublisherMobile,
				Message = "商家【" + m.ReceiverContact + " " + m.ReceiverMobile + "】已完成您发布的任务【" + m.Title + "】并申请确认，请至“会员中心-我的任务”进行确认！",
				HasShortMessage = true,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
			msg = new S_Message()
			{
				Receiver = m.Receiver,
				ReceiverNumber = m.ReceiverMobile,
				Message = "你已对任务【" + m.Title + "】发起了完工确认！",
				HasShortMessage = false,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
		}

		private void UpdateMissionLastTimeByPeriod(M_Period period, DateTime dateTime)
		{
			if (period == null)
			{
				throw new Exception("未知任务周期。");
			}
			M_Mission m = _MissionRepository.GetById(period.MissionId);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			m.LastTime = dateTime;
			_MissionRepository.Update(m);
			_MissionRepository.SaveChanges();
		}

		/// <summary>
		/// 验收通过。
		/// </summary>
		/// <param name="id"></param>
		public void Pass(int id)
		{
			M_Mission m = _MissionRepository.GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			M_Period period = _PeriodRepository.Get(p => p.MissionId == id && ((CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Check)).FirstOrDefault();
			if (period == null)
			{
				return;
			}
			period.Status = (int)CommonEnum.StatusOfPeriod.Payment;
			period.PassTime = DateTime.Now;
			_PeriodRepository.SaveChanges();
			UpdateMissionLastTimeByPeriod(period, period.PassTime.Value);
			if (!m.IsOffLinePayment)
			{
				throw new Exception("支付平台暂时连不了。");
				ConfirmPaymentPeriod(id);
			}
		}

		/// <summary>
		/// 本期确认付款成功，本期彻底完成。
		/// </summary>
		/// <param name="id"></param>
		public void ConfirmPaymentPeriod(int id)
		{
			M_Mission m = _MissionRepository.GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			M_Period period = _PeriodRepository.Get(p => p.MissionId == id && ((CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Payment)).FirstOrDefault();
			if (period == null)
			{
				return;
			}
			period.Status = (int)CommonEnum.StatusOfPeriod.Finish;
			period.FinishTime = DateTime.Now;
			_PeriodRepository.SaveChanges();
			UpdateMissionLastTimeByPeriod(period, period.FinishTime.Value);
			M_Period nextPeriod = _PeriodRepository.Get(p => p.MissionId == id && p.Period == period.Period + 1).FirstOrDefault();
			if (nextPeriod == null)
			{
				m.Status = (int)CommonEnum.StatusOfMission.Complete;
				m.LastTime = DateTime.Now;
				_MissionRepository.Update(m);
				_MissionRepository.SaveChanges();
			}
			else
			{
				RunPeriod(nextPeriod);
			}
			S_Message msg = new S_Message()
			{
				Receiver = m.Publisher,
				ReceiverNumber = m.PublisherMobile,
				Message = "您已确认商家【" + m.ReceiverContact + " " + m.ReceiverMobile + "】完成任务【" + m.Title + "】，款项支付完成！",
				HasShortMessage = true,
				DetailsUrl = "~/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
			msg = new S_Message()
			{
				Receiver = m.Receiver,
				ReceiverNumber = m.ReceiverMobile,
				Message = "会员【" + m.PublisherContact + "】已通过了您的完工申请，任务款已支付至您的账户！",
				HasShortMessage = true,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
		}

		private void RunPeriod(M_Period period)
		{
			period.Status = (int)CommonEnum.StatusOfPeriod.Run;
			period.StartTime = DateTime.Now;
			_PeriodRepository.Update(period);
			_PeriodRepository.SaveChanges();
			UpdateMissionLastTimeByPeriod(period, period.StartTime.Value);
		}

		/// <summary>
		/// 验收不合格。
		/// </summary>
		/// <param name="id"></param>
		/// <param name="failedReason"></param>
		public void Failed(int id, string failedReason)
		{
			M_Period period = _PeriodRepository.Get(p => p.MissionId == id && (CommonEnum.StatusOfPeriod)p.Status == CommonEnum.StatusOfPeriod.Check).FirstOrDefault();
			if (period == null)
			{
				return;
			}
			period.FailedReasion = failedReason;
			period.Status = (int)CommonEnum.StatusOfPeriod.Failed;
			_PeriodRepository.SaveChanges();
			S_Message msg = new S_Message()
			{
				Receiver = period.FkMission.Publisher,
				ReceiverNumber = period.FkMission.PublisherMobile,
				Message = "您拒绝了商家【" + period.FkMission.ReceiverContact + " " + period.FkMission.ReceiverMobile + "】未完成【" + period.FkMission.Title + "第" + period.Period + "期" + period.Standard + "】，原因：【" + period.FailedReasion + "】",
				HasShortMessage = false,
				DetailsUrl = "~/Mission/Edit/" + period.FkMission.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
			msg = new S_Message()
			{
				Receiver = period.FkMission.Receiver,
				ReceiverNumber = period.FkMission.ReceiverMobile,
				Message = "会员【" + "period.FkMission.PublisherContact" + "】拒绝了您的完工申请，原因：【" + period.FailedReasion + "】，请修正后发起完工申请！",
				HasShortMessage = true,
				DetailsUrl = "/Mission/Edit/" + period.FkMission.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
		}

		/// <summary>
		/// 申诉。
		/// </summary>
		/// <param name="id"></param>
		public void Appeal(int id, string appealReason)
		{
			M_Mission m = _MissionRepository.GetById(id);
			if (m == null)
			{
				return;
			}
			m.AppealReason = appealReason;
			m.Status = (int)CommonEnum.StatusOfMission.Appeal;
			m.AppealTime =
			m.LastTime = DateTime.Now;
			_MissionRepository.SaveChanges();
		}

		/// <summary>
		/// 接单。
		/// </summary>
		/// <param name="id"></param>
		/// <param name="customer"></param>
		public void Connecting(int id, Customer bd)
		{
			if (bd == null)
			{
				throw new Exception("未知用户。");
			}
			if ((CommonEnum.GroupOfCustomer)bd.GroupId != CommonEnum.GroupOfCustomer.BD)
			{
				throw new Exception("未知的用户.");
			}
			M_Mission m = _MissionRepository.GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			if ((CommonEnum.StatusOfMission)m.Status != CommonEnum.StatusOfMission.Issue)
			{
				throw new Exception("只能接单已发布任务。");
			}
			m.Status = (int)CommonEnum.StatusOfMission.Connecting;
			m.ConnectingTime =
				m.LastTime = DateTime.Now;
			m.Bd = bd.Id;
			_MissionRepository.Update(m);
			_MissionRepository.SaveChanges();
			S_Message msg = new S_Message()
			{
				Receiver = m.Publisher,
				ReceiverNumber = m.PublisherMobile,
				Message = "您发布的任务【" + m.Title + "】已有客服【" + bd.Name + " " + bd.Mobile + "】接单，将为您推荐服务商家！",
				HasShortMessage = true,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			};
			_messageService.Send(msg);
		}

		/// <summary>
		/// 完成合同上传及周期设定。
		/// </summary>
		/// <param name="id"></param>
		public void Contract(int id)
		{
			M_Mission m = GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			m.Status = (int)CommonEnum.StatusOfMission.Contract;
			m.ReceiveTime =
			m.LastTime = DateTime.Now;
			_MissionRepository.SaveChanges();
			_messageService.Send(new S_Message()
			{
				Receiver = m.Publisher,
				ReceiverNumber = m.PublisherMobile,
				Message = "您发布的任务【" + m.Title + "】已确认服务商家并上传服务合同，请至“会员中心-我的任务”确认。",
				HasShortMessage = true,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			});
			_messageService.Send(new S_Message()
			{
				Receiver = m.Receiver,
				ReceiverNumber = m.ReceiverMobile,
				Message = "会员【" + m.PublisherContact + "】发布的任务【" + m.Title + "】系统推荐您来落实，待会员确认并托管资金。",
				HasShortMessage = false,
				DetailsUrl = "/Mission/Edit/" + m.Id,
				MessageType = (int)CommonEnum.MessageType.Mission
			});
		}

		/// <summary>
		/// 获取最后发布的n个任务。
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public List<M_Mission> GetMissionListTopN(int count)
		{
			if (count <= 0)
			{
				throw new Exception("数量必须大于0。");
			}
			return (from m in _MissionRepository.Table
					orderby m.PublisheTime descending
					select m).Take(count).ToList();
		}

		/// <summary>
		/// 取消。
		/// </summary>
		/// <param name="id"></param>
		public void Cancel(int id)
		{
			M_Mission m = _MissionRepository.GetById(id);
			if (m == null)
			{
				throw new Exception("未知任务。");
			}
			m.Status = (int)CommonEnum.StatusOfMission.Cancel;
			_MissionRepository.Update(m);
			_MissionRepository.SaveChanges();
		}

		/// <summary>
		/// 获取支付方式。
		/// </summary>
		/// <returns></returns>
		public Dictionary<int, string> GetPaymentTypeList()
		{
			return CommonEnum.GetDictionaryFromEnum(typeof(CommonEnum.TypeOfPayment));
		}

		/// <summary>
		/// 获取待办任务数量。
		/// </summary>
		/// <param name="customer"></param>
		/// <returns></returns>
		public int CountOfPendingMission(Customer customer)
		{
			MissionSearchCriteria criteria = new MissionSearchCriteria();
			criteria.pg = 1;
			criteria.PageSize = int.MaxValue;
			criteria.UserId = customer.Id;
			criteria.Role = (CommonEnum.GroupOfCustomer)customer.GroupId;
			criteria.IsPending = true;
			Tuple<M_Mission[], PagerEntity> missionList = GetEntitsList(criteria);
			int result = 0;
			if (missionList != null && missionList.Item2 != null)
			{
				result = missionList.Item2.Total;
			}
			return result;
		}
	}
}
