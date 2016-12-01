using System;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;
using System.Linq;
using System.Data.Entity.SqlServer;
using System.Collections.Generic;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
	public class MessageService : IMessageService
	{
		private readonly IRepository<S_Message> _MessageRepository;
		private readonly IRepository<Customer> _CustomerRepository;
		private readonly ICustomerService _iCustomerService;

		public MessageService(IRepository<S_Message> MessageRepository, IRepository<Customer> CustomerRepository, IRepository<SmsLog> smsLongRepository)
		{
			_MessageRepository = MessageRepository;
			_CustomerRepository = CustomerRepository;
			_iCustomerService = new CustomerService(CustomerRepository, smsLongRepository);
		}
		public Tuple<S_Message[], PagerEntity> GetEntitsList(MessageSearchCriteria filter)
		{
			DateTime? right = filter.SendDateRight.HasValue ? (DateTime?)filter.SendDateRight.Value.AddDays(1) : null;
			var query = (from m in _MessageRepository.Table
						 from c in _CustomerRepository.Table
						 where m.Receiver == c.Id
						 && (m.Receiver == filter.UserId)
						 && (string.IsNullOrEmpty(filter.Message) ? true : m.Message.Contains(filter.Message))
						 && (string.IsNullOrEmpty(filter.Receiver) ? true : c.Name.Contains(filter.Receiver))
						 && (filter.SendDateLeft.HasValue ? m.SendTime >= filter.SendDateLeft : true)
						 && (filter.SendDateRight.HasValue ? m.SendTime < right : true)
						 && (filter.MessageSource.HasValue && filter.MessageSource.Value >= 0 ? (CommonEnum.TypeOfMessage)m.MessageType == filter.MessageSource.Value : true)
						 && (string.IsNullOrEmpty(filter.Key) ? true : (m.FkReceiver.Name.Contains(filter.Key) || m.ReceiverNumber.Contains(filter.Key)))
						 select m);
			query = query.OrderByDescending(m => m.Id);
			return filter.GetPagerData(query);
		}

		/// <summary>
		/// 获取最新5条消息。
		/// </summary>
		/// <param name="user"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		public S_Message[] GetTopN(Customer user, int n = 5)
		{
			MessageSearchCriteria filter = new MessageSearchCriteria();
			filter.pg = 1;
			filter.PageSize = 5;
			filter.UserId = user.Id;
			Tuple<S_Message[], PagerEntity> messages = GetEntitsList(filter);
			if (messages != null)
			{
				return messages.Item1;
			}
			return null;
		}

		/// <summary>
		/// 获取消息来源。
		/// </summary>
		/// <returns></returns>
		public Dictionary<int, string> GetSourceList()
		{
			return CommonEnum.GetDictionaryFromEnum(typeof(CommonEnum.TypeOfMessage));
		}

		public void Dispose()
		{
			_MessageRepository.Dispose();
			_CustomerRepository.Dispose();
		}


		/// <summary>
		/// 发消息。
		/// </summary>
		/// <param name="msg"></param>
		public void Send(S_Message msg)
		{
			if (msg == null)
			{
				throw new Exception("未知消息。");
			}
			msg.Sender = Define.CURRENT_PLATFORM_MESSAGE_ACCOUNT;
			msg.SendTime = DateTime.Now;
			_MessageRepository.Insert(msg);
			_MessageRepository.SaveChanges();
			if (msg.HasShortMessage)
			{
				SendSms(msg.ReceiverNumber, msg.Message);
			}
		}

		/// <summary>
		/// 发短信。
		/// </summary>
		/// <param name="phone"></param>
		/// <returns></returns>
		public void SendSms(string phone, string msg)
		{
			SendMessageHelper smsHelper = new SendMessageHelper();
			string smsResult = string.Empty;
			try
			{
				smsResult = smsHelper.SendMessage(phone, msg);
			}
			catch (Exception ex)
			{
				return;
			}
			SmsLog smsModel = new SmsLog()
			{
				PhoneNumber = phone,
				SendStatus = smsResult,
				Content = msg
			};
			_iCustomerService.SendSmsSaveLog(smsModel);
		}
	}
}
