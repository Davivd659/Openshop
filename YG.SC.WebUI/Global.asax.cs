using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YG.SC.Common;

namespace YG.SC.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Log4Utility.Register(ConfigurationManager.AppSettings["log4net"]);//注册日志
           // CacheUtility.Register();//注册缓存

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutofacConfig.Bootstrapper();

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.RegisterMappings();
          // HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The Object</param>
        /// <param name="e">The EventArgs</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/12 15:24
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected void Application_Error(object sender, EventArgs e)
        {
            Log4.LogException("Global", "Application_Error", null, Server.GetLastError());
        }

    }
}