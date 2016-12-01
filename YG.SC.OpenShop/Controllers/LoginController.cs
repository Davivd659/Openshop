using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using NetDimension.Weibo;
using YG.SC.DataAccess;
using YG.SC.OpenShop.Authentication;
using YG.SC.OpenShop.Enum;
using YG.SC.OpenShop.Helper;
using YG.SC.OpenShop.Models;
using YG.SC.Service.IService;
using Cookie = YG.SC.OpenShop.Helper.Cookie;
using YG.SC.WeiXin.Models;
using YG.SC.Common;
using System.Web.Security;

namespace YG.SC.OpenShop.Controllers
{
	public class LoginController : Controller
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
		public ActionResult Index(string returnUrl = "")
		{
			if (_loginOrginType == LoginOrginType.Sina)
			{
				code = Request.QueryString["code"];
				accressToken = _oauth.GetAccessTokenByAuthorizationCode(code);
				cookie["AccessToken"] = accressToken.Token;
				Id = accressToken.UID;
				var request = HttpWebRequest.Create(new Uri("https://api.weibo.com/2/users/show.json"));//通过get请求获取用户信息，参数传cod/Customer/Index?CustomerId=e或者appkey， 还有id或者name
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
			if (rememberMe == "true" && !string.IsNullOrEmpty(RememberName))
			{
				var entity = this._iCustomerService.GetEntityByName(RememberName);
				WriteUserSession(entity, rememberMe);
				return RedirectToAction("index", "Home");
			}
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
		[HttpPost]
		[ActionName("index")]
		public ActionResult Login(string LoginName, string Password, string rememberMe = "checked", string returnUrl = "")
		{
			this.Session.Clear();
			var cookie = Response.Cookies["rememberMeLogin"];
			if (cookie != null)
				cookie.Expires = DateTime.Now.AddDays(-100);
			var acookie = Response.Cookies["rememberMeName"];
			if (cookie != null)
				acookie.Expires = DateTime.Now.AddDays(-100);
			YG.SC.OpenShop.Authentication.FormAuthenticationService fom = new FormAuthenticationService();
			fom.SignOut();
			//获取账户
			string message = "IsSuccss";
			var entity = this._iCustomerService.GetEntityByNameAndPassword(LoginName, Password);
			if (entity == null
				|| !entity.GroupId.HasValue
				|| (CommonEnum.GroupOfCustomer)entity.GroupId != CommonEnum.GroupOfCustomer.Member && (CommonEnum.GroupOfCustomer)entity.GroupId != CommonEnum.GroupOfCustomer.OpenShop
				)//前台只有会员和商家可以登录。
			{
				entity = null;
			}
			if (entity == null)
			{
				message = "用户名或密码错误";
				ViewBag.msg = message;
				return View("index");
				// if (!entity.Password.Equals(Password)) return "密码错误";
				//if (entity.UserCd != UserCdSenior) return Json(new ResultModel(false, "权限错误"));
			}

			if (entity.GroupId == (int)YG.SC.Common.CommonEnum.GroupOfCustomer.Member)
			{
				WriteUserSession(entity, rememberMe);
				if (!string.IsNullOrEmpty(returnUrl))
				{
					return Redirect(HttpUtility.UrlDecode(returnUrl));
				}
				return Redirect("~/Customer/Memberindex/" + entity.Companyid);
			}
			else
			{
				message = "用户名或密码错误";
				ViewBag.msg = message;
				return View();
			}
		}

		[HttpPost]
		[ActionName("Login")]
		public ContentResult LoginAjax(string LoginName, string Password, string rememberMe = "checked", string returnUrl = "")
		{
			this.Session.Clear();
			var cookie = Response.Cookies["rememberMeLogin"];
			if (cookie != null)
				cookie.Expires = DateTime.Now.AddDays(-100);
			var acookie = Response.Cookies["rememberMeName"];
			if (cookie != null)
				acookie.Expires = DateTime.Now.AddDays(-100);
			YG.SC.OpenShop.Authentication.FormAuthenticationService fom = new FormAuthenticationService();
			fom.SignOut();
			//获取账户
			string message = "IsSuccss";
			var entity = this._iCustomerService.GetEntityByNameAndPassword(LoginName, Password);
			if (entity == null
				|| !entity.GroupId.HasValue
				|| (CommonEnum.GroupOfCustomer)entity.GroupId != CommonEnum.GroupOfCustomer.Member
				)//只有会员可以登录。
			{
				entity = null;
			}
			if (entity == null)
			{
				message = "用户名或密码错误";
				ViewBag.msg = message;
				return Content("0");//登录失败。
			}
			WriteUserSession(entity, rememberMe);
			return Content("1");//登录成功。
		}
		private void WriteUserSession(Customer user, string rememberMe)
		{
			Session[CommonContorllers.UserIdCacheName] = user.Id;
			Session[CommonContorllers.UserNameCacheName] = user.LoginName;

			//if (!rememberMe.Equals("true", StringComparison.OrdinalIgnoreCase))
			//{
			//    var cookie = Response.Cookies["rememberMeName"];
			//    if (cookie != null)
			//        cookie.Expires = DateTime.Now.AddDays(-100);
			//    var httpCookie = Response.Cookies["rememberMePassword"];
			//    if (httpCookie != null)
			//        httpCookie.Expires = DateTime.Now.AddDays(-100);
			//}
			//else
			//{
			var _authenticationService = new FormAuthenticationService();
			_authenticationService.SignIn(user.LoginName, true);

			var aCookie = new HttpCookie("rememberMeName") { Value = user.LoginName, Expires = DateTime.Now.AddDays(10) };
			var cCookie = new HttpCookie("rememberMeLogin") { Value = rememberMe, Expires = DateTime.Now.AddDays(10) };
			Response.Cookies.Add(aCookie);
			Response.Cookies.Add(cCookie);
			//}
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
			if (this._iCustomerService.GetEntityByName(model.UserName) != null)
			{
				msg = "此用户已存在";

				ViewBag.msg = msg;
				return View("Regist");
			}
			if (this._iCustomerService.GetCustomerByMobile(model.Moblie) != null)
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
			cs.GroupId = (int)YG.SC.Common.CommonEnum.GroupOfCustomer.Member;
			cs.Online = true;
			cs.Property = model.Property == "个人" ? false : true;
			cs.registertime = DateTime.Now;

			this._iCustomerService.Insert(cs);
			var entity = this._iCustomerService.GetEntityByNameAndPassword(cs.Mobile, cs.Password);
			string rememberMe = "";
			WriteUserSession(entity, rememberMe);
			if (entity.GroupId == (int)YG.SC.Common.CommonEnum.GroupOfCustomer.OpenShop)
			{
				return Redirect("~/Merchant/Company_index/" + entity.Companyid);
			}
			else
			{
				return Redirect("~/Customer/Memberindex/" + entity.Companyid);
			}
		}

		public string ajaxSend(string phone)
		{
			var randomCode = new Random().Next(100000).ToString("D6");
			CheckMobileModel model = new CheckMobileModel()
			{
				Phone = phone,
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
				YG.SC.OpenShop.PCnCache.Instance.Add(model.Phone, randomCode);
				// 更新短信通道 

				SmsLog smsModel = new SmsLog()
				{
					PhoneNumber = model.Phone,
					SendStatus = smsResult
					,
					Content = model.Message
				};
				_iCustomerService.SendSmsSaveLog(smsModel);
				return
					"ok";
				// SimpleResultModel.Conclude(ValidatePhoneStatus.验证码已发送);

			}
			catch (Exception ex)
			{
				return "-1";
				// return SimpleResultModel.Conclude(ValidatePhoneStatus.发送失败);
			}

		}
		public string ValidateMobile(ValidatePhoneNumber model)
		{
			// string syscode = .Instance.Get(model.Phone, randomCode);
			string CacheValue = "";
			if (!string.IsNullOrEmpty(model.Phone))
			{
				CacheValue = YG.SC.OpenShop.PCnCache.Instance.Get(model.Phone);
			}
			if (CacheValue == model.Code)
			{
				return "ok";

			}

			return "-1";
		}
		public ActionResult Logout()
		{
			string url = "";
			this.Session.Clear();
			var cookie = Response.Cookies["rememberMeLogin"];
			if (cookie != null)
				cookie.Expires = DateTime.Now.AddDays(-100);
			var acookie = Response.Cookies["rememberMeName"];
			if (cookie != null)
				acookie.Expires = DateTime.Now.AddDays(-100);
			if (UserContext.Current.Groupid == (int)Common.CommonEnum.GroupOfCustomer.OpenShop)
			{
				url = "/Merchant/Company_Login";
			}
			else
			{
				url = "/Login";
			}
			YG.SC.OpenShop.Authentication.FormAuthenticationService fom = new FormAuthenticationService();
			fom.SignOut();
			return Redirect(url);
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

		/// <summary>
		/// 登出不切换页面
		/// </summary>
		/// <returns></returns>
		//public bool Logout2()
		//{
		//    this.Session.Clear();
		//    var cookie = Response.Cookies["rememberMeLogin"];
		//    if (cookie != null)
		//        cookie.Expires = DateTime.Now.AddDays(-100);
		//    var acookie = Response.Cookies["rememberMeName"];
		//    if (cookie != null)
		//        acookie.Expires = DateTime.Now.AddDays(-100);

		//    FormsAuthentication.SignOut();
		//    return true;
		//}
	}
}
