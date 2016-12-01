using LitJson;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.OpenShop.Authentication;
using YG.SC.Service;
using YG.SC.Service.IService;

namespace YG.SC.OpenShop.Controllers
{
    public class MerchantController : Controller
    {
        //
        // GET: /Merchant/
        private static int _openShopId;
        private readonly ICustomerService _iCustomerService;
        private readonly IOpenShopService _iOpenShopService;
        private readonly IShopAttributesService _iShopAttributesService;
        private readonly IMessageService _messageService;
        private readonly IMissionService _missionService;
        private readonly IHotareaService _hotareaService;
        private readonly IObjectService _objectService;
        public MerchantController(IOpenShopService iOpenShopService, IShopAttributesService iShopAttributesServiceService, ICustomerService iCustomerService, IMessageService messageService,
            IMissionService missionService, IHotareaService hotareaService, IObjectService objectService)
        {
            _iShopAttributesService = iShopAttributesServiceService;
            _iOpenShopService = iOpenShopService;
            _iCustomerService = iCustomerService;
            _messageService = messageService;
            _missionService = missionService;
            _hotareaService = hotareaService;
            _objectService = objectService;
        }
        protected override void Dispose(bool disposing)
        {
            this._iOpenShopService.Dispose();
            _iShopAttributesService.Dispose();
            _iCustomerService.Dispose();
            _hotareaService.Dispose();
            _messageService.Dispose();
            _objectService.Dispose();
            base.Dispose(disposing);
        }
        int OpenShopAttrId = 20;
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var entity = this._iOpenShopService.GetById(id);
            return View(entity);
        }


