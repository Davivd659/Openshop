
namespace YG.SC.WebAPIService.Controllers
{
    using System.Net.Http;
    using System.Web.Http;
    using YG.SC.Service;
    using YG.SC.WebAPIService.Models;

    /// <summary>
    /// 类名称：AppController
    /// 命名空间：YG.SC.WebAPIService.Controllers
    /// 类功能：app升级服务
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/10/13 12:05
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class AppController : WebApiBaseController
    {
        /// <summary>
        /// 字段_appService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/13 12:15
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly IAppService _appService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppController"/> class.
        /// </summary>
        /// <param name="appService">The appService</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/13 12:15
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public AppController(IAppService appService)
        {
            this._appService = appService;
        }

        /// <summary>
        /// 释放对象使用的非托管资源，并有选择性地释放托管资源。
        /// </summary>
        /// <param name="disposing">若为 true，则同时释放托管资源和非托管资源；若为 false，则仅释放非托管资源。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/13 12:15
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            this._appService.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// The method will
        /// </summary>
        /// <param name="echostr">随机字符串防止缓存用</param>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/13 12:17
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [HttpGet]
        public HttpResponseMessage Version([FromUri]string echostr = "")
        {
            var appVersion = this._appService.GetVersion(SourceCd);

            var appVersionModel = new AppVersionModel
            {
                Version = appVersion.Version,
                VersionCode = appVersion.VersionCode,
                IsMandatory = appVersion.IsMandatory == "1",
                UrlAddress = appVersion.UrlAddress,
                SDesc = appVersion.SDesc
            };
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<AppVersionModel>
                {
                    Result = appVersionModel
                }.Transform()
            };
        }
    }

}