
namespace YG.SC.WebUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using YG.SC.DataAccess;
    using YG.SC.Model;
    using YG.SC.Service;
    using YG.SC.Service.IService;
    using YG.SC.WebUI.Filters;

    /// <summary>
    /// 类名称：WebBaseController
    /// 命名空间：YG.SC.WebUI.Controllers
    /// 类功能：Web控制器基类
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/17 13:27
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    [UserAuthorize]
    public abstract class WebBaseController : Controller
    {
        /// <summary>
        /// 在调用操作方法前调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 17:50
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TempData["LayoutUserName"] = UserName;
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/19 14:38
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int UserId
        {
            get
            {
                return Session[CommonContorllers.UserIdCacheName] == null ? 2 : Convert.ToInt32(Session[CommonContorllers.UserIdCacheName]);
            }
        }

        /// <summary>
        /// 当前登录用户名
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/17 13:25
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string UserName
        {
            get
            {
                return Session[CommonContorllers.UserNameCacheName] == null
                    ? "未知"
                    : Session[CommonContorllers.UserNameCacheName].ToString();
            }
        }

        /// <summary>
        /// 用户角色code sysrefcd
        /// </summary>
        /// <value>
        /// The user cd.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 16:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected string UserCd
        {
            get
            {
                return Session[CommonContorllers.UserCdCacheName] == null
                    ? "未知" : Session[CommonContorllers.UserCdCacheName].ToString();
            }
        }

        /// <summary>
        /// 用户的角色id (暂无角色叠加)
        /// </summary>
        /// <value>
        /// The user role ids.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 16:51
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected string UserRoleIds
        {
            get
            {
                var roles = this.Session[CommonContorllers.UserRoleIdsCacheName] as int[];
                return roles == null ? string.Empty : string.Join("_", roles);
            }
        }

        /// <summary>
        /// 用户菜单动作id 用户被授权的动作
        /// </summary>
        /// <value>
        /// The user action ids.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 16:53
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected int[] UserActionIds
        {
            get
            {
                return (Session[CommonContorllers.UserActionIdsCacheName] == null
                    ? new int[0]
                    : Session[CommonContorllers.UserActionIdsCacheName] as int[]);

            }
        }

        /// <summary>
        /// 用户菜单
        /// </summary>
        /// <value>
        /// The user menu.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 17:16
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public List<ScRoleNormalFirstMenu> UserMenu
        {
            get
            {
                return this.Session[CommonContorllers.UserMenuCacheName] as List<ScRoleNormalFirstMenu>;
            }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns></returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 19:15
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected bool CanActionShow(string path)
        {
            var page = UserMenu.SelectMany(
               item =>
                   item.Menu.SelectMany(
                       p => p.Page.Where(k => string.Equals(k.Url, path, StringComparison.OrdinalIgnoreCase))))
               .FirstOrDefault();
            return page != null && UserActionIds.Contains(Int32.Parse(page.AId));
        }



        /// <summary>
        /// 默认排序编号
        /// </summary>
        /// <value>
        /// The definition ord seq.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/17 13:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public decimal DefOrdSeq { get { return 1000; } }

        /// <summary>
        /// 默认状态位 0有效 1无效
        /// </summary>
        /// <value>
        /// The definition recsts.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 16:23
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string DefRecsts { get { return "0"; } }
    }
}
