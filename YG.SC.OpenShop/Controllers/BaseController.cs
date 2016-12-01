using System;
using System.Web.Mvc;

namespace YG.SC.OpenShop.Controllers
{
    [UserAuthorize]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (UserId > 0)
            {
                TempData["LayoutUserName"] = UserName;
                TempData["LayoutUserId"] = UserId;
            }
            else
            {
                YG.SC.OpenShop.Authentication.FormAuthenticationService fom = new YG.SC.OpenShop.Authentication.FormAuthenticationService();
                fom.SignOut();
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
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
                return Session[CommonContorllers.UserIdCacheName] == null ? 0 : Convert.ToInt32(Session[CommonContorllers.UserIdCacheName]);
            }
        }
    }
}
