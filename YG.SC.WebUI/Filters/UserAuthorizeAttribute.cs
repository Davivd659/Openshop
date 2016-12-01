
namespace YG.SC.WebUI.Filters
{
    using System.Web;
    using YG.SC.WebUI.Controllers;
    using System.Web.Mvc;

    /// <summary>
    /// 类名称：UserAuthorize
    /// 命名空间：YG.SC.WebUI.Filters
    /// 类功能：用户控制登录
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/22 17:23
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class UserAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// 在过程请求授权时调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文，它封装有关使用 <see cref="T:System.Web.Mvc.AuthorizeAttribute" /> 的信息。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 17:23
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!IsLogin(filterContext.RequestContext.HttpContext))
            {
                filterContext.Result=new RedirectResult("/Login/Index");
            }

            //var controller = filterContext.RouteData.Values["controller"].ToString();
            //var action = filterContext.RouteData.Values["action"].ToString();
            //if (!IsAllowed(controller, action))
            //{
            //    filterContext.RequestContext.HttpContext.Response.Write("无权访问");
            //    filterContext.RequestContext.HttpContext.Response.End();
            //}
        }

        /// <summary>
        /// Description:
        /// </summary>
        /// <param name="context">The context</param>
        /// <returns></returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 17:23
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private bool IsLogin(HttpContextBase context)
        {
            return context.Session[CommonContorllers.UserIdCacheName] != null &&
                   context.Session[CommonContorllers.UserNameCacheName] != null;
        }

    }
}