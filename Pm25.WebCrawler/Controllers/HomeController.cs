using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Pm25.WebCrawler.Controllers
{
    using Pm25.WebCrawler.Common;
    using System.Text.RegularExpressions;
    using System.Collections;

    public class HomeController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {

            ShopRoomCrawler cra = new ShopRoomCrawler();
            var list = cra.Extract();
            YG.SC.Service.ShopRoomLogic logic = new YG.SC.Service.ShopRoomLogic();

            foreach (var m in list)
            {
                logic.Add(m);
            }
            return View();
        }


    }
}
