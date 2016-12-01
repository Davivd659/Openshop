using System.Web;
using System.Web.Mvc;

namespace YG.SC.WebCrawler
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}