using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YG.SC.Common;

namespace YG.SC.WeiXin.Models
{
    public enum ValidatePhoneStatus
    {
        [DisplayText("正在发送")]
        验证码已发送 = 1,

        [DisplayText("手机号码无效")]
        手机号码无效,

        [DisplayText("发送失败")]
        发送失败
    }

    public enum ValidatePhoneCode
    {
        [DisplayText("Success")]
        Success = 1,

        [DisplayText("Failed")]
        Failed

    }

}