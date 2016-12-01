using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.DataAccess;
using YG.SC.Repository;
using YG.SC.Service;
using YG.SC.Service.IService;

namespace YG.SC.OpenShop.Controllers
{
    [UserAuthorize]
    public class InternalController : Controller
    {

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
    }
}
