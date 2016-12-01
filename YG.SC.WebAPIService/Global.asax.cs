
namespace YG.SC.WebAPIService
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using YG.SC.Common;
    using YG.SC.WebAPIService.Filters;

    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static Dictionary<string, string> MockCacheDictionary;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log4Utility.Register(ConfigurationManager.AppSettings["log4net"]);//注册日志
           // CacheUtility.Register();//注册缓存

            AutofacConfig.Bootstrapper();

            GlobalConfiguration.Configuration.Filters.Add(new YgScExceptionFilterAttribute());//异常日志

            MockCacheDictionary = new Dictionary<string, string>();
            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
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