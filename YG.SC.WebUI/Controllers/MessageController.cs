using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Service;

namespace YG.SC.WebUI.Controllers
{
    public class MessageController : WebBaseController
    {
        private readonly IMessageService _IMessageService;

        public MessageController(IMessageService IMessageService)
        {
            _IMessageService = IMessageService;
        }

        public ActionResult List(int pg = 1, string message = "", string receiver = "")
        {
            MessageSearchCriteria filter = new MessageSearchCriteria();
            filter.pg = pg;
            filter.PageSize = Define.PAGE_SIZE;
            filter.Message = message;
            filter.Receiver = receiver;
            Tuple<S_Message[], PagerEntity> model = this._IMessageService.GetEntitsList(filter);
            return View(model);
        }
    }
}
