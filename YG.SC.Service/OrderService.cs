using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Service.IService;
using YG.SC.DataAccess;
using YG.SC.Repository;
using YG.SC.Model;
using YG.SC.Common;

namespace YG.SC.Service
{
	public class OrderService : IOrderService
	{
		private readonly IRepository<O_Order> _OrderRepository;

		public OrderService(IRepository<O_Order> orderRepository)
		{
			_OrderRepository = orderRepository;
		}

		public void Insert(DataAccess.O_Order order)
		{
			this._OrderRepository.Insert(order);
			this._OrderRepository.SaveChanges();
		}
		
		public void Dispose()
		{
			this.Dispose();
		}
		
		public O_Order GetByID(int id)
		{
			return this._OrderRepository.GetById(id);
		}
		
		public Tuple<O_Order[], Model.PagerEntity> GetSeachOrder(OrderSearchCriteria order)
		{
			int top = 6;
			if (order.PageSize > 0)
			{ top = order.PageSize; }

			var idx = (order.PageIndex - 1) < 0 ? 0 : (order.PageIndex - 1);

			var query = _OrderRepository.Table.Where(m => m.Status == 1);
			if (!string.IsNullOrEmpty(order.Keys))
			{
				query = query.Where(m => m.BuyerName.Contains(order.Keys) || m.BuyerMobile.Contains(order.Keys));
			}
			int total = query.Count();
			var array = query.OrderByDescending(m => m.OrderId).Skip(idx * top).Take(top).ToArray();

			return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
		}
		
		public void Updata(O_Order order)
		{
			this._OrderRepository.Update(order);
			this._OrderRepository.SaveChanges();
		}
		
		public Dictionary<int, string> GetFundFlowList()
		{
			return CommonEnum.GetDictionaryFromEnum(typeof(CommonEnum.FundFlow));
		}

		public Dictionary<int, string> GetOrderTypeList()
		{
			return CommonEnum.GetDictionaryFromEnum(typeof(CommonEnum.OrderType));
		}

		public Tuple<OrderSearchResult[], PagerEntity> GetOrderList(OrderSearchFilter filter)
		{
			int? userId = null;
			if (filter.Role == CommonEnum.GroupOfCustomer.Vistor)
			{
				userId = -1;
			}
			else if (filter.Role == CommonEnum.GroupOfCustomer.Member || filter.Role == CommonEnum.GroupOfCustomer.OpenShop)
			{
				userId = filter.UserId;
			}
			DateTime? dtRight = filter.OrderTimeRight.HasValue ? (DateTime?)filter.OrderTimeRight.Value.AddDays(1) : null;
			if (!string.IsNullOrEmpty(filter.Key) && filter.Key.StartsWith(Define.ORDER_NUMBER_PERFIX))
			{
				filter.Key = filter.Key.Substring(Define.ORDER_NUMBER_PERFIX.Length);
			}
			var qTop = (from o in _OrderRepository.Table
						where (!userId.HasValue || o.Buyer == userId)
						&& (!filter.FundFlow.HasValue || filter.FundFlow == CommonEnum.FundFlow.Income ? o.TotalPrice < 0 : o.TotalPrice > 0)
						&& (!filter.OrderType.HasValue || (CommonEnum.OrderType)o.OrderType == filter.OrderType)
						&& (!filter.OrderTimeLeft.HasValue || o.OrderTime >= filter.OrderTimeLeft)
						&& (!dtRight.HasValue || o.OrderTime < dtRight)
						&& (string.IsNullOrEmpty(filter.Key) || o.OrderId.ToString().Contains(filter.Key) || o.SalerName.Contains(filter.Key) || o.SalerMobile.Contains(filter.Key))
						select new OrderSearchResult()
						{
							Id = o.OrderId,
							OrderTime = o.OrderTime,
							OrderType = (CommonEnum.OrderType)o.OrderType,
							Title = o.Title,
							DetailUrl = o.DetailUrl,
							Contact = o.SalerName,
							Mobile = o.SalerMobile,
							Price = o.TotalPrice,
							Status = (CommonEnum.OrderStatus)o.Status,
							BusinessDataType = (CommonEnum.BusinessDataType)o.BusinessDataType,
							BusinessDataId = o.BusinessDataId
						});
			var q = qTop.Union(
								 from o in _OrderRepository.Table
								 where (!userId.HasValue || o.Saler == userId)
								 && (!filter.FundFlow.HasValue || filter.FundFlow == CommonEnum.FundFlow.Income ? o.TotalPrice > 0 : o.TotalPrice < 0)
								 && (!filter.OrderType.HasValue || (CommonEnum.OrderType)o.OrderType == filter.OrderType)
								 && (!filter.OrderTimeLeft.HasValue || o.OrderTime >= filter.OrderTimeLeft)
								 && (!dtRight.HasValue || o.OrderTime < dtRight)
								 && (string.IsNullOrEmpty(filter.Key) || o.OrderId.ToString().Contains(filter.Key) || o.BuyerName.Contains(filter.Key) || o.BuyerMobile.Contains(filter.Key))
								 select new OrderSearchResult()
								 {
									 Id = o.OrderId,
									 OrderTime = o.OrderTime,
									 OrderType = (CommonEnum.OrderType)o.OrderType,
									 Title = o.Title,
									 DetailUrl = o.DetailUrl,
									 Contact = o.BuyerName,
									 Mobile = o.BuyerMobile,
									 Price = o.TotalPrice,
									 Status = (CommonEnum.OrderStatus)o.Status,
									 BusinessDataType = (CommonEnum.BusinessDataType)o.BusinessDataType,
									 BusinessDataId = o.BusinessDataId
								 });
			var query = (from t in q
						 orderby t.Id descending
						 select t);
			return filter.GetPagerData(query);
		}

		/// <summary>
		/// 获取最新n个订单。
		/// </summary>
		/// <param name="user"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		public List<OrderSearchResult> GetOrderTopN(Customer user, int n = 5)
		{
			OrderSearchFilter filter = new OrderSearchFilter();
			filter.pg = 1;
			filter.PageSize = n;
			filter.UserId = user.Id;
			filter.Role = (CommonEnum.GroupOfCustomer)user.GroupId;
			Tuple<OrderSearchResult[], PagerEntity> orderList = GetOrderList(filter);
			if (orderList!=null)
			{
				return orderList.Item1.ToList();
			}
			return null;
		}
	}
}
