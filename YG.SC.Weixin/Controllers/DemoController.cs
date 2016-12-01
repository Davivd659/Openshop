using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using YG.SC.Service;
using YG.SC.Model;
using YG.SC.DataAccess;
using YG.SC.Service.IService ;


namespace YG.SC.Weixin.Controllers
{
    public class DemoController : Controller
    {
        private readonly IShopDemoService _IshopDemoService;

        public DemoController(IShopDemoService shopDemoService)
        {
            _IshopDemoService = shopDemoService;

        }
        protected override void Dispose(bool disposing)
        {
            this._IshopDemoService.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /Demo/

        public ActionResult Index()
        {
            var demo = _IshopDemoService.GetById(2);
            // viewbag . 
            ViewBag.demo = demo;

            return View();
        }

    }
}
