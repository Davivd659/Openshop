
using System;
using System.Web.Caching;
using System.Web.Mvc;
using YG.SC.Common;

namespace YG.SC.WebAPIService.Controllers
{
    using System.Web.Http;
    using System.Net;
    using System.Net.Http;
    using YG.SC.Service;
    using YG.SC.WebAPIService.Models;
    using YG.SC.WebAPIService.Filters;

    public class HellWorldController : WebApiBaseController
    {
        /// <summary>
        /// 字段_helloWorldService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 11:58
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly IHelloWorldService _helloWorldService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HellWorldController"/> class.
        /// </summary>
        /// <param name="helloWorldService">The helloWorldService</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 15:13
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public HellWorldController(IHelloWorldService helloWorldService)
        {
            this._helloWorldService = helloWorldService;
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 15:13
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        public HttpResponseMessage Get()
        {
            var msg = this._helloWorldService.Hello(0);

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<string>
                {
                    Result = msg,
                    StatusCode = (int)ApiStatusCode.Succeed.Ok,
                    StatusMsg = "请求成功"
                }.Transform(),
                StatusCode = HttpStatusCode.OK
            };
        }

        [ScApiFilter]
        [HttpPost]
        [ActionName("Hello2")]
        public HttpResponseMessage Hello2([FromBody] HellWorldParameter hellWorldParameter, [FromUri] string sourcecd)
        {
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<string>
                {
                    Result = string.Format("{0}_{1}", sourcecd, hellWorldParameter.Name),
                    StatusCode = (int)ApiStatusCode.Succeed.Ok,
                    StatusMsg = "请求成功"
                }.Transform(),
                StatusCode = HttpStatusCode.OK
            };
        }

        [ScApiFilter]
        [HttpPost]
        [ActionName("Hello")]
        public HttpResponseMessage Hello([FromBody] HellWorldParameter hellWorldParameter, [FromUri] string sourcecd)
        {
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<string>
                {
                    Result = string.Format("{0}_{1}", sourcecd, hellWorldParameter.Name),
                    StatusCode = (int)ApiStatusCode.Succeed.Ok,
                    StatusMsg = "请求成功"
                }.Transform(),
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpGet]
        public HttpResponseMessage Test()
        {
            //string memcacheValue;
            //const string memcacheKey = "test1";
            //if (CacheUtility.MemcacheInstance.Exists(memcacheKey))
            //{
            //    memcacheValue = CacheUtility.MemcacheInstance.Get(memcacheKey) as string;
            //}
            //else
            //{
            //    memcacheValue = DateTime.Now.ToString();
            //    CacheUtility.MemcacheInstance.Add(memcacheKey, memcacheValue);
            //}
            //return new HttpResponseMessage
            //{
            //    Content = new StringContent(string.Join(",",CacheUtility.MemcacheInstance.AllKeys))
            //};
            return new HttpResponseMessage
            {
                Content = new StringContent("")
            };
        }

    }
}
