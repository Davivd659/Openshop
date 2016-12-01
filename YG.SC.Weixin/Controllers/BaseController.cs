using System;
using System.Web.Mvc;

namespace YG.SC.WeiXin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TempData["LayoutUserName"] = UserName;
            TempData["LayoutUserId"] = UserId;
            base.OnActionExecuting(filterContext);
        }
        public string UserName
        {
            get
            {
                return Session[CommonContorllers.UserNameCacheName] == null
                    ? ""
                    : Session[CommonContorllers.UserNameCacheName].ToString();
            }
        }
        public int UserId
        {
            get
            {
                return Session[CommonContorllers.UserIdCacheName] == null ? 2 : Convert.ToInt32(Session[CommonContorllers.UserIdCacheName]);
            }
        }
    }
}
