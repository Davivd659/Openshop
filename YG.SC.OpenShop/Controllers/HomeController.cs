using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.Model;
using YG.SC.OpenShop.Models;
using YG.SC.Service.IService;

namespace YG.SC.OpenShop.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly ILinkService _ilinkService;
        private readonly IShopProjectService _iShopProjectService;
        private readonly IShopAdPositionService _iShopAdPositionService;
        public HomeController(ILinkService ilinkService, IShopProjectService iShopProjectService, IShopAdPositionService iShopAdPositionService)
        {
            _ilinkService = ilinkService;
            _iShopProjectService = iShopProjectService;
            _iShopAdPositionService = iShopAdPositionService;
        }

        //[OutputCache]
        public ActionResult Index()
        {
            ViewBag.LinkTypes = this._ilinkService.GetAll();
            DateTime Date = DateTime.Now;
            var ad_xuanzhi = _iShopAdPositionService.SearchAdPosition((int)PositionCode.首页我要选址, (int)EnumProjectType.选址, Date.AddYears(-1), Date);
            var ad_kaidian = _iShopAdPositionService.SearchAdPosition((int)PositionCode.首页开店帮, (int)EnumProjectType.装修, Date.AddYears(-1), Date);
            var Brand = _iShopAdPositionService.SearchAdPosition((int)PositionCode.首页品牌街, (int)EnumProjectType.品牌, Date.AddYears(-1), Date);

            var model = new HomeIndexModel();
            model.AdXuanZhi = ad_xuanzhi.Take(8).ToList();
            model.ad_kaidian = ad_kaidian.Take(8).ToList();
            model.AdBrand = Brand.Take(8).ToList();
            var projectList = _iShopProjectService.GetTop(4);
            ViewBag.projectList = projectList;
            if (UserContext.Current.Id > 0)
            {
                ViewBag.UserId = UserContext.Current.Id;
            }

            return View(model);
        }
        public ActionResult ceshi()
        {
            return View();
        }

    }
}
