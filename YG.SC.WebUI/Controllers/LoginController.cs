
namespace YG.SC.WebUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using YG.SC.Common;
    using YG.SC.DataAccess;
    using YG.SC.Model;
    using YG.SC.Service.IService;
    using YG.SC.WebUI.Authentication;
    using YG.SC.WebUI.Models;

    public class LoginController : Controller
    {
        /// <summary>
        /// 字段UserCdSenior
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 16:47
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private const string UserCdSenior = "SENIOR";

        /// <summary>
        /// 字段 用户信息接口
        /// </summary>
        /// 创建者：边亮
        /// 创建日期：2014/9/22 17:35
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ICustomerGroupOnService _iCustomerGroupOnService;
        private readonly ICustomerService _iCustomerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="ICustomerGroupOnService"></param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/24 14:59
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public LoginController(ICustomerGroupOnService ICustomerGroupOnService, ICustomerService iCustomerService)
        {
            this._iCustomerGroupOnService = ICustomerGroupOnService;
            this._iCustomerService = iCustomerService;
        }

        /// <summary>
        /// 释放非托管资源和托管资源（后者为可选项）。
        /// </summary>
        /// <param name="disposing">若为 true，则同时释放托管资源和非托管资源；若为 false，则仅释放非托管资源。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/24 14:59
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            this._iCustomerGroupOnService.Dispose();
            this._iCustomerService.Dispose();
            base.Dispose(disposing);
        }


        /// <summary>
        /// The method will 
        /// </summary>
        /// <returns>
        /// The ActionResult
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/24 14:59
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public ActionResult Index()
        {
            ViewBag.RememberMe = Request.Cookies["rememberMeName"] != null ? Request.Cookies["rememberMeName"].Value : "";
            ViewBag.rememberMePassword = Request.Cookies["rememberMePassword"] != null ? Request.Cookies["rememberMePassword"].Value : "";
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="rememberMe">The rememberMe</param>
        /// <returns>
        /// The ActionResult
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014/9/22 17:31
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string rememberMe)
        {
            var captcha = Session["captcha"];
            if (captcha == null || !captcha.ToString().Equals(model.Captcha, StringComparison.CurrentCultureIgnoreCase))
            {
                return Json(new ResultModel(false, "验证码错误"));
            }
            //获取账户
            var entity = this._iCustomerService.GetEntityByName(model.LoginName);
            if (entity == null || !entity.GroupId.HasValue || (CommonEnum.GroupOfCustomer)entity.GroupId.Value != CommonEnum.GroupOfCustomer.BD
                                            && (CommonEnum.GroupOfCustomer)entity.GroupId.Value != CommonEnum.GroupOfCustomer.Admin)//后台只有BD和管理员可以登录。
            {
                entity = null;
            }
            if (entity == null) return Json(new ResultModel(false, "用户名或密码错误"));
            if (!entity.Password.Equals(model.Password)) return Json(new ResultModel(false, "密码错误"));
            //if (entity.UserCd != UserCdSenior) return Json(new ResultModel(false, "权限错误"));
            this.Session.Clear();
            WriteUserSession(entity, rememberMe);
            return Json(new ResultModel(true));
        }

        /// <summary>
        /// 保存用户信息session
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="rememberMe">The rememberMe</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 11:27
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private void WriteUserSession(Customer user, string rememberMe)
        {
            // var userRoleIds = this._iScmUserService.GetUserInRole(user.Id);
            //获取当前用户所有权限
            //var actionIds = this._iScmUserService.GetUserActionId(user.Id);
            //获取当前用户所有被限制的权限
            //var actionRestriction = this._iScmUserService.GetUserActionRestriction(user.Id);
            //var userActionIds = actionIds.Except(actionRestriction).ToArray();

            this.Session[CommonContorllers.UserIdCacheName] = user.Id;
            this.Session[CommonContorllers.UserNameCacheName] = user.Name;
            // this.Session[CommonContorllers.UserCdCacheName] = user.UserCd;
            //this.Session[CommonContorllers.UserRoleIdsCacheName] = userRoleIds;
            //this.Session[CommonContorllers.UserActionIdsCacheName] = userActionIds;
            // this.Session[CommonContorllers.UserMenuCacheName] = UserMenu(user.UserCd, userRoleIds, userActionIds);

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
                var bCookie = new HttpCookie("rememberMePassword") { Value = user.Password, Expires = DateTime.Now.AddDays(10) };
                Response.Cookies.Add(aCookie);
                Response.Cookies.Add(bCookie);
            }
        }

        /// <summary>
        /// 用户菜单
        /// </summary>
        /// <param name="userCd">The userCd</param>
        /// <param name="userRoleIds">The userRoleIds</param>
        /// <param name="actionIds">The actionIds</param>
        /// <returns>
        /// List{YG.SC.Model.ScRoleNormalFirstMenu}
        /// </returns>
        /// <value>
        /// The user menu.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 17:39
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private List<ScRoleNormalFirstMenu> UserMenu(string userCd, IEnumerable<int> userRoleIds, IEnumerable<int> actionIds)
        {
            var security = GetRoleSecurity(userCd, userRoleIds);
            var list = new List<ScRoleNormalFirstMenu>();
            foreach (var item in security.Items)
            {
                var firstMenu = new ScRoleNormalFirstMenu { Id = item.Id, Name = item.Name };
                var menuList = new List<ScRoleNormalFirstMenuMenu>();
                foreach (var p in item.Menu)
                {
                    var menu = new ScRoleNormalFirstMenuMenu { Id = p.Id, Name = p.Name };
                    var pageList = new List<ScRoleNormalFirstMenuMenuPage>();
                    foreach (var k in p.Page)
                    {
                        if (actionIds.Contains(Int32.Parse(k.AId)))
                        {
                            var page = new ScRoleNormalFirstMenuMenuPage
                            {
                                Id = k.Id,
                                AId = k.AId,
                                Name = k.Name,
                                Url = k.Url
                            };
                            pageList.Add(page);
                        }
                        menu.Page = pageList.ToArray();
                    }
                    if (menu.Page.Length > 0) menuList.Add(menu);
                }
                firstMenu.Menu = menuList.ToArray();
                list.Add(firstMenu);
            }
            return list;
        }

        /// <summary>
        /// 获取用户菜单原始权限
        /// </summary>
        /// <param name="userCd">The userCd</param>
        /// <param name="userRoleIds">The userRoleIds</param>
        /// <returns>
        /// RoleSecurityEntity
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 16:52
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private RoleSecurityEntity GetRoleSecurity(string userCd, IEnumerable<int> userRoleIds)
        {
            var userRoleId = string.Join("_", userRoleIds);
            var xmlPath = Server.MapPath(CommonContorllers.UserSecurityPath + string.Format("{0}_{1}.xml", userCd, userRoleId));
            string xmlString;
            using (var streamReader = new StreamReader(xmlPath, Encoding.UTF8))
            {
                xmlString = streamReader.ReadToEnd();
            }
            var roleSecurity = XmlUtility<RoleSecurityEntity>.XmlDeserialize(xmlString);

            return roleSecurity;
        }

        /// <summary>
        /// //验证码生成
        /// </summary>
        /// <returns>
        /// FileContentResult
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014/9/22 17:36
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public FileContentResult CaptchaImage()
        {
            var captcha = new LiteralCaptcha(60, 30, 4);
            var bytes = captcha.Generate();
            Session["captcha"] = captcha.Captcha;
            return new FileContentResult(bytes, "image/jpeg"); ;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns>
        /// The ActionResult
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/24 15:01
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public ActionResult Logout()
        {
            this.Session.Clear();
            return RedirectToAction("index", "Login");
        }
    }
}
