using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.Model;
using YG.SC.Service;
using YG.SC.Service.IService;

namespace YG.SC.OpenShop.Controllers
{
    public class MessageController : BaseController
    {
        private IMessageService _messageService;
        private ICustomerService _customerService;
        public MessageController(IMessageService messageService, ICustomerService customerService)
        {
            _messageService = messageService;
            _customerService = customerService;
        }
        public ActionResult List(MessageSearchCriteria filter = null)
        {
            if (filter == null)
            {
                filter = new MessageSearchCriteria();
                filter.pg = 1;
            }
            filter.PageSize = Define.PAGE_SIZE;
            filter.UserId = UserId;
            filter.Role = (CommonEnum.GroupOfCustomer)_customerService.GetEntityById(UserId).GroupId;
            ViewBag.MessageSourceList = _messageService.GetSourceList();
            ViewBag.MessageList = _messageService.GetEntitsList(filter);
            return View();
        }

    }
}
