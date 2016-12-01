using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Service.IService;

namespace YG.SC.OpenShop.Controllers
{
    public class BrandController : BaseController
    {
        private readonly ICustomerService _iCustomerService;
        private IShopBrandService _shopBrandService;
        private readonly IShopAttributesService _iShopAttributesServiceService;
        public BrandController(IShopBrandService shopBrandService, IShopAttributesService ShopAttributesServiceService, ICustomerService iCustomerService)
        {
            _shopBrandService = shopBrandService;
            _iShopAttributesServiceService = ShopAttributesServiceService;
            _iCustomerService = iCustomerService;
        }

        public ActionResult ApplyList(BrandApplyFilter filter)
        {
            if (filter == null)
            {
                filter = new BrandApplyFilter();
                filter.pg = 1;
            }
            filter.PageSize = Define.PAGE_SIZE;
            filter.ApplyUserId = UserId;
            ViewBag.StatusList = _shopBrandService.GetApplyStatusList();
            ViewBag.ApplyBrandList = _shopBrandService.GetApplyBrandList(filter);
            return View();
        }

        public ActionResult AuditList(BrandApplyFilter filter)
        {
            if (filter == null)
            {
                filter = new BrandApplyFilter();
                filter.pg = 1;
            }
            filter.PageSize = Define.PAGE_SIZE;
            filter.AuditUserId = UserId;
            ViewBag.StatusList = _shopBrandService.GetApplyStatusList();
            ViewBag.AuditBrandList = _shopBrandService.GetAuditBrandList(filter);
            return View();
        }

