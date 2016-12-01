using YG.SC.WebCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Service;

namespace YG.SC.WebCrawler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Guid jobkey = Guid.NewGuid();
            //for (int i = 1; i < 50000; i++)
            //{
            //    ShopRoomCrawler cra = new ShopRoomCrawler(i);
            //    var list = cra.Extract();
            //    YG.SC.Service.ShopRoomLogic logic = new YG.SC.Service.ShopRoomLogic();
            //    if (list.Count() > 0)
            //    {
            //        logic.AddList(list.ToList());
            //    }
            //}

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            ShopRoomLogic logic = new ShopRoomLogic();
            YG.SC.DataAccess.ShopRoom sr = new DataAccess.ShopRoom();
            sr.RName = "112";
            sr.AddTime = DateTime.Now;
            sr.ShopId = "122";

            logic.Add(sr);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
