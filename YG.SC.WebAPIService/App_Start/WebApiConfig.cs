

namespace YG.SC.WebAPIService
{
    using System.Web.Http;
    using WebApi.OutputCache.V2;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{sourcecd}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
           config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => new YgScOutputCache());
        }
    }
}