        [HttpPost]
        [ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult EditPost(YG.SC.DataAccess.OpenShop shopBrand)
        {
            var entity = this._iOpenShopService.GetById(shopBrand.Id);

            var fileLogoName = UploadImgUtility.UploadImage(Request.Files["LogoImg"], Server.MapPath(CommonContorllers.FileUploadOpenShopLogoImgPath));
            if (!string.IsNullOrEmpty(fileLogoName))
            {
                entity.Logo = CommonContorllers.FileUploadOpenShopLogoImgPath + fileLogoName;
            }
            entity.Name = shopBrand.Name;
            entity.Rmark = shopBrand.Rmark;
            entity.Url = shopBrand.Url;
            entity.VidoUrl = shopBrand.VidoUrl;
            this._iOpenShopService.Update(entity);
            string msg = "修改完成";
            ViewBag.msg = msg;
            return Redirect("~/Merchant/Edit?id=" + entity.Id);
        }
        #region 商家注册

        public ActionResult OpenShop_add1()
        {
            var attrsTypes = this._iShopAttributesService.GetListByAttributeId(OpenShopAttrId);
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.ValueStr,
                                Value = m.Id.ToString()
                            }).ToList();
            ViewBag.shopType = typelist;

            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.ParentId = -1;
            var rangelist = this._objectService.SearchCategory(SearchCriteria).Item1;
            var range = (from m in rangelist
                            select new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.Id.ToString()
                            }).ToList();
            ViewBag.range = range;
            var Districtlist = this._hotareaService.GetByParentId(1);
            var District = (from m in Districtlist
                            select new SelectListItem
                            {
                                Text = m.name,
                                Value = m.id.ToString()
                            }).ToList();
            ViewBag.District = District;
            OpenShopViewModel moudel = new OpenShopViewModel();
            return View(moudel);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ActionName("OpenShop_add1")]
        public ActionResult OpenShop_add1(OpenShopViewModel openShop)
        {

            var attrsTypes = this._iShopAttributesService.GetListByAttributeId(OpenShopAttrId);
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.ValueStr,
                                Value = m.Id.ToString()
                            }).ToList();
            ViewBag.shopType = typelist;
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.ParentId = -1;
            var rangelist = this._objectService.SearchCategory(SearchCriteria).Item1;
            var range = (from m in rangelist
                         select new SelectListItem
                         {
                             Text = m.Name,
                             Value = m.Id.ToString()
                         }).ToList();
            ViewBag.range = range;
            var Districtlist = this._hotareaService.GetByParentId(1);
            var District = (from m in Districtlist
                            select new SelectListItem
                            {
                                Text = m.name,
                                Value = m.id.ToString()
                            }).ToList();
            ViewBag.District = District;
            DataAccess.OpenShop moudel = new DataAccess.OpenShop();
            moudel.CreateTime = DateTime.Now;
            moudel.Recsts = 1;
            moudel.Name = openShop.Name;
            moudel.Abbreviation = openShop.Abbreviation;
            moudel.Type = openShop.Type != null ? openShop.Type.Value : 0;
            moudel.Url = openShop.Url;
            moudel.Address = openShop.Address;
            moudel.Introduction = openShop.Introduction;
            moudel.Logo = openShop.Logo;
            moudel.QRcode = openShop.QRcode;
            moudel.Districtid = openShop.Districtid;
            moudel.Rangeid = openShop.Rangeid;
            this._iOpenShopService.Insert(moudel);
            Customer customer = new Customer();
            customer.Name = openShop.Customer;
            customer.LoginName = openShop.Name;
            customer.Mobile = openShop.Mobile;
            customer.Password = openShop.Password.Trim();
            customer.Property = true;
            customer.registertime = DateTime.Now;
            customer.GroupId = (int)CommonEnum.GroupOfCustomer.OpenShop;
            customer.Online = true;
            customer.Companyid = moudel.Id;
            _openShopId = moudel.Id;
            this._iCustomerService.Insert(customer);
            WriteUserSession(customer, "");
            return RedirectToAction("OpenShop_add2/" + moudel.Id);
        }
        [HttpGet]
        public ActionResult OpenShop_add2(int Id = 0)
        {
            OpenShopPhoto Photo = new OpenShopPhoto();
            Photo.OpenShopId = Id;
            return View(Photo);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ActionName("OpenShop_add2")]
        public ActionResult OpenShop_add2img(OpenShopPhoto OPPhoto)
        {

            if (OPPhoto.Id == 0)
            {
                this._iOpenShopService.InsertPhoto(OPPhoto);
            }
            else
            {
                OpenShopPhoto model = this._iOpenShopService.GetPhotoById(OPPhoto.Id);
                model.Id = OPPhoto.Id;
                model.title = OPPhoto.title != null ? OPPhoto.title : model.title;
                model.Rmark = OPPhoto.Rmark != null ? OPPhoto.Rmark : model.Rmark;
                model.Photo = OPPhoto.Photo != null ? OPPhoto.Photo : model.Photo;
                model.PhotoSmall = OPPhoto.PhotoSmall != null ? OPPhoto.PhotoSmall : model.PhotoSmall;
                model.PhotoSquare = OPPhoto.PhotoSquare != null ? OPPhoto.PhotoSquare : model.PhotoSquare;
                model.PhotoRectangle = OPPhoto.PhotoRectangle != null ? OPPhoto.PhotoRectangle : model.PhotoRectangle;
                this._iOpenShopService.UpdatePhoto(model);
            }

            return RedirectToAction("OpenShop_add2/" + OPPhoto.OpenShopId);
        }
        public ActionResult OpenShop_add3(int Id = 0)
        {
            OpenShopViewModel openshop = new OpenShopViewModel();
            openshop.Id = Id;
            return View(openshop);
        }
        [HttpPost]
        [ActionName("OpenShop_add3")]
        public ActionResult OpenShop_add3UPdate(OpenShopViewModel openshop)
        {
            DataAccess.OpenShop moudel = this._iOpenShopService.GetById(openshop.Id);
            moudel.Id = openshop.Id;
            moudel.BLS = openshop.BLS;
            moudel.CIC = openshop.CIC;
            this._iOpenShopService.Update(moudel);
            return RedirectToAction("OpenShop_add4");
        }

        public ActionResult OpenShop_add4()
        {
            return View();
        }
        public ActionResult uploadPhoto(OpenShopPhoto moudel)
        {
            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["OpenShopPhoto"], Server.MapPath(CommonContorllers.FileUploadOpenShopImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSmallImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSquareImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                moudel.Photo = CommonContorllers.FileUploadOpenShopImgPath + fileName;
                moudel.PhotoSmall = CommonContorllers.FileUploadOpenShopSmallImgPath + fileName;
                moudel.PhotoSquare = CommonContorllers.FileUploadOpenShopSquareImgPath + fileName;
                moudel.PhotoRectangle = CommonContorllers.FileUploadOpenShopRectangleImgPath + fileName;
            }
            return View("OpenShop_add2", moudel);
        }
        [ValidateInput(false)]
        public ActionResult uploadimg(OpenShopViewModel moudel)
        {
            string viewname = "";
            if (moudel.Id != 0)
            {
                viewname = "OpenShop_add3";
                var fileBLSName = UploadImgUtility.UploadImage(Request.Files["BLSfile"], Server.MapPath(CommonContorllers.FileUploadOpenShopBLSImgPath));
                if (!string.IsNullOrEmpty(fileBLSName))
                {
                    moudel.BLS = CommonContorllers.FileUploadOpenShopBLSImgPath + fileBLSName;
                }
                var fileCICName = UploadImgUtility.UploadImage(Request.Files["CICfile"], Server.MapPath(CommonContorllers.FileUploadOpenShopCICImgPath));
                if (!string.IsNullOrEmpty(fileCICName))
                {
                    moudel.CIC = CommonContorllers.FileUploadOpenShopCICImgPath + fileCICName;
                }
            }
            else
            {
                YG.SC.DataAccess.OpenShop model = new DataAccess.OpenShop();
                var attrsTypes = this._iShopAttributesService.GetListByAttributeId(OpenShopAttrId);
                var typelist = (from m in attrsTypes
                                select new SelectListItem
                                {
                                    Text = m.ValueStr,
                                    Value = m.Id.ToString()
                                }).ToList();
                ViewBag.shopType = typelist;
                CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
                SearchCriteria.ParentId = -1;
                var rangelist = this._objectService.SearchCategory(SearchCriteria).Item1;
                var range = (from m in rangelist
                             select new SelectListItem
                             {
                                 Text = m.Name,
                                 Value = m.Id.ToString()
                             }).ToList();
                ViewBag.range = range;
                var Districtlist = this._hotareaService.GetByParentId(1);
                var District = (from m in Districtlist
                                select new SelectListItem
                                {
                                    Text = m.name,
                                    Value = m.id.ToString()
                                }).ToList();
                ViewBag.District = District;
                var fileLogoName = UploadImgUtility.UploadImage(Request.Files["Logofile"], Server.MapPath(CommonContorllers.FileUploadOpenShopLogoImgPath));
                if (!string.IsNullOrEmpty(fileLogoName))
                {
                    moudel.Logo = CommonContorllers.FileUploadOpenShopLogoImgPath + fileLogoName;

                }
                var fileQRcodeName = UploadImgUtility.UploadImage(Request.Files["QRcodefile"], Server.MapPath(CommonContorllers.FileUploadOpenShopQRCodeImgPath));
                if (!string.IsNullOrEmpty(fileQRcodeName))
                {
                    moudel.QRcode = CommonContorllers.FileUploadOpenShopQRCodeImgPath + fileQRcodeName;
                }
                ViewBag.quyu = Request.Form["District"];
                viewname = "OpenShop_add1";
            }
            return View(viewname, moudel);
        }
        public JsonResult GetNodelist(int id)
        {
            var attrsTypes =_hotareaService.GetByParentId(id);
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.name,
                                Value = m.id.ToString()
                            }).ToList();
            return Json(typelist, JsonRequestBehavior.DenyGet);

        }
        #endregion
        #region 商家登录
        public ActionResult Company_Login()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Company_Login")]
        public ActionResult Company_post(string UserName, string Password)
        {
            this.Session.Clear();
            var cookie = Response.Cookies["rememberMeLogin"];
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-100);
            var acookie = Response.Cookies["rememberMeName"];
            if (cookie != null)
                acookie.Expires = DateTime.Now.AddDays(-100);
            YG.SC.OpenShop.Authentication.FormAuthenticationService fom = new FormAuthenticationService();
            fom.SignOut();
            //获取账户
            string message = "IsSuccss";
            var entity = this._iCustomerService.GetEntityByNameAndPassword(UserName, Password);
            if (entity == null
                || !entity.GroupId.HasValue
                || (CommonEnum.GroupOfCustomer)entity.GroupId != CommonEnum.GroupOfCustomer.Member && (CommonEnum.GroupOfCustomer)entity.GroupId != CommonEnum.GroupOfCustomer.OpenShop
                )//前台只有会员和商家可以登录。
            {
                entity = null;
            }
            if (entity == null)
            {
                message = "用户名或密码错误";
                ViewBag.msg = message;
                return View();
                // if (!entity.Password.Equals(Password)) return "密码错误";
                //if (entity.UserCd != UserCdSenior) return Json(new ResultModel(false, "权限错误"));
            }
            if (entity.GroupId == (int)YG.SC.Common.CommonEnum.GroupOfCustomer.OpenShop)
            {
                WriteUserSession(entity, "");
                return Redirect("~/Merchant/Company_index/" + entity.Companyid);
            }
            else
            {
                message = "用户名或密码错误";
                ViewBag.msg = message;
                return View();
            }
        }

        #endregion
        #region 商家信息修改
        [UserAuthorize]
        public ActionResult Company_info(int id = 0)
        {
            var moudel = this._iOpenShopService.GetById(id);
            ViewBag.ValueStr = id == 0 ? "" : moudel.FkType.ValueStr;
            ViewBag.District = id == 0 ? "" : moudel.Fkhotarea.name;
            ViewBag.Range = id == 0 ? "" : moudel.FKObject.Name;
            return View(moudel);
        }
        [HttpPost]
        [ActionName("Company_info")]
        public ActionResult Company_infoEidt(DataAccess.OpenShop openshop)
        {
            var moudel = this._iOpenShopService.GetById(openshop.Id);
            moudel.Id = openshop.Id;
            moudel.Address = openshop.Address;
            moudel.Url = openshop.Url;
            moudel.Introduction = openshop.Introduction;
            moudel.Logo = openshop.Logo;
            moudel.QRcode = openshop.QRcode;
            this._iOpenShopService.Update(moudel);
            return RedirectToAction("Company_info/" + moudel.Id);
        }
        [ValidateInput(false)]
        public ActionResult Company_Case(int Id = 0)
        {
            OpenShopPhoto Photo = new OpenShopPhoto();
            Photo.OpenShopId = Id;
            return View(Photo);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Company_Case")]
        public ActionResult Company_Caseimg(OpenShopPhoto OPPhoto)
        {

            if (OPPhoto.Id == 0)
            {
                this._iOpenShopService.InsertPhoto(OPPhoto);
            }
            else
            {
                OpenShopPhoto model = this._iOpenShopService.GetPhotoById(OPPhoto.Id);
                model.Id = OPPhoto.Id;
                model.title = OPPhoto.title != null ? OPPhoto.title : model.title;
                model.Rmark = OPPhoto.Rmark != null ? OPPhoto.Rmark : model.Rmark;
                model.Photo = OPPhoto.Photo != null ? OPPhoto.Photo : model.Photo;
                model.PhotoSmall = OPPhoto.PhotoSmall != null ? OPPhoto.PhotoSmall : model.PhotoSmall;
                model.PhotoSquare = OPPhoto.PhotoSquare != null ? OPPhoto.PhotoSquare : model.PhotoSquare;
                model.PhotoRectangle = OPPhoto.PhotoRectangle != null ? OPPhoto.PhotoRectangle : model.PhotoRectangle;
                this._iOpenShopService.UpdatePhoto(model);
            }

            return RedirectToAction("Company_Case/" + OPPhoto.OpenShopId);
        }
        [UserAuthorize]
        public ActionResult Company_Qualifications(int Id = 0)
        {
            DataAccess.OpenShop openshop = this._iOpenShopService.GetById(Id);
            return View(openshop);
        }
        [HttpPost]
        [ActionName("Company_Qualifications")]
        public ActionResult Company_QualificationsUPdate(DataAccess.OpenShop openshop)
        {
            DataAccess.OpenShop moudel = this._iOpenShopService.GetById(openshop.Id);
            moudel.Id = openshop.Id;
            moudel.BLS = openshop.BLS;
            moudel.CIC = openshop.CIC;
            this._iOpenShopService.Update(moudel);
            return RedirectToAction("Company_Qualifications/" + moudel.Id);
        }
        [ValidateInput(false)]
        public ActionResult Company_infoimg(DataAccess.OpenShop moudel)
        {
            string viewname = "";
            var fileLogoName = UploadImgUtility.UploadImage(Request.Files["Logofile"], Server.MapPath(CommonContorllers.FileUploadOpenShopLogoImgPath));
            if (!string.IsNullOrEmpty(fileLogoName))
            {
                ViewBag.Range = Request["Range"];
                ViewBag.ValueStr = Request["ValueStr"];
                ViewBag.District = Request["District"];
                moudel.Logo = CommonContorllers.FileUploadOpenShopLogoImgPath + fileLogoName;
                viewname = "Company_info";

            }
            var fileQRcodeName = UploadImgUtility.UploadImage(Request.Files["QRcodefile"], Server.MapPath(CommonContorllers.FileUploadOpenShopQRCodeImgPath));
            if (!string.IsNullOrEmpty(fileQRcodeName))
            {
                ViewBag.Range = Request["Range"];
                ViewBag.ValueStr = Request["ValueStr"];
                ViewBag.District = Request["District"];
                moudel.QRcode = CommonContorllers.FileUploadOpenShopQRCodeImgPath + fileQRcodeName;
                viewname = "Company_info";
            }

            var fileBLSName = UploadImgUtility.UploadImage(Request.Files["BLSfile"], Server.MapPath(CommonContorllers.FileUploadOpenShopBLSImgPath));
            if (!string.IsNullOrEmpty(fileBLSName))
            {
                moudel.BLS = CommonContorllers.FileUploadOpenShopBLSImgPath + fileBLSName;
                viewname = "Company_Qualifications";
            }
            var fileCICName = UploadImgUtility.UploadImage(Request.Files["CICfile"], Server.MapPath(CommonContorllers.FileUploadOpenShopCICImgPath));
            if (!string.IsNullOrEmpty(fileCICName))
            {
                moudel.CIC = CommonContorllers.FileUploadOpenShopCICImgPath + fileCICName;
                viewname = "Company_Qualifications";
            }
            return View(viewname, moudel);

        }
        [ValidateInput(false)]
        public ActionResult EditPhoto(OpenShopPhoto moudel)
        {
            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["OpenShopPhoto"], Server.MapPath(CommonContorllers.FileUploadOpenShopImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSmallImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSquareImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                moudel.Photo = CommonContorllers.FileUploadOpenShopImgPath + fileName;
                moudel.PhotoSmall = CommonContorllers.FileUploadOpenShopSmallImgPath + fileName;
                moudel.PhotoSquare = CommonContorllers.FileUploadOpenShopSquareImgPath + fileName;
                moudel.PhotoRectangle = CommonContorllers.FileUploadOpenShopRectangleImgPath + fileName;
            }
            return View("Company_Case", moudel);
        }
        #endregion
        #region 商家中心
        [UserAuthorize]
        public ActionResult Company_index(int id = 0)
        {
            var moudel = this._iOpenShopService.GetById(id);
            Customer customer = this._iCustomerService.GetEntityById(UserContext.Current.Id);
            ViewBag.message = this._messageService.GetTopN(customer);
            ViewBag.CountOfPendingMission = _missionService.CountOfPendingMission(customer);
            return View(moudel);
        }
        #endregion
        #region 验证信息
        public ActionResult CasePartial(int OpenShopId)
        {
            if (OpenShopId <= 0)
            {
                OpenShopId = -1;
            }
            var moudel = this._iOpenShopService.GetPhotoByBrandId(1, OpenShopId, "");
            return View(moudel);
        }
        /// <summary>
        /// 验证商家名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool validateRename(string name)
        {
            return this._iOpenShopService.GetEntitsByName(name.Trim());
        }
        /// <summary>
        /// 验证商家简称
        /// </summary>
        /// <param name="Abbreviation"></param>
        /// <returns></returns>
        public bool validateAbbreviation(string Abbreviation)
        {
            return this._iOpenShopService.GetEntitsByAbbreviation(Abbreviation.Trim());
        }
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="Moblie"></param>
        /// <returns></returns>
        public bool validateMobile(string Moblie)
        {
            if (this._iCustomerService.GetCustomerByMobile(Moblie) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        private void WriteUserSession(Customer user, string rememberMe)
        {
            Session[CommonContorllers.UserIdCacheName] = user.Id;
            Session[CommonContorllers.UserNameCacheName] = user.LoginName;
            var _authenticationService = new FormAuthenticationService();
            _authenticationService.SignIn(user.LoginName, true);
            var aCookie = new HttpCookie("rememberMeName") { Value = user.LoginName, Expires = DateTime.Now.AddDays(10) };
            var cCookie = new HttpCookie("rememberMeLogin") { Value = rememberMe, Expires = DateTime.Now.AddDays(10) };
            Response.Cookies.Add(aCookie);
            Response.Cookies.Add(cCookie);
        }
        #region//开店帮图片
        public ActionResult OpenShopPhoto(int pg = 1, int Id = 0, string txtName = "")
        {
            _openShopId = Id;
            var model = this._iOpenShopService.GetPhotoByBrandId(pg, Id, txtName);
            return View(model);
        }

        [HttpGet]
        public ActionResult PhotoAdd(ShopBrandPhoto shopBrand)
        {
            return View();
        }


        [HttpPost]
        [ActionName("PhotoAdd")]
        public ActionResult PhotoAddPost(OpenShopPhoto Photo)
        {

            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["OpenShopPhoto"], Server.MapPath(CommonContorllers.FileUploadOpenShopImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSmallImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSquareImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                Photo.Photo = CommonContorllers.FileUploadOpenShopImgPath + fileName;
                Photo.PhotoSmall = CommonContorllers.FileUploadOpenShopSmallImgPath + fileName;
                Photo.PhotoSquare = CommonContorllers.FileUploadOpenShopSquareImgPath + fileName;
                Photo.PhotoRectangle = CommonContorllers.FileUploadOpenShopRectangleImgPath + fileName;
            }
            Photo.OpenShopId = _openShopId;
            Photo.Recsts = 1;
            this._iOpenShopService.InsertPhoto(Photo);

            return Redirect("~/Merchant/OpenShopPhoto?Id=" + _openShopId);
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>
        /// The ActionResult
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/22 16:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult PhotoEdit(int id = 0)
        {
            var entity = this._iOpenShopService.GetPhotoById(id);
            return View(entity);
        }

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="shopBrand"></param>
        /// <returns>
        /// ActionResult
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/22 16:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [HttpPost]
        [ActionName("PhotoEdit")]
        public ActionResult PhotoEditPost(OpenShopPhoto Photo)
        {

            var entity = this._iOpenShopService.GetPhotoById(Photo.Id);
            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["OpenShopPhoto"], Server.MapPath(CommonContorllers.FileUploadOpenShopImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSmallImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSquareImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                entity.Photo = CommonContorllers.FileUploadOpenShopImgPath + fileName;
                entity.PhotoSmall = CommonContorllers.FileUploadOpenShopSmallImgPath + fileName;
                entity.PhotoSquare = CommonContorllers.FileUploadOpenShopSquareImgPath + fileName;
                entity.PhotoRectangle = CommonContorllers.FileUploadOpenShopRectangleImgPath + fileName;
            }
            this._iOpenShopService.UpdatePhoto(entity);

            return Redirect("~/Merchant/OpenShopPhoto?Id=" + _openShopId);
        }

        public void PhotoDelete(int id, string state)
        {
            var entity = this._iOpenShopService.GetPhotoById(id);

            var recsts = state == "delete" ? -1 : 0;
            entity.Recsts = recsts;
            this._iOpenShopService.UpdatePhoto(entity);
            TempData["SelRecsts"] = entity.Recsts;
        }

        public void PhotoUpdate(int id)
        {
            var entity = this._iOpenShopService.GetPhotoById(id);
            if (entity != null)
            {
                entity.Recsts = 1;
                this._iOpenShopService.UpdatePhoto(entity);
                TempData["SelRecsts"] = entity.Recsts;
            }

        }
        #endregion//
        #region//富文本编辑器
        //public ActionResult EditRmark(int id)
        //{
        //    var entity = this._iOpenShopService.GetById(id);

        //    return View(entity);
        //}

        //[ValidateInput(false)]
        //public ActionResult Process(int id, string data)
        //{
        //    var entity = this._iOpenShopService.GetById(id);
        //    entity.Rmark = data;
        //    this._iOpenShopService.Update(entity);
        //    return RedirectToAction("Index");
        //}

        public void ProcessFile()
        {
            String aspxUrl = ""; // Request.Path.Substring(0,  Request.Path.LastIndexOf("/") + 1);

            //文件保存目录路径
            String savePath = "/attached/";

            //文件保存目录URL
            String saveUrl = aspxUrl + "/attached/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;
            this.context = context;

            var imgFile = Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }

            String dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                showError("上传目录不存在。");
            }

            String dirName = Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            String fileUrl = saveUrl + newFileName;

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;
            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            Response.Write(JsonMapper.ToJson(hash));
            Response.End();

        }


        public HttpContext context;

        public void ProcessFileManger()
        {
            //String aspxUrl =  Request.Path.Substring(0,  Request.Path.LastIndexOf("/") + 1);

            //文件保存目录路径
            String savePath = "/attached/";

            //文件保存目录URL
            String saveUrl = "/attached/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;
            this.context = context;

            var imgFile = Request.Files["imgFile"];
            if (imgFile == null)
            {
                showError("请选择文件。");
            }

            String dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                showError("上传目录不存在。");
            }

            String dirName = Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                showError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                showError("上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;

            imgFile.SaveAs(filePath);

            String fileUrl = saveUrl + newFileName;

            Hashtable hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = fileUrl;
            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            Response.Write(JsonMapper.ToJson(hash));
            Response.End();
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            Response.Write(JsonMapper.ToJson(hash));
            Response.End();
        }

        #endregion//富文本编辑器


    }
}
