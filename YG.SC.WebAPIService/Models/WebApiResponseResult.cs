
namespace YG.SC.WebAPIService.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Net.Http;
    using System.Text;

    /// <summary>
    /// 类名称：WebApiResponseModel
    /// 命名空间：YG.SC.WebAPIService.Models
    /// 类功能：
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/23 14:55
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class WebApiResponseModel<T> where T : class
    {
        /// <summary>
        /// 结果实体
        /// </summary>
        /// <value>
        /// The Result
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 14:03
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public T Result { get; set; }

        /// <summary>
        /// 字段_statusCode
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private int _statusCode;

        /// <summary>
        /// 获取或设置 HTTP 响应的状态代码。
        /// </summary>
        /// <returns>返回 <see cref="T:System.Net.HttpStatusCode" />。 HTTP 响应的状态代码。</returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 14:49
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int StatusCode
        {
            get
            {
                return _statusCode == 0 ? (int)ApiStatusCode.Succeed.Ok : _statusCode;
            }
            set { _statusCode = value; }
        }

        /// <summary>
        /// 字段_statusMsg
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private string _statusMsg;

        /// <summary>
        /// 返回状态信息
        /// </summary>
        /// <value>
        /// The StatusMsg
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 13:59
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string StatusMsg
        {
            get { return string.IsNullOrEmpty(_statusMsg) ? "请求成功" : _statusMsg; }
            set { _statusMsg = value; }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns>
        /// The String
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 15:05
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public HttpContent Transform()
        {
            return new StringContent(JsonConvert.SerializeObject(this, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }), Encoding.UTF8, "application/json");
        }

    }
}