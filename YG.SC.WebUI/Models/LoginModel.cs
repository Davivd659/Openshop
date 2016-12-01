using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace YG.SC.WebUI.Models
{
    public class LoginModel
    {

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string Captcha { get; set; }
       
        public bool RememberMe { get; set; }

        public ResultModel CheckModel()
        {
            if (string.IsNullOrEmpty(this.LoginName))
            {
                return new ResultModel(false, "用户名不能为空");
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                return new ResultModel(false, "密码不能为空");
            }
            if (string.IsNullOrEmpty(this.Captcha))
            {
                return new ResultModel(false, "验证码不能为空");
            }
            return new ResultModel(true);
        }
    }
}