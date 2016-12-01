
namespace YG.SC.WebAPIService.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Controllers;

    /// <summary>
    /// 类名称：WebApiBaseController
    /// 命名空间：YG.SC.WebAPIService.Controllers
    /// 类功能：webapi返回基类
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/23 13:56
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class WebApiBaseController : ApiController
    {
        /// <summary>
        /// 请求来源
        /// </summary>
        /// <value>
        /// The source cd.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:41
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string SourceCd { get; private set; }

        /// <summary>
        /// 使用指定的 controllerContext 初始化 <see cref="T:System.Web.Http.ApiController" /> 实例。
        /// </summary>
        /// <param name="controllerContext">用于初始化的 <see cref="T:System.Web.Http.Controllers.HttpControllerContext" /> 对象。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:42
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            SourceCd = controllerContext.RouteData.Values["sourcecd"].ToString();
            base.Initialize(controllerContext);
        }
    }
}
