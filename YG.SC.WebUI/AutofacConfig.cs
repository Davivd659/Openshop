﻿
namespace YG.SC.WebUI
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;
    using YG.SC.DataAccess;
    using YG.SC.Repository;

    /// <summary>
    /// 类名称：AutofacConfig
    /// 命名空间：YG.SC.WebUI
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/10 18:06
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class AutofacConfig
    {
        /// <summary>
        /// The method will 
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/10 18:07
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static void Bootstrapper()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            builder.RegisterType(typeof(Entities)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();
            //builder.RegisterAssemblyTypes(Assembly.Load("YG.SC.Repository")).Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("YG.SC.Service")).Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}