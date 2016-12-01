using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Service.IService;

namespace YG.SC.WebUI.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private readonly ICustomerService _iCustomerService;
        private readonly IApplyActiviteService _iApplyActiviteService;
        public AccountController(ICustomerService iCustomerService,IApplyActiviteService iApplyActiviteService)
        {
            _iCustomerService = iCustomerService;
            _iApplyActiviteService = iApplyActiviteService;
        }

        public ActionResult Index(int pg = 1, string txtName = "")
        {
            ViewBag.ShopProject = _iCustomerService.GetAll();
            var model = _iCustomerService.GetEntitsByName(pg, txtName);
            return View(model);
        }

        /// <summary>
        /// 团购报名
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="txtName"></param>
        /// <returns></returns>
        public ActionResult GrouponIndex(int pg = 1, string txtName = "")
        {
            ViewBag.ShopProject = _iApplyActiviteService.GetAll();
            var model = _iApplyActiviteService.GetEntitsByImageName(pg, txtName);
            return View(model);
        }
    }
}