        public ActionResult Pass(int passApplyId)
        {
            _shopBrandService.PassApply(passApplyId);
            return RedirectToAction("AuditList");
        }
        public ActionResult Reject(int rejectApplyId, string rejectReason)
        {
            _shopBrandService.RejectApply(rejectApplyId, rejectReason);
            return RedirectToAction("AuditList");
        }
        #region 升级为品牌会员
        [UserAuthorize]
        public ActionResult Brand_add1()
        {
            ShopBrand moudel = new ShopBrand();
            var attrsTypes = this._iShopAttributesServiceService.GetListByAttributeId(9);
            ViewBag.shopType = attrsTypes;
            return View(moudel);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Brand_add1")]
        public ActionResult Brand_add1(YG.SC.DataAccess.ShopBrand shopBrand)
        {
            DataAccess.ShopBrand moudel = new DataAccess.ShopBrand();
            moudel.CreatTime = DateTime.Now;
            moudel.Recsts = 1;
            moudel.Name = shopBrand.Name;
            moudel.Abbreviation = shopBrand.Abbreviation;
            moudel.Url = shopBrand.Url;
            moudel.Address = shopBrand.Address;
            moudel.Rmark = shopBrand.Rmark;
            moudel.Logo = shopBrand.Logo;
            moudel.QRCode = shopBrand.QRCode;
            _shopBrandService.Insert(moudel);
            Customer customer = _iCustomerService.GetEntityById(UserId);
            customer.Companyid = moudel.Id;
            customer.Id = UserId;
            this._iCustomerService.Update(customer);
            //增加业态
            List<ShopBrandAttributeValues> listattr = new List<ShopBrandAttributeValues>();
            var attributesvalue = Request["selAttribute"].TrimEnd(',');
            if (!string.IsNullOrEmpty(attributesvalue))
            {
                foreach (var item in attributesvalue.Split(','))
                {
                    ShopBrandAttributeValues bv = new ShopBrandAttributeValues()
                    {
                        BrandId = moudel.Id,
                        Recsts = 1,
                        AttributesId = Convert.ToInt32(item.Split('-')[0]),
                        AttributeValuesId = Convert.ToInt32(item.Split('-')[1]),
                    };
                    listattr.Add(bv);
                }
            }
            this._iShopAttributesServiceService.InsertList(listattr);
            ViewBag.attributesvalue = Request["selAttribute"];
            return RedirectToAction("Brand_add2/" + moudel.Id);
        }
        [HttpGet]
        [UserAuthorize]
        public ActionResult Brand_add2(int Id = 0)
        {
            ShopBrandPhoto Photo = new ShopBrandPhoto();
            Photo.BrandId = Id;
            return View(Photo);
        }
        [HttpPost]
        [ActionName("Brand_add2")]
        public ActionResult Brand_add2img(ShopBrandPhoto BPhoto)
        {
            if (BPhoto.Id == 0)
            {
                BPhoto.Recsts = 1;
                this._shopBrandService.InsertPhoto(BPhoto);
            }
            else
            {
                ShopBrandPhoto model = this._shopBrandService.GetPhotoById(BPhoto.Id);
                model.Id = BPhoto.Id;
                model.title = BPhoto.title != null ? BPhoto.title : model.title;
                model.Rmark = BPhoto.Rmark != null ? BPhoto.Rmark : model.Rmark;
                model.Photo = BPhoto.Photo != null ? BPhoto.Photo : model.Photo;
                model.PhotoSmall = BPhoto.PhotoSmall != null ? BPhoto.PhotoSmall : model.PhotoSmall;
                model.PhotoSquare = BPhoto.PhotoSquare != null ? BPhoto.PhotoSquare : model.PhotoSquare;
                model.PhotoRectangle = BPhoto.PhotoRectangle != null ? BPhoto.PhotoRectangle : model.PhotoRectangle;
                model.Recsts = 1;
                this._shopBrandService.UpdatePhoto(model);
            }
            return RedirectToAction("Brand_add2/" + BPhoto.BrandId);
        }
        [UserAuthorize]
        public ActionResult Brand_add3(int Id = 0)
        {
            ShopBrand openshop = new ShopBrand();
            openshop.Id = Id;
            return View(openshop);
        }
        [HttpPost]
        [ActionName("Brand_add3")]
        public ActionResult Brand_add3UPdate(ShopBrand openshop)
        {
            DataAccess.ShopBrand moudel = this._shopBrandService.GetById(openshop.Id);
            moudel.Id = openshop.Id;
            moudel.BLS = openshop.BLS;
            moudel.CIC = openshop.CIC;
            this._shopBrandService.Update(moudel);
            return RedirectToAction("Brand_add4");
        }
        [UserAuthorize]
        public ActionResult Brand_add4()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult uploadPhoto(ShopBrandPhoto moudel)
        {
            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["BrandPhoto"], Server.MapPath(CommonContorllers.FileUploadBrandImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSmallImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSquareImgPath), Server.MapPath(CommonContorllers.FileUploadBrandRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                moudel.Photo = CommonContorllers.FileUploadBrandImgPath + fileName;
                moudel.PhotoSmall = CommonContorllers.FileUploadBrandSmallImgPath + fileName;
                moudel.PhotoSquare = CommonContorllers.FileUploadBrandSquareImgPath + fileName;
                moudel.PhotoRectangle = CommonContorllers.FileUploadBrandRectangleImgPath + fileName;
            }
            return View("Brand_add2", moudel);
        }
        [ValidateInput(false)]
        public ActionResult uploadimg(ShopBrand moudel)
        {
            string viewname = "";
            if (moudel.Id != 0)
            {
                viewname = "Brand_add3";
                var fileBLSName = UploadImgUtility.UploadImage(Request.Files["BLSfile"], Server.MapPath(CommonContorllers.FileUploadBrandBLSImgPath));
                if (!string.IsNullOrEmpty(fileBLSName))
                {
                    moudel.BLS = CommonContorllers.FileUploadBrandBLSImgPath + fileBLSName;
                }
                var fileCICName = UploadImgUtility.UploadImage(Request.Files["CICfile"], Server.MapPath(CommonContorllers.FileUploadBrandCICImgPath));
                if (!string.IsNullOrEmpty(fileCICName))
                {
                    moudel.CIC = CommonContorllers.FileUploadBrandCICImgPath + fileCICName;
                }
            }
            else
            {
                YG.SC.DataAccess.ShopBrand model = new DataAccess.ShopBrand();
                var attrsTypes = this._iShopAttributesServiceService.GetListByAttributeId(9);
                ViewBag.shopType = attrsTypes;

                var fileLogoName = UploadImgUtility.UploadImage(Request.Files["Logofile"], Server.MapPath(CommonContorllers.FileUploadBrandLogoImgPath));
                if (!string.IsNullOrEmpty(fileLogoName))
                {
                    moudel.Logo = CommonContorllers.FileUploadBrandLogoImgPath + fileLogoName;

                }
                var fileQRcodeName = UploadImgUtility.UploadImage(Request.Files["QRcodefile"], Server.MapPath(CommonContorllers.FileUploadBrandQRCodeImgPath));
                if (!string.IsNullOrEmpty(fileQRcodeName))
                {
                    moudel.QRCode = CommonContorllers.FileUploadBrandQRCodeImgPath + fileQRcodeName;
                }
                ViewBag.attributesvalue = Request["selAttribute"];
                viewname = "Brand_add1";
            }
            return View(viewname, moudel);
        }
        #endregion
        #region 品牌会员信息修改
        [UserAuthorize]
        public ActionResult Company_info(int id = 0)
        {
            var moudel = this._shopBrandService.GetById(id);
            return View(moudel);
        }
        [HttpPost]
        [ActionName("Company_info")]
        public ActionResult Company_infoEidt(DataAccess.ShopBrand openshop)
        {
            var moudel = this._shopBrandService.GetById(openshop.Id);
            moudel.Id = openshop.Id;
            moudel.Address = openshop.Address;
            moudel.Url = openshop.Url;
            moudel.Rmark = openshop.Rmark;
            moudel.Logo = openshop.Logo;
            moudel.QRCode = openshop.QRCode;
            this._shopBrandService.Update(moudel);
            return RedirectToAction("Company_info/" + moudel.Id);
        }
        [UserAuthorize]
        [ValidateInput(false)]
        public ActionResult Company_Case(int Id = 0)
        {
            ShopBrandPhoto Photo = new ShopBrandPhoto();
            Photo.BrandId = Id;
            return View(Photo);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Company_Case")]
        public ActionResult Company_Caseimg(ShopBrandPhoto BRPhoto)
        {

            if (BRPhoto.Id == 0)
            {
                BRPhoto.Recsts = 1;
                this._shopBrandService.InsertPhoto(BRPhoto);
            }
            else
            {
                ShopBrandPhoto model = this._shopBrandService.GetPhotoById(BRPhoto.Id);
                model.Id = BRPhoto.Id;
                model.title = BRPhoto.title != null ? BRPhoto.title : model.title;
                model.Rmark = BRPhoto.Rmark != null ? BRPhoto.Rmark : model.Rmark;
                model.Photo = BRPhoto.Photo != null ? BRPhoto.Photo : model.Photo;
                model.PhotoSmall = BRPhoto.PhotoSmall != null ? BRPhoto.PhotoSmall : model.PhotoSmall;
                model.PhotoSquare = BRPhoto.PhotoSquare != null ? BRPhoto.PhotoSquare : model.PhotoSquare;
                model.PhotoRectangle = BRPhoto.PhotoRectangle != null ? BRPhoto.PhotoRectangle : model.PhotoRectangle;
                model.Recsts = 1;
                this._shopBrandService.UpdatePhoto(model);
            }

            return RedirectToAction("Company_Case/" + BRPhoto.BrandId);
        }
        [UserAuthorize]
        public ActionResult Company_Qualifications(int Id = 0)
        {
            ShopBrand openshop = this._shopBrandService.GetById(Id);
            return View(openshop);
        }
        [HttpPost]
        [ActionName("Company_Qualifications")]
        public ActionResult Company_QualificationsUPdate(ShopBrand Brand)
        {
            ShopBrand moudel = this._shopBrandService.GetById(Brand.Id);
            moudel.Id = Brand.Id;
            moudel.BLS = Brand.BLS;
            moudel.CIC = Brand.CIC;
            this._shopBrandService.Update(moudel);
            return RedirectToAction("Company_Qualifications/" + moudel.Id);
        }
        [ValidateInput(false)]
        public ActionResult Company_infoimg(ShopBrand moudel)
        {
            string viewname = "";
            var fileLogoName = UploadImgUtility.UploadImage(Request.Files["Logofile"], Server.MapPath(CommonContorllers.FileUploadBrandLogoImgPath));
            if (!string.IsNullOrEmpty(fileLogoName))
            {
                moudel.Logo = CommonContorllers.FileUploadBrandLogoImgPath + fileLogoName;
                viewname = "Company_info";

            }
            var fileQRcodeName = UploadImgUtility.UploadImage(Request.Files["QRcodefile"], Server.MapPath(CommonContorllers.FileUploadBrandQRCodeImgPath));
            if (!string.IsNullOrEmpty(fileQRcodeName))
            {
                moudel.QRCode = CommonContorllers.FileUploadBrandQRCodeImgPath + fileQRcodeName;
                viewname = "Company_info";
            }

            var fileBLSName = UploadImgUtility.UploadImage(Request.Files["BLSfile"], Server.MapPath(CommonContorllers.FileUploadBrandBLSImgPath));
            if (!string.IsNullOrEmpty(fileBLSName))
            {
                moudel.BLS = CommonContorllers.FileUploadBrandBLSImgPath + fileBLSName;
                viewname = "Company_Qualifications";
            }
            var fileCICName = UploadImgUtility.UploadImage(Request.Files["CICfile"], Server.MapPath(CommonContorllers.FileUploadBrandCICImgPath));
            if (!string.IsNullOrEmpty(fileCICName))
            {
                moudel.CIC = CommonContorllers.FileUploadBrandCICImgPath + fileCICName;
                viewname = "Company_Qualifications";
            }
            return View(viewname, moudel);

        }
        [ValidateInput(false)]
        public ActionResult EditPhoto(ShopBrandPhoto moudel)
        {
            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["BrandPhoto"], Server.MapPath(CommonContorllers.FileUploadBrandImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSmallImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSquareImgPath), Server.MapPath(CommonContorllers.FileUploadBrandRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                moudel.Photo = CommonContorllers.FileUploadBrandImgPath + fileName;
                moudel.PhotoSmall = CommonContorllers.FileUploadBrandSmallImgPath + fileName;
                moudel.PhotoSquare = CommonContorllers.FileUploadBrandSquareImgPath + fileName;
                moudel.PhotoRectangle = CommonContorllers.FileUploadBrandRectangleImgPath + fileName;
            }
            return View("Company_Case", moudel);
        }
        #endregion
        public ActionResult CasePartial(int BrandId)
        {
            if (BrandId <= 0)
            {
                BrandId = -1;
            }
            var moudel = this._shopBrandService.GetPhotoByBrandId(1, BrandId, "");
            return View(moudel);
        }
        public bool validateAbbreviation(string Abbreviation)
        {
            return this._shopBrandService.GetEntitsByAbbreviation(Abbreviation.Trim());
        }
        public bool validateRename(string name)
        {
            return this._shopBrandService.GetEntitsByName(name.Trim());
        }
    }
}
