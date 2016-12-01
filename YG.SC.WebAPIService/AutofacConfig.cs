
namespace YG.SC.WebAPIService
{
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using System.Data.Entity;
    using System.Reflection;
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

            // builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).PropertiesAutowired();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType(typeof(FoodMaterialAllianceEntities)).As(typeof(DbContext)).InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterAssemblyTypes(Assembly.Load("YG.SC.Service")).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
   
            var container = builder.Build();
            //   DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}