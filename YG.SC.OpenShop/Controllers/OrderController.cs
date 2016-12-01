using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Service.IService;

namespace YG.SC.OpenShop.Controllers
{
	public class OrderController : BaseController
	{
		private ICustomerService _customerService;
		private IOrderService _orderService;

		public OrderController(ICustomerService customerService, IOrderService orderService)
		{
			_customerService = customerService;
			_orderService = orderService;
		}

		public ActionResult List(OrderSearchFilter filter = null)
		{
			if (filter == null)
			{
				filter = new OrderSearchFilter();
				filter.pg = 1;
			}
			filter.PageSize = Define.PAGE_SIZE;
			filter.UserId = UserId;
			filter.Role = (CommonEnum.GroupOfCustomer)_customerService.GetEntityById(UserId).GroupId;
			ViewBag.FundFlowList = _orderService.GetFundFlowList();
			ViewBag.OrderTypeList = _orderService.GetOrderTypeList();
			Tuple<OrderSearchResult[], PagerEntity> orderListPaging = _orderService.GetOrderList(filter);
			ViewBag.OrderList = orderListPaging;
			return View();
		}

	}
}
