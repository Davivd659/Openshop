using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WeiXin.Models
{
    public class CheckMobileModel
    {
        public string Phone { get; set; }
        public string Message { get; set; }

    }
    public class ValidatePhoneNumber
    {
        public string Phone { get; set; }
        public string Code { get; set; }

    }
}