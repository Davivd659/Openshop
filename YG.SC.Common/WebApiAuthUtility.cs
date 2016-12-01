
namespace YG.SC.Common
{
    using System;
    using System.Web.Security;

    public class WebApiAuthUtility
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiAuthUtility" /> class.
        /// </summary>
        /// <param name="token">The token</param>
        /// <param name="signature">The signature</param>
        /// <param name="timestamp">The timestamp</param>
        /// <param name="nonce">The nonce</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/23 15:53
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public WebApiAuthUtility(string token, string signature, string timestamp, string nonce)
        {
            this.Token = token;
            this.Signature = signature;
            this.Timestamp = timestamp;
            this.Nonce = nonce;
        }

        /// <summary>
        /// 双方约定加密KEY
        /// </summary>
        /// <value>
        /// The Token
        /// </value>
        /// 创建者：孟祺宙 创建日期：2014/4/18 10:35
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Token { get; set; }

        /// <summary>
        /// 微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数
        /// </summary>
        /// <value>
        /// The Signature
        /// </value>
        /// 创建者：孟祺宙 创建日期：2014/4/18 10:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        /// <value>
        /// The Timestamp
        /// </value>
        /// 创建者：孟祺宙 创建日期：2014/4/18 10:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        /// <value>
        /// The Nonce
        /// </value>
        /// 创建者：孟祺宙 创建日期：2014/4/18 10:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Nonce { get; set; }

        /// <summary>
        /// 验证当前签名
        /// </summary>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙 创建日期：2014/4/18 10:33
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Authorize()
        {
            var columns = new string[] { this.Token, this.Timestamp, this.Nonce };
            Array.Sort(columns);
            var requestSingature = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Join("", columns), "SHA1").ToLower();

            return this.Signature == requestSingature;
        }
    }
}
