using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using NetDimension.Weibo;
using YG.SC.DataAccess;
using YG.SC.WeiXin.Authentication;
using YG.SC.WeiXin.Helper;
using YG.SC.WeiXin.Models;
using YG.SC.Service.IService;
using Cookie = YG.SC.WeiXin.Helper.Cookie;
using System.Threading.Tasks;
using YG.SC.WeiXin;

namespace YG.SC.WeiXin.Controllers
{
    public class LoginController : BaseController
    {
        
        Client _sina = null;
        readonly OAuth _oauth = new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"], ConfigurationManager.AppSettings["CallbackUrl"]);
        Cookie cookie = new Cookie("WeiboDemo", 24, TimeUnit.Hour);
        private string code = "";
        private LoginOrginType _loginOrginType;
        private string Id = "";
        private string Password = "";
        //
        // GET: /Login/
        private readonly ICustomerService _iCustomerService;

        public LoginController(ICustomerService iCustomerService)
        {
            _iCustomerService = iCustomerService;
        }
        protected override void Dispose(bool disposing)
        {
            this._iCustomerService.Dispose();
            base.Dispose(disposing);
        }

        private AccessToken accressToken = null;
        public ActionResult Index()
        {
            string fromurl = Request.QueryString["fromurl"];
            if (_loginOrginType == LoginOrginType.Sina)
            {
                code = Request.QueryString["code"];
                accressToken = _oauth.GetAccessTokenByAuthorizationCode(code);
                cookie["AccessToken"] = accressToken.Token;
                Id = accressToken.UID;
                var request = HttpWebRequest.Create(new Uri("https://api.weibo.com/2/users/show.json"));//通过get请求获取用户信息，参数传code或者appkey， 还有id或者name
                //eg:"https://api.weibo.com/2/users/show.json?source=3842433884&uid=1691097624"
            }
            else if (_loginOrginType == LoginOrginType.Qq)
            {
                
            }
            //ViewBag.RememberMe = Request.Cookies["rememberMeName"] != null ? Request.Cookies["rememberMeName"].Value : "";
            //ViewBag.rememberMePassword = Request.Cookies["rememberMePassword"] != null ? Request.Cookies["rememberMePassword"].Value : "";
            //自动登录
            var rememberMe = Request.Cookies["rememberMeLogin"] != null ? Request.Cookies["rememberMeLogin"].Value : "";
            var RememberName = Request.Cookies["rememberMeName"] != null ? Request.Cookies["rememberMeName"].Value : "";
            //var rememberMePassword = Request.Cookies["rememberMePassword"] != null ? Request.Cookies["rememberMePassword"].Value : "";
            if (rememberMe=="true"&&!string.IsNullOrEmpty(RememberName))
            {
                var entity = this._iCustomerService.GetEntityByName(RememberName);
                WriteUserSession(entity, rememberMe);
                //if (fromurl != "")
                //{
                //    return Redirect("fromurl");
                //}
                //else
                //{
                    return RedirectToAction("index", "Home");
                //}
            }
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public string Login(string username, string password, string rememberMe)
        {
            //获取账户
            var message = "IsSuccss";
            var entity = this._iCustomerService.GetEntityByNameAndPassword(username, password);
            if (entity == null)
            {
                return "用户名或密码错误";
            }
            WriteUserSession(entity, rememberMe);
          
            return message;
        }
        private void WriteUserSession(Customer user, string rememberMe)
        {
            this.Session[CommonContorllers.UserIdCacheName] = user.Id;
            this.Session[CommonContorllers.UserNameCacheName] = user.Name;

            if (!rememberMe.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                var cookie = Response.Cookies["rememberMeName"];
                if (cookie != null)
                    cookie.Expires = DateTime.Now.AddDays(-100);
                var httpCookie = Response.Cookies["rememberMePassword"];
                if (httpCookie != null)
                    httpCookie.Expires = DateTime.Now.AddDays(-100);
            }
            else
            {
                var _authenticationService = new FormAuthenticationService();
                _authenticationService.SignIn(user.Name, true);

                var aCookie = new HttpCookie("rememberMeName") { Value = user.Name, Expires = DateTime.Now.AddDays(10) };
                var cCookie = new HttpCookie("rememberMeLogin") { Value = rememberMe, Expires = DateTime.Now.AddDays(10) };
                Response.Cookies.Add(aCookie);
                Response.Cookies.Add(cCookie);
            }
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Regist()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Regist")]
        public ActionResult RegistAdd(RegistModel model)
        {
            string msg = "";
            if (this._iCustomerService.GetEntityByName(model.UserName)!=null)
            {
                msg =  "此用户已存在";

                ViewBag.msg = msg;
                return View("Regist");
            }
            if (this._iCustomerService.GetCustomerByMobile(model.Moblie)!=null)
            {
                msg = "此手机号已注册";

                ViewBag.msg = msg;
                return View("Regist");
            }
            Customer cs = new Customer();
            cs.Name = model.UserName;
            cs.LoginName = model.UserName;
            cs.Password = model.Password;
            cs.Mobile = model.Moblie;
            cs.Online = true;
            cs.Property = model.Property=="个人"?false:true;

            this._iCustomerService.Insert(cs);

            return Redirect("~/Project/List");
        }
        public string ajaxSend(string phone)
        { 
            var randomCode = new Random().Next(100000).ToString("D6");
            CheckMobileModel model =new CheckMobileModel(){
                Phone = phone ,
                Message = string.Format(SmsConfig.Instance.RgistetSmsText, randomCode)
            };
            YG.SC.Common.SendMessageHelper smsHelper = new Common.SendMessageHelper();
            //if (!CommonValidator.IsValidPhoneNumber(PhoneNo))
            //{
            //    return SimpleResultModel.Conclude(SendCheckCodeStatus.手机号码无效);
            //}
            try
            {
                string smsResult = smsHelper.SendMessage(phone, model.Message);
                YG.SC.Weixin.WeiXinCache.Instance.Add(model.Phone, randomCode);
                // 更新短信通道 
                
                    SmsLog smsModel = new SmsLog()
                    {
                        PhoneNumber = model.Phone,
                        SendStatus =smsResult
                        , Content = model.Message
                    };
                    _iCustomerService.SendSmsSaveLog(smsModel);
                    return
                        "ok";
                     // SimpleResultModel.Conclude(ValidatePhoneStatus.验证码已发送);

            }
            catch (Exception)
            {
                return "-1";
               // return SimpleResultModel.Conclude(ValidatePhoneStatus.发送失败);
            }

        }
        public string ValidateMobile(ValidatePhoneNumber model)
        {
           // string syscode = .Instance.Get(model.Phone, randomCode);

            string CacheValue = YG.SC.Weixin.WeiXinCache.Instance.Get(model.Phone);
            
            if(CacheValue == model.Code){
                return "ok";
                
            }

            return "-1";
        }
        
        public ActionResult Logout()
        {
            this.Session.Clear();
            var cookie = Response.Cookies["rememberMeLogin"];
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-100);
            var acookie = Response.Cookies["rememberMeName"];
            if (cookie != null)
                acookie.Expires = DateTime.Now.AddDays(-100);
            return RedirectToAction("index", "Login");
        }

        public ActionResult SinaLogin()
        {
            _loginOrginType = LoginOrginType.Sina;
            _sina = new Client(_oauth);
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                var token = _oauth.GetAccessTokenByAuthorizationCode(Request.QueryString["code"]);
                string accessToken = token.Token;
                cookie["AccessToken"] = accessToken;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string url = _oauth.GetAuthorizeURL();

                Response.Redirect(url);
                var strss = Request.Url.AbsoluteUri;
                return null;
            }
        }
    }
}
