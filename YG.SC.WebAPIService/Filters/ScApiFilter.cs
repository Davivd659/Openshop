
namespace YG.SC.WebAPIService.Filters
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using YG.SC.Common;
    using YG.SC.WebAPIService.Controllers;

    /// <summary>
    /// 类名称：ScApiFilter
    /// 命名空间：YG.SC.WebAPIService.Filters
    /// 类功能：通信验证
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/23 15:20
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class ScApiFilter : ActionFilterAttribute
    {
 
        /// <summary>
        /// 在调用操作方法之前发生。
        /// </summary>
        /// <param name="actionContext">操作上下文。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 15:21
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (CommonContorllers.CanLogRequestInfo)
            {
                var requestUrl = actionContext.Request.RequestUri.ToString();
                var requestParameters = JsonConvert.SerializeObject(actionContext.ActionArguments.Values);
                Log4.LogSuccess("BaseController", "Initialize", new object[] { string.Concat("RequestUrl：", requestUrl), "\r\n\r\n", string.Concat("RequestBody：", requestParameters) }, null);
            }

            ////获取信息来源
            //var sourcecd = actionContext.ControllerContext.RouteData.Values["sourcecd"] as string;

            ////获取头验证信息
            //IEnumerable<string> headerValues;
            //var hasYgScHeader = actionContext.Request.Headers.TryGetValues("YgSc", out headerValues);
            //if (!hasYgScHeader)
            //{
            //    actionContext.Response = new HttpResponseMessage
            //    {
            //        StatusCode = System.Net.HttpStatusCode.Unauthorized,
            //        Content = new StringContent("没有访问权限")
            //    };
            //}
            //else
            //{
            //    var canAuthorize = Authorize(headerValues, sourcecd);

            //    if (!canAuthorize)
            //        actionContext.Response = new HttpResponseMessage
            //        {
            //            StatusCode = System.Net.HttpStatusCode.Forbidden,
            //            Content = new StringContent("权限验证失败")
            //        };
            //}
        }

        
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="ygscHeaders">The ygscHeaders</param>
        /// <param name="sourcecd">The sourcecd</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 16:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private bool Authorize(IEnumerable<string> ygscHeaders, string sourcecd)
        {
            var ygscHeader = ygscHeaders.First();
            var dic = ygscHeader.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries)).ToDictionary(item => item[0], item => item[1]);

            if (!dic.ContainsKey("signature") || !dic.ContainsKey("timestamp") || !dic.ContainsKey("nonce")) return false;

            string token = GetTokenBySourceCd(sourcecd),
                signature = dic["signature"],
                timestamp = dic["timestamp"],
                nonce = dic["nonce"];

            return new WebApiAuthUtility(token, signature, timestamp, nonce).Authorize();
        }

        /// <summary>
        /// 根据来源获取token
        /// </summary>
        /// <param name="sourceCd">The sourceCd</param>
        /// <returns>
        /// String
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 15:51
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private string GetTokenBySourceCd(string sourceCd)
        {
            var dic = new Dictionary<string, string>
            {
                {"ANDROID","YG.SC.ANDROID"},
                {"IPHONE","YG.SC.IPHONE"}
            };
            var key = sourceCd.ToUpper();
            return dic.ContainsKey(key) ? dic[key] : string.Empty;
        }
    }

}