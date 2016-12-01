using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.OpenShop.Models
{
    public class SmsConfig
    {
        public static SmsConfig Instance = new SmsConfig();
        /// <summary>
        /// 注册短信
        /// </summary>
        public string RgistetSmsText = "验证码：{0}";
    }
}