using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.Model;
using YG.SC.WeiXin.Models;
using YG.SC.Service.IService;
using AutoMapper;

namespace YG.SC.WeiXin.Controllers
{
    public class HomeController : BaseController
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


        public ActionResult Index()
        {
            

            return View();
        }

        //[OutputCache]
        public ActionResult Index2()
        {
            ViewBag.LinkTypes = this._ilinkService.GetAll();
            DateTime Date = DateTime.Now;
            var ad_haodian = _iShopAdPositionService.SearchAdPosition((int)PositionCode.首页与好点相知相遇, (int)EnumProjectType.选址, Date);
            var ad_xuanzhi = _iShopAdPositionService.SearchAdPosition((int)PositionCode.首页我要选址, (int)EnumProjectType.选址, Date);
            var ad_kaidian = _iShopAdPositionService.SearchAdPosition((int)PositionCode.首页开店帮, (int)EnumProjectType.装修, Date);

            var model = new HomeIndexModel();
            model.AdHaoDian = ad_haodian.Take(3).ToList();
            model.AdXuanZhi = ad_xuanzhi.Take(4).ToList();
            model.AdKaiDianLeft = ad_kaidian.Take(2).ToList();
            model.AdKaiDianRight = ad_kaidian.Skip(2).Take(2).ToList();
            var projectList = _iShopProjectService.GetTop(4);
            ViewBag.projectList = projectList;
            ViewBag.UserId = UserId;

            //YG.SC.Model.Project.ProjectDetailsViewModel[]
            // 取广告展示的项目详细信息
            List<DataAccess.ShopProject> ad_haodian_project_list = new List<DataAccess.ShopProject>();
            foreach (var AdItem in model.AdHaoDian)
            {
                DataAccess.ShopProject proj_item = new DataAccess.ShopProject();
                proj_item = _iShopProjectService.GetById(AdItem.KeyId ?? 0);
                if (proj_item != null)
                {
                    ad_haodian_project_list.Add(proj_item);
                }
            }

            var haodian_viewmodel = Mapper.Map<List<DataAccess.ShopProject>, List<YG.SC.Model.Project.ProjectDetailsViewModel>>(ad_haodian_project_list);

            ViewBag.haodian_viewmodel = haodian_viewmodel;

            return View(model);
        }

    }
}
