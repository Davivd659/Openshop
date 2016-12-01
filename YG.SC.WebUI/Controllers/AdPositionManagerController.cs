using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Model.AdPosition;
using YG.SC.Service.IService;
using YG.SC.WebUI.Models;
using YG.SC.WebUI.Models.AdPosition;
using YG.SC.WebUI.Views.AdPositionManager;

namespace YG.SC.WebUI.Controllers
{
    public class AdPositionManagerController : WebBaseController
    {
        //
        // GET: /AdPositionManager/
        private readonly IShopAdPositionService _iShopAdPositionService;
        private readonly IShopAdPositionDetailsService _iShopAdPositionDetailsService;
        private readonly IShopProjectService _iShopProjectService;
        private readonly IShopBrandService _IShopBrandService;
        private readonly IOpenShopService _iOpenShopService;

        public AdPositionManagerController(IShopAdPositionService iShopAdPositionService,
            IShopAdPositionDetailsService iShopAdPositionDetailsService,
            IShopBrandService iShopBrandService , IOpenShopService iOpenShopService,
            IShopProjectService iShopProjectService)
        {
            _iShopAdPositionService = iShopAdPositionService;
            _iShopAdPositionDetailsService = iShopAdPositionDetailsService;
            _iShopProjectService = iShopProjectService;
            _IShopBrandService = iShopBrandService;
            _iOpenShopService = iOpenShopService;
        }
       
        public ActionResult Index(int pg = 1, string AdName = "")
        {
            ViewBag.ShopProject = _iShopAdPositionService.GetAll();
            var model=new AdPositionModel();
            model.ShopAdPositionList=_iShopAdPositionService.GetEntitsByImageName(pg, AdName);
            model.ShopProjectList = _iShopAdPositionService.GetShopProjectByName(pg, AdName);
            return View(model);
        }
        /// <summary>
        /// 查询当月广告排期
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public ActionResult PositionIndex(YG.SC.WebUI.Models.AdPosition.PositonIndexModel position)
        {
            string PositonDate = "";
            if (position==null || position .PositionId ==0 ||position.PositonDate < new DateTime(2015,3,10) )
            {
                position = new PositonIndexModel();
                position.PositonDate = DateTime.Now;
                
                position.PositionId = 1;
                position.TypeId = 1; 
            } 
            PositonDate = position.PositonDate.ToString("yyyy-MM-dd");             

            ViewBag.PositionDetails = GetPositionDetailsList();
            ViewBag.TypeList = GetTypeList();

            ViewBag.TypeId = position.TypeId;
            ViewBag.PositonId = position.PositionId;
            ViewBag.PositonDate = PositonDate;

            var model = new PositonIndexModel();
            model.ViewModel = new PositonIndexViewModel();

            string innerHTML = "";
            string temp;
            DateTime time =position.PositonDate;
            DateTime start = new DateTime(time.Year, time.Month, 1);
            DateTime end = new DateTime(time.Year, time.Month, time.AddMonths(1).AddDays(-1).Day);
            string year = time.Year.ToString();
            string month = time.Month.ToString();

            int i = 0;
            switch (start.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    i = 0;
                    break;
                case DayOfWeek.Monday:
                    i = 1;
                    break;
                case DayOfWeek.Tuesday:
                    i = 2;
                    break;
                case DayOfWeek.Wednesday:
                    i = 3;
                    break;
                case DayOfWeek.Thursday:
                    i = 4;
                    break;
                case DayOfWeek.Friday:
                    i = 5;
                    break;
                case DayOfWeek.Saturday:
                    i = 6;
                    break;
            }
            List<PositionItem> itemList = new List<PositionItem>();
            DateTime DateTimeBegin = new DateTime(time.Year, time.Month, 1);
            DateTime DateTimeEnd =  new DateTime(time.Year, time.Month, time.AddMonths(1).AddDays(-1).Day);
            List<ShopAdPosition> PositionList = _iShopAdPositionService.SearchAdPosition(position.PositionId, position.TypeId, start, end);
            string SiteURL = ConfigurationManager.AppSettings["SiteURL"];
            for (int d = 1; d <= end.Day; d++)
            {
                innerHTML = "";
                temp = year + "-" + month + "-" + d.ToString();
                DateTime today = new DateTime(start.Year, start.Month, d);
                innerHTML += "<b>" + d.ToString() + "</b>";
                // innerhtml 
                var dayPositionList = PositionList.Where(m => m.StartDate == today).ToList();;
                // 单日内容显示
                string parStr = "/AdpositionManager/Add?PositionId=" + position.PositionId + "&TypeId=" + position.TypeId + "&PositonDate=" + today.ToString("yyyy-MM-dd");
                if (dayPositionList.Count == 0)
                {
                    innerHTML += "<br /><span style=\"color:red;\">无排期</span>";
                    innerHTML += "<br /><span style=\"color:red;\"><a href=\"" + parStr + "\" >添加排期</a></span>";
                }
                else
                {
                    int indexNum = 0;
                    foreach (var m in dayPositionList)
                    {
                        indexNum++;
                        string linkUrl = "";
                        
                        switch(position.TypeId) 
                        {
                            case 1: linkUrl = SiteURL + "/ShopBrand/brand?shopBrandId=" + m.KeyId; break;
                            case 2: linkUrl = SiteURL + "/OpenShop/OpenShop?openShopId=" + m.KeyId; break;
                            case 3: linkUrl = SiteURL + "/Project/index/" + m.KeyId; break;
                        }

                        innerHTML += "<br /><span style=\"color:blue;\"><a href=\"" + linkUrl + "\""+ " target='_blank' style='color: blue;'>" + indexNum + ". " + m.AdWords + "</a></span>";
                    }
                    innerHTML += "<br /><span style=\"color:blue;\"><a href=\"" + parStr + "\" >修改排期</a></span>";
                }
                // end day html 

                string key = "Td" + (i + d).ToString();
                itemList.Add(new PositionItem()
                {
                    Key = key,
                    Values = innerHTML
                });
            }
            ViewBag.ItemList = itemList;
            return View(model);
        }
        
        /// <summary>
        /// 获取项目类型，从枚举中取。
        /// </summary>
        /// <returns></returns>
        private static dynamic GetTypeList()
        {
            return (from EnumProjectType item in System.Enum.GetValues(typeof (EnumProjectType))
                select new SelectListItem()
                {
                    Value = Convert.ToInt32(item).ToString(), Text = item.ToString()
                }).ToList();
        }
        /// <summary>
        /// 广告显示位置
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetPositionDetailsList()
        {
            var list = _iShopAdPositionDetailsService.GetAll();
            var selectItems = list.Select(item => new SelectListItem() {Text = item.Details, Value = item.Id.ToString()}).ToList(); 
            return selectItems;
        }

        [HttpPost]
        public ActionResult AddAdPosition(FormCollection collection)
        {
            var typeid = collection["TypeId"];
            var position = collection["PositionId"];
            var keyWords = collection["KeyWords"];
            var startDateTime = collection["PositonDate"];
            var url = collection["Url"];
            //var Adpic = collection["AdPic"];
            var Adpic = string.Empty;
            var AdPicSmall = string.Empty;
            var fileAdPic = UploadImgUtility.UploadGoodsImg(Request.Files["AdPic"], Server.MapPath(CommonContorllers.FileUploadAdPicSmallImgPath), Server.MapPath(CommonContorllers.FileUploadAdPicImgPath));
            if (!string.IsNullOrEmpty(fileAdPic))
            {
                Adpic = CommonContorllers.FileUploadAdPicImgPath + fileAdPic;
                AdPicSmall = CommonContorllers.FileUploadAdPicSmallImgPath + fileAdPic;
            }
            
            // keysid 
            string strKeysId = collection["ckPartId"];
            string[] ArrayKeysId = strKeysId.Split(',');

            foreach (string keyid in ArrayKeysId)
            {
                var model = new ShopAdPosition();
                //var fileName = UploadImgUtility.UpLoadBannerImage(Request.Files["AdPic"], Server.MapPath(CommonContorllers.FileUploadProjectPhotoSmallPath), Server.MapPath(CommonContorllers.FileUploadProjectPhotoPath));
                //model.AdPic = CommonContorllers.FileUploadProjectPhotoPath + fileName;
                model.TypesId = int.Parse(typeid);
                model.PositionId = int.Parse(position);
                model.KeyId = int.Parse(keyid);
                model.StartDate = DateTime.Parse(startDateTime);
                model.Url = url;
                model.AdPic = Adpic;
                model.AdPicSmall = AdPicSmall;
                model.Status = 1;
                model.Sort = 1;
                model.AdWords = GetNameById(model.KeyId ?? 0, model.TypesId);
                _iShopAdPositionService.Insert(model);
            }

            return RedirectToAction("Add", new { TypeId  = typeid , PositionId = position,  PositonDate = startDateTime });
        }

        [HttpPost]
        public JsonResult DeletePosition(int Id)
        {
            _iShopAdPositionService.DeleteById(Id);

            var model = new ResultModel(true, string.Empty);
            return Json(model);
        }

// <option value="1">品牌</option>
//<option selected="selected" value="2">选址</option>
//<option value="3">装修</option>
        private string GetNameById(int id, int TypeId)
        {
            string key = "";
            if (TypeId == 1)
            {
                var brand = _IShopBrandService.GetById(id);
                if (brand != null)
                {
                    return brand.Name;
                }
            }
            else if (TypeId == 2)
            {
                var project = _iShopProjectService.GetById(id);
                if (project != null)
                {
                    return project.NAME;
                }
            }
            else
            {
                var openshop = _iOpenShopService.GetById(id);
                if (openshop != null)
                {
                    return openshop.Name;
                }
            }
            return key;
        }

        public ActionResult Add(AdPositionModel model)
        {
            string PositonDate = "";
            if (model == null || model.PositionId == 0 || model.PositonDate < new DateTime(2015, 3, 10))
            {
                model = new AdPositionModel();
                model.PositonDate = DateTime.Now;
                model.PositionId = 1;
                model.TypeId = 1;
            }
            PositonDate = model.PositonDate.ToString("yyyy-MM-dd");

            ViewBag.TypeId = model.TypeId;
            ViewBag.PositionId = model.PositionId;
            ViewBag.PositonDate = PositonDate;

            ViewBag.PositionDetails = GetPositionDetailsList();
            ViewBag.TypeList = GetTypeList();

            //var model = new AdPositionModel();
            model.ShopAdPositionList = _iShopAdPositionService.GetEntitsByImageName(1, null);
            model.ShopProjectList = _iShopAdPositionService.GetShopProjectByName(1, null);

            return View(model);
        }
    
        /// <summary>
        /// 查询当天已添加广告信息
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="PositionId"></param>
        /// <param name="PositonDate"></param>
        /// <returns></returns>
        public PartialViewResult AddPositionDay(AdPositionModel model)
        {
            int typeid = model.TypeId;
            int positionId = model.PositionId;
            // string positionDate= model.PositonDate.ToString("yyyy-MM-dd") ;
            var ShopAdPositonList = _iShopAdPositionService.SearchAdPosition(positionId, typeid, model.PositonDate, model.PositonDate).OrderBy(m=> m.Sort).ToList();
            var PositionDayItem = Mapper.Map<List<ShopAdPosition>, List<AddPositionDayItem>>(ShopAdPositonList);
            ViewBag.PositionList = PositionDayItem;
            return PartialView("_AddPositionDay");
        }
        /// <summary>
        /// 根据日期查询数据
        /// </summary>
        /// <param name="TypeId">1：品牌， 2 选址，3：装修</param>
        /// <param name="Keys"></param>
        /// <param name="pg"></param>
        /// <returns></returns>
        public PartialViewResult AddPositionSearch(int TypeId, string Keys, int pg)
        {
            PagerEntity pager = new PagerEntity();
            AdPositionSearchPartial[] list = { }; 
            switch (TypeId)
            {
                case 1: 
                    //品牌 brandk 
                    var brandList = _IShopBrandService.GetEntitsByName(pg, 0, Keys);
                    list = Mapper.Map<ShopBrand[], AdPositionSearchPartial[]>(brandList.Item1);
                    pager = brandList.Item2;
                    break;
                case 2:
                    // 选址 projec
                    var ProjectList = _iShopProjectService.GetEntitsByShopProjectMainName(pg, Keys);
                    list = Mapper.Map<ShopProject[], AdPositionSearchPartial[]>(ProjectList.Item1);
                    pager = ProjectList.Item2;
                    break;
                case 3:
                    // 装修\开店
                    var openShop = _iOpenShopService.GetEntitsByName(pg, 0, Keys);
                    list = Mapper.Map<OpenShop[], AdPositionSearchPartial[]>(openShop.Item1);
                    pager = openShop.Item2;
                    break;
            }
            Tuple<AdPositionSearchPartial[], Model.PagerEntity> part = Tuple.Create(list, pager);
            return PartialView("_AddPositionSearch", part);
        }

        public ActionResult Delete(int id, string state)
        {
            var entity = _iShopAdPositionService.GetById(id); 
            entity.Status = 0;
            _iShopAdPositionService.Update(entity);
            return RedirectToAction("Index");
        }
    }
}
