

namespace YG.SC.WebAPIService.Filters
{
    using System.Linq;
    using System.Web.Http.Filters;
    using YG.SC.Common;

    /// <summary>
    /// 类名称：YgScExceptionFilterAttribute
    /// 命名空间：YG.SC.WebAPIService.Filters
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/10/20 15:27
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class YgScExceptionFilterAttribute : ExceptionFilterAttribute 
    {
        /// <summary>
        /// 引发异常事件。
        /// </summary>
        /// <param name="actionExecutedContext">操作的上下文。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/20 15:27
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Log4.LogException(actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName, actionExecutedContext.ActionContext.ActionDescriptor.ActionName, actionExecutedContext.ActionContext.ActionArguments.Select(item => string.Format("{0}：{1}", item.Key, item.Value)).ToArray(), actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}