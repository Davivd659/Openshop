using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Core.Lifetime;
using AutoMapper;
using YG.SC.Common;
using YG.SC.Common.AttributeExtention;
using YG.SC.DataAccess;
using YG.SC.Model.Project;
using YG.SC.Service.IService;

namespace YG.SC.WebUI.Controllers
{
    /// <summary>
    /// 项目管理列表
    /// </summary>
    public class ProjectManagerController : WebBaseController
    {
        private const int IndustryId  = 9;// 行业
        private const int StateId = 10;// 出租、出售
        private const int WuyeTypeId = 1;// 物业类型
        private const int SubWayId = 8;// 地铁
        //
        // GET: /ProjectManager/
        private readonly IShopProjectService _IShopProjectService;
        private readonly IProjectPhotoService _IProjectPhotoService;
        private readonly IShopAttributesService _iShopAttributesService;
        public ProjectManagerController(IProjectPhotoService iProjectPhotoService, IShopProjectService iShopProjectService, IShopAttributesService iShopAttributesService)
        {
            _IProjectPhotoService = iProjectPhotoService;
            _IShopProjectService = iShopProjectService;
            _iShopAttributesService = iShopAttributesService;
        }
        protected override void Dispose(bool disposing)
        {
            this._IProjectPhotoService.Dispose();
            this._IShopProjectService.Dispose();
            this._iShopAttributesService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index(int pg = 1, string txtProjectName = "")
        {
            ViewBag.ShopProject = _IShopProjectService.GetAll();
            var model = _IShopProjectService.GetEntitsByImageName(pg, txtProjectName);
            return View(model);
        }

        public ActionResult Add()
        {
            List<CodeDescription> codes=new List<CodeDescription>();
            var shopAttributes = _iShopAttributesService.GetAll();
            if (shopAttributes != null)
            {
                foreach (var item in shopAttributes)
                {
                    var attributeValues=_iShopAttributesService.GetListByAttributeId(item.Id);
                    if (attributeValues != null)
                    {
                        foreach (var tmp in attributeValues)
                        {
                            codes.Add(new CodeDescription(tmp.Id.ToString(), tmp.ValueStr, item.AttributeName));
                        }
                    }
                }
            }
            CodeManager.codes = codes.ToArray();
            return View();
        }

        public ActionResult AddProject(FormCollection collection)
        {
            var fileName = UploadImgUtility.UpLoadBannerImage(Request.Files["CoverPhoto"], Server.MapPath(CommonContorllers.FileUploadProjectCoverPhotoSmallPath), Server.MapPath(CommonContorllers.FileUploadProjectCoverPhotoPath));
       
            ShopProjectMain shopProjectMain;
            var model = ShopProject(collection, null, null, out shopProjectMain);
            model.CoverPhoto = CommonContorllers.FileUploadProjectCoverPhotoPath + fileName;
            _IShopProjectService.Insert(model);
           
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 保存提交的修改
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult EditProject(FormCollection collection)
        {
            var fileName = UploadImgUtility.UpLoadBannerImage(Request.Files["CoverPhoto"], Server.MapPath(CommonContorllers.FileUploadProjectCoverPhotoSmallPath), Server.MapPath(CommonContorllers.FileUploadProjectCoverPhotoPath));

            var id = collection["Id"];
            var projectMainId = collection["ProjectMainId"];
            ShopProjectMain shopProjectMain;

            var model = ShopProject(collection, projectMainId, id, out shopProjectMain);
            if (!string.IsNullOrEmpty(fileName))
            {
                model.CoverPhoto = CommonContorllers.FileUploadProjectCoverPhotoPath + fileName;
            }
            else
            {
                model.CoverPhoto = collection["OldCoverPhoto"];
            }
            _IShopProjectService.Update(shopProjectMain);
            model.Id = Convert.ToInt32(id);
            _IShopProjectService.Update(model);
            return RedirectToAction("Index");
        }

        private ShopProject ShopProject(FormCollection collection, string projectMainId, string id,
            out ShopProjectMain shopProjectMain)
        {
            #region 主表

            var trade = collection["Trade"]; //行业
            var region = collection["Region"]; //区域
            var tradingArea = collection["TradingArea"]; //商圈
            // var circleLine = collection["CircleLine"]; //环线
            var salePrice = collection["SalePrice"]; //售价
            var rentalPrice = collection["RentalPrice"]; //租价
            var metro = collection["Metro"]; //地铁
            var state = collection["State"]; //状态
            var openDate = collection["OpenDate"]; //开盘时间
            var TypeOfOperationId = collection["TypeOfOperation"]; //物业类型

            #endregion
            var name = collection["Name"];
            var addressLine = collection["AddressLine"];
            var tuanDuiInfo = collection["TuanDuiInfo"];
            var ProjectIntro = collection["ProjectIntro"];
            var Listings = collection["Listings"];
            var teSe = collection["ProjectFeature"];
            var youShi = collection["ProjectAdvantage"];
            var quanShu = collection["OwnerShip"];
            var waiLiMian = collection["Facade"];
            var jieGou = collection["Architecture"];
            var zongJianZhuMianJi = collection["TotalFloorArea"];
            var zongCengShu = collection["TotalLayerNum"];
            var cengGao = collection["SignalLayerHeight"];
            var cengMianJi = collection["SignalLayerArea"];
            var zhuLiHuXing = collection["MainUnit"];
            var winOpen = collection["IsWinOpen"];
            var chengZhongZhu = collection["KingTowerPosition"];
            var heXinTong = collection["CoreTubeLayout"];
            var heiFangJian = collection["BlackArea"];
            var liftBrand = collection["ElevatorBrand"];
            var liftIntro = "";// collection[""];
            var liftFenQu = collection["IsPartitionControl"];
            var liftSpeed = collection["ElevatorSpeed"];
            var airConditoningBrand = collection["AirConditionBrand"];
            var airConditoningControl = collection["CenterAirCoolWay"];
            var prkingSpace = collection["ParkingPlace"];
            var teaIntroduction = collection["PassengerElevatorNum"]; //待定
            var ContactPhone = collection["ContactPhone"];
            var lat = collection["Lat"];
            var lng = collection["Long"];


            var model = new ShopProject();
            model.NAME = name;
            model.AddressLine = addressLine;
            model.BJieGou = jieGou;
            model.BQuanShu = quanShu;
            model.BWaiLiMian = waiLiMian;
            model.JChengGao = Convert.ToDecimal(cengGao);
            model.JZongChengShu = Convert.ToInt16(zongCengShu);
            model.JZongJianZhuMianJi = Convert.ToDecimal(zongJianZhuMianJi);
            decimal _lat = 0;
            decimal _lng = 0;
            decimal.TryParse(lat, out _lat);
            decimal.TryParse(lng, out _lng);
            model.Lat = _lat;
            model.Long = _lng;
            model.PChengMianJi = Convert.ToDecimal(cengMianJi);
            model.PChengZhongZhu = chengZhongZhu;
            model.PHeXinTong = heXinTong;
            model.PHeiFangJian = heiFangJian;
            model.PWindowOpen = winOpen.Contains("true") ? "true" : "false";
            model.PZhuLiHuxing = zhuLiHuXing;
            model.TeShe = teSe;
            model.TeaIntroduction = teaIntroduction;
            model.YAirConditoningBrand = airConditoningBrand;
            model.YAirConditoningControl = airConditoningControl;
            model.YLiftBrand = liftBrand;
            model.YLiftFenQu = liftFenQu.Contains("true") ? "true" : "false";
            model.YLiftIntro = liftIntro;
            model.YLiftSpeed = liftSpeed;
            model.YPrkingSpace = prkingSpace;
            model.YouShi = youShi;
            model.TuanDuiInfo = tuanDuiInfo;
            model.Listings = Listings;
            model.ProjectIntro = ProjectIntro;
            model.Status = 1;
            model.ContactPhone = ContactPhone;

            shopProjectMain = new ShopProjectMain();
            if (projectMainId != null) { 
                shopProjectMain.Id = Convert.ToInt32(projectMainId);
            }
            // shopProjectMain.CircleLineId = Convert.ToInt32(circleLine);
            shopProjectMain.CreatTime = DateTime.Now;
            shopProjectMain.MetroId = metro;
            DateTime dtOpenTime = new DateTime();
            bool boolOpenTime = DateTime.TryParse(openDate, out dtOpenTime);
            if (boolOpenTime)
            {
                shopProjectMain.OpenDate = dtOpenTime;
            }
            shopProjectMain.Recsts = 1;
            shopProjectMain.RegionId = Convert.ToInt32(region);
            shopProjectMain.RentalPrice = !string.IsNullOrEmpty(rentalPrice) ? Convert.ToDecimal(rentalPrice) : 0;
            shopProjectMain.SalePrice = !string.IsNullOrEmpty(salePrice) ? Convert.ToDecimal(salePrice) : 0;
            shopProjectMain.StateId = (state);// 出租 ，出售 
            shopProjectMain.TradeId = trade;
            shopProjectMain.TradingAreaId = Convert.ToInt32(tradingArea);
            if (id != null)
            {
                int projectId =Convert.ToInt32(id);
                shopProjectMain.ShopProjectId = projectId;
                // 行业ID
                _IShopProjectService.UpdateProjectAttributes(IndustryId,trade,projectId);
                // 出租、出售
                _IShopProjectService.UpdateProjectAttributes(StateId, state, projectId);
                //  物业类型
                _IShopProjectService.UpdateProjectAttributes(WuyeTypeId, TypeOfOperationId, projectId);
                // 地铁
                _IShopProjectService.UpdateProjectAttributes(SubWayId, metro, projectId);
            }
            shopProjectMain.TypeOfOperationId =(TypeOfOperationId);
            model.ShopProjectMain.Add(shopProjectMain);
            return model;
        }

        public ActionResult Edit(int id)
        {
            var codes = new List<CodeDescription>();
            var shopAttributes = _iShopAttributesService.GetAll();
            if (shopAttributes != null)
            {
                foreach (var item in shopAttributes)
                {
                    var attributeValues = _iShopAttributesService.GetListByAttributeId(item.Id);
                    if (attributeValues != null)
                    {
                        codes.AddRange(attributeValues.Select(tmp => new CodeDescription(tmp.Id.ToString(), tmp.ValueStr, item.AttributeName)));
                    }
                }
            }
            CodeManager.codes = codes.ToArray();

            ViewBag.ShopProject = _IShopProjectService.GetAll();
            var shopProject = _IShopProjectService.GetById(id);
            var model = Mapper.Map<ShopProject, ProjectViewModel>(shopProject);
            return View(model);
        }

        public ActionResult Delete(int id, string state)
        {
            var entity = _IShopProjectService.GetById(id);
            entity.Status = 0;
            _IShopProjectService.Update(entity);
            
            return RedirectToAction("Index");
        }

        public ActionResult SetMap(int id)
        {
            var shopProject = _IShopProjectService.GetById(id);
            var model = Mapper.Map<ShopProject, ProjectViewModel>(shopProject);
            return View(model);
        }

        [HttpPost]
        [ActionName("SetMap")]
        public ActionResult SetMapSave(ProjectViewModel model)
        {
            var shopProject = _IShopProjectService.GetById(model.Id);
            shopProject.Lat = (decimal)model.Lat;
            shopProject.Long = (decimal)model.Long;

            _IShopProjectService.Update(shopProject);

            return RedirectToAction("Index");
        }

    }
}
