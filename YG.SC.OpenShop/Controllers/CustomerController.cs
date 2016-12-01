using System.Web.Mvc;
using AutoMapper;
using YG.SC.DataAccess;
using YG.SC.Model.Project;
using YG.SC.Service.IService;
using System;
using System.Linq;
using System.Collections.Generic;
using YG.SC.Common;
using YG.SC.Model;
using System.Drawing;
using YG.SC.OpenShop.Models;
using System.Configuration;
using System.IO;
using YG.SC.OpenShop.Helper;
using System.Collections;
using System.Globalization;
using LitJson;
using System.Web;
using YG.SC.Service;
namespace YG.SC.OpenShop.Controllers
{
    public class CustomerController : BaseController
    {
        //
        // GET: /Customer/
        private static int _brandId;
        private readonly ICustomerService _iCustomerService;
        private readonly IApplyActiviteService _ApplyActivite;
        private readonly IShopProjectService _IShopProjectService;
        private readonly ICustomerLogService _iCustomerLogService;
        private readonly IOpenShopService _iOpenShopService;
        private readonly IShopBrandService _IShopBrandService;
        private readonly IShopAttributesService _iShopAttributesServiceService;
        private readonly IMessageService _messageService;
        private readonly IOrderService _orderService;
        private readonly IMissionService _missionService;
        /// <summary>
        /// 上传图片域名。
        /// </summary>
        public static string CDN_DOMAIN = ConfigurationManager.AppSettings["CdnDomain"];

        public CustomerController(ICustomerService iCustomerService, IApplyActiviteService ApplyActivite, IShopProjectService IShopProjectService, ICustomerLogService iCustomerLogService, IOpenShopService OpenShopService, IShopBrandService IShopBrandService, IShopAttributesService IShopAttributesServiceService, IMessageService messageService, IOrderService orderService, IMissionService missionService)
        {
            _IShopProjectService = IShopProjectService;
            _iCustomerService = iCustomerService;
            _ApplyActivite = ApplyActivite;
            _iCustomerLogService = iCustomerLogService;
            _iOpenShopService = OpenShopService;
            _IShopBrandService = IShopBrandService;
            _iShopAttributesServiceService = IShopAttributesServiceService;
            _messageService = messageService;
            _orderService = orderService;
            _missionService = missionService;
        }
        protected override void Dispose(bool disposing)
        {
            this._iCustomerLogService.Dispose();
            this._iOpenShopService.Dispose();
            this._IShopProjectService.Dispose();
            this._ApplyActivite.Dispose();
            this._iCustomerService.Dispose();
            _messageService.Dispose();
            base.Dispose(disposing);
        }

        public ViewResult Accounts()
        {
            Customer c = _iCustomerService.GetEntityById(UserId);
            ViewBag.OrderList = _orderService.GetOrderTopN(c);
            return View(c);
        }

        public ActionResult Index(int CustomerId = 1)
        {
            var query = this._iCustomerService.GetEntityById(CustomerId);
            //var customer = Mapper.Map<Customer, CustomerModel>(query);
            var searchCriteria = new ProjectSearchCriteria();
            searchCriteria.Projecthot = 1;
            var searchResult = _IShopProjectService.SearchProject(searchCriteria);
            var arraylist = Mapper.Map<ShopProject[], ProjectDetailsViewModel[]>(searchResult.Item1).OrderByDescending(p => p.CreatTime).Take(3).ToList();
            ViewBag.array = arraylist;

            return View(query);
        }
        [HttpGet]
        public ActionResult Userinfo(int Id = 1)
        {
            var query = this._iCustomerService.GetEntityById(Id);
            return View(query);
        }
        [HttpPost]
        [ActionName("Userinfo")]
        [ValidateInput(true)]
        public ActionResult UserinfoEdit(Customer model)
        {
            var entity = this._iCustomerService.GetEntityById(model.Id);
            entity.Name = model.Name != null ? model.Name : entity.Name;
            entity.Email = model.Email != null ? model.Email : entity.Email;
            entity.portrait = model.portrait != null ? model.portrait : entity.portrait;
            this._iCustomerService.Update(entity);
            ViewBag.msg = "修改成功";
            return View(entity);
        }
        public ActionResult uploadimg(Customer moudel)
        {
            var fileBLSName = UploadImgUtility.UploadImage(Request.Files["portraitfile"], Server.MapPath(CommonContorllers.FileUploadPortraitImgPath));
            if (!string.IsNullOrEmpty(fileBLSName))
            {
                moudel.portrait = CommonContorllers.FileUploadPortraitImgPath + fileBLSName;
            }
            return View("Userinfo", moudel);
        }
        [HttpGet]
        public ActionResult EditPwd(int CustomerId = 1)
        {
            var query = this._iCustomerService.GetEntityById(CustomerId);
            //var customer = Mapper.Map<Customer, CustomerModel>(query);
            return View(query);
        }
        [HttpPost]
        [ActionName("EditPwd")]
        [ValidateInput(true)]
        public ActionResult EditPwd(Customer model)
        {
            var entity = this._iCustomerService.GetEntityById(model.Id);
            entity.Password = model.Password != null ? model.Password : entity.Password;
            this._iCustomerService.Update(entity);
            return Redirect("~/Customer/Index?CustomerId=" + model.Id);
        }
        public string ValidatePWD(string LoginName, string Password)
        {
            string message = "";
            var entity = this._iCustomerService.GetEntityByNameAndPassword(LoginName, Password);
            if (entity == null)
            {
                message = "密码错误";
            }
            return message;
        }
        public ActionResult Focus(int CustomerId = 0)
        {
            List<ProjectDetailsViewModel> Shoplist = new List<ProjectDetailsViewModel>();
            List<DataAccess.OpenShop> zhuangxiu = new List<DataAccess.OpenShop>();
            List<DataAccess.OpenShop> jiaju = new List<DataAccess.OpenShop>();
            //List<DataAccess.OpenShop> rongzhi = new List<DataAccess.OpenShop>();
            List<DataAccess.OpenShop> tuiguang = new List<DataAccess.OpenShop>();
            List<CustomerLog> loglist = this._iCustomerLogService.GetEntityByCustomer(CustomerId).Distinct().OrderByDescending(p => p.addtime).ToList();
            foreach (CustomerLog log in loglist)
            {
                if (log.ProjectId.HasValue && log.ProjectId > 0)
                {
                    if (log.Targetsubject == 1)
                    {
                        ProjectDetailsViewModel shop = Mapper.Map<ShopProject, ProjectDetailsViewModel>(this._IShopProjectService.GetById(log.ProjectId.Value));
                        if (shop != null)
                        {
                            Shoplist.Add(shop);
                        }
                    }
                    else if (log.Targetsubject == 3)
                    {
                        DataAccess.OpenShop shop = this._iOpenShopService.GetById(log.ProjectId.Value);
                        if (shop != null)
                        {
                            zhuangxiu.Add(shop);
                        }
                    }
                    else if (log.Targetsubject == 4)
                    {
                        DataAccess.OpenShop shop = this._iOpenShopService.GetById(log.ProjectId.Value);
                        if (shop != null)
                        {
                            jiaju.Add(shop);
                        }
                    }
                    //else if (log.type == 5)
                    //{
                    //    DataAccess.OpenShop oshop = this._iOpenShopService.GetById(log.ProjectId.Value);
                    //    rongzhi.Add(oshop);
                    //}
                    else if (log.Targetsubject == 6)
                    {
                        DataAccess.OpenShop shop = this._iOpenShopService.GetById(log.ProjectId.Value);
                        if (shop != null)
                        {
                            tuiguang.Add(shop);
                        }
                    }
                }
                ViewBag.shoplist = Shoplist.Take(3).ToList();
                ViewBag.zhuangxiu = zhuangxiu.Take(6).ToList();
                ViewBag.jiaju = jiaju.Take(6).ToList();
                //ViewBag.rongzhi = rongzhi.Take(6).ToList();
                ViewBag.tuiguang = tuiguang.Take(6).ToList();
            }
            return View();
        }

        public ActionResult Tuan(int Id = 1)
        {
            Customer moudel = this._iCustomerService.GetEntityById(Id);
            if (moudel != null && !string.IsNullOrEmpty(moudel.Mobile))
            {
                string pg = Request.Params["pg"] == null ? "0" : Request.Params["pg"];
                string ProjectName = Request.Params["ProjectName"] == null ? "" : Request.Params["ProjectName"];
                string Begintime = Request.Params["Begintime"] == null ? "" : Request.Params["Begintime"];
                string Endtime = Request.Params["Endtime"] == null ? "": Request.Params["Endtime"];
                YG.SC.Model.ApplyActivitySearchCriteria SearchCriteria = new Model.ApplyActivitySearchCriteria();
                SearchCriteria.pg = int.Parse(pg);
                SearchCriteria.Phone = moudel.Mobile;
                SearchCriteria.ProjectName=ProjectName;
                if(!string.IsNullOrEmpty(Begintime))
                {
                SearchCriteria. BeginTime = DateTime.Parse(Begintime);
                }
               if(!string.IsNullOrEmpty(Endtime))
                {
                SearchCriteria.EndTime = DateTime.Parse(Endtime);
                 }
               if (!string.IsNullOrEmpty(Request.Params["Status"]))
               {
                   SearchCriteria.Status = int.Parse(Request.Params["Status"]);
               }
                Tuple<ApplyActivity[], YG.SC.Model.PagerEntity> searchresult = this._ApplyActivite.Search(SearchCriteria);
                ViewBag.array = searchresult;
            }

            return View();
        }
        public ActionResult Memberindex(int id = 1)
        {
            var moudel = this._IShopBrandService.GetById(id);
            Customer customer = this._iCustomerService.GetEntityById(UserId);
            ViewBag.customer = customer;
            ViewBag.message = this._messageService.GetTopN(customer);
            ViewBag.CountOfPendingMission = _missionService.CountOfPendingMission(customer);
            return View(moudel);
        }

        /// <summary>
        /// 获取当前登录用户。
        /// </summary>
        /// <param name="isMember">确认是会员用户。</param>
        /// <param name="isBrand">确认是品牌用户。</param>
        /// <returns></returns>
        public Customer GetCurrentUser(out bool isMember, out bool isBrand)
        {
            isMember = false;
            isBrand = false;
            Customer c = _iCustomerService.GetEntityById(UserContext.Current.Id);
            if (c == null)
            {
                return null;
            }
            isMember = c.GroupId.HasValue && (CommonEnum.GroupOfCustomer)c.GroupId == CommonEnum.GroupOfCustomer.Member;
            isBrand = isMember && c.Companyid > 0;
            return c;
        }

        /// <summary>
        /// 品牌。
        /// </summary>
        /// <returns></returns>
        public ActionResult Brand()
        {
            ShopBrand entity = null;
            bool isMember;
            bool isBrand;
            Customer member = GetCurrentUser(out isMember, out isBrand);
            if (member == null || !isMember)//只有已登录的会员用户才可以管理自己的品牌。
            {
                return RedirectToAction("Index", "Login");
            }
            if (isBrand)
            {
                entity = _IShopBrandService.GetById(member.Companyid);
            }
            Brand b = new Brand();
            if (entity != null)
            {
                b.Id = entity.Id;
                b.Name = entity.Name;
                b.Logo = Path.Combine(CDN_DOMAIN, CommonContorllers.FileUploadBrandLogoImgPath, entity.Logo);
                b.QRCode = Path.Combine(CDN_DOMAIN, CommonContorllers.FileUploadBrandQRCodeImgPath, entity.QRCode);
                b.WebUrl = entity.Url;
                b.VideoUrl = entity.VidoUrl;
                b.IsAllowJoin = entity.JoinIn.HasValue ? entity.JoinIn.Value : false;
                b.IsValid = entity.Recsts != 0;
                b.Introduce = entity.Abbreviation;
                b.ShopBrandAttributeValues = entity.ShopBrandAttributeValues;
            }
            ViewBag.FunctionName = isBrand ? "修改品牌" : "品牌申请";

            //#region 业态使用的扩展控件需要先定义选项。

            //List<ShopAttributeValues> list = _iShopAttributesServiceService.GetListByAttributeId(9);//业态。
            //List<CodeDescription> codes = new List<CodeDescription>();
            //if (list != null)
            //{
            //    foreach (ShopAttributeValues item in list)
            //    {
            //        codes.Add(new CodeDescription(item.Id.ToString(), item.ValueStr, "业态"));
            //    }
            //}
            //CodeManager.codes = codes.ToArray();

            //#endregion

            ViewBag.AttriButes = this._iShopAttributesServiceService.GetListByAttributeId(9);

            return View(b);
        }

        /// <summary>
        /// 添加品牌。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BrandEdit(Brand model)
        {
            bool isMember;
            bool isBrand;
            Customer member = GetCurrentUser(out isMember, out isBrand);
            if (member == null || !isMember)//只有已登录的会员用户才可以管理自己的品牌。
            {
                return RedirectToAction("Index", "Login");
            }
            var fileLogoName = UploadImgUtility.UploadImage(Request.Files["Logo"], Server.MapPath(CommonContorllers.FileUploadBrandLogoImgPath));
            var fileQRCodeName = UploadImgUtility.UploadImage(Request.Files["QRCode"], Server.MapPath(CommonContorllers.FileUploadBrandQRCodeImgPath));
            ShopBrand sb = new ShopBrand();
            if (isBrand)
            {
                sb = _IShopBrandService.GetById(member.Companyid);
            }
            sb.Name = model.Name;
            if (!string.IsNullOrEmpty(fileLogoName))
            {
                sb.Logo = fileLogoName;
            }
            if (!string.IsNullOrEmpty(fileQRCodeName))
            {
                sb.QRCode = fileQRCodeName;
            }
            sb.Url = model.WebUrl;
            sb.VidoUrl = model.VideoUrl;
            sb.JoinIn = model.IsAllowJoin;
            sb.Recsts = model.IsValid ? 1 : 0;
            sb.Abbreviation = model.Introduce;
            if (isBrand)
            {
                sb.Id = member.Companyid;
                _IShopBrandService.Update(sb);
            }
            else
            {
                _IShopBrandService.Insert(sb);
                Customer c = _iCustomerService.GetEntityById(UserContext.Current.Id);
                c.Companyid = sb.Id;
                _iCustomerService.Update(c);
            }
            //增加业态
            List<ShopBrandAttributeValues> listattr = new List<ShopBrandAttributeValues>();
            var attributesvalue = Request["selAttribute"];
            if (!string.IsNullOrEmpty(attributesvalue))
            {
                foreach (var item in attributesvalue.Split(','))
                {
                    ShopBrandAttributeValues bv = new ShopBrandAttributeValues()
                    {
                        BrandId = sb.Id,
                        Recsts = 1,
                        AttributesId = Convert.ToInt32(item.Split('-')[0]),
                        AttributeValuesId = Convert.ToInt32(item.Split('-')[1]),
                    };
                    listattr.Add(bv);
                }
            }
            this._iShopAttributesServiceService.InsertList(listattr);
            return RedirectToAction("Brand");
        }

        /// <summary>
        /// 输出销售价格
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string GetSalePrice(object price)
        {
            if (price == null || Convert.ToDouble(price) == 0)
            {
                return "面议";
            }
            else
            {
                double _price = Convert.ToDouble(price);
                return "" + MoneyManage.ToCHSimpleMoney(_price);

            }
        }
        /// <summary>
        /// 获取图片宽高别设置img宽高和空余
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetWH(string path, int width, int height)
        {
            string strStyle = "";

            Image imageFrom = null;

            string pathStr = System.Web.HttpContext.Current.Server.MapPath(path);

            //检查图片路径是否正确
            if (System.IO.File.Exists(pathStr))
            {
                imageFrom = Image.FromFile(pathStr);
            }
            if (imageFrom == null)
            {
                strStyle = "width:" + width + "px;height:" + height + "px;";
                return strStyle;
            }

            //原图宽高
            int imageWidth = imageFrom.Width;
            int imageHeight = imageFrom.Height;

            if (imageWidth * height > imageHeight * width)
            {
                strStyle = "width:" + width + "px;height:" + imageHeight * width / imageWidth + "px;margin-top:" + (height - imageHeight * width / imageWidth) / 2 + "px;";
            }
            else
            {
                strStyle = "width:" + imageWidth * height / imageHeight + "px;height:" + height + "px;margin-left:" + (width - imageWidth * height / imageHeight) / 2 + "px;";
            }

            return strStyle;
        }


        public ContentResult IsLogin()
        {
            Customer c = _iCustomerService.GetEntityById(UserId);
            return Content((c == null ? 0 : 1).ToString());
        }

        #region//品牌图片
        public ActionResult OpenShopPhoto(int pg = 1, int Id = 0, string txtName = "")
        {
            _brandId = Id;
            var model = this._IShopBrandService.GetPhotoByBrandId(pg, Id, txtName);
            return View(model);
        }

        [HttpGet]
        public ActionResult PhotoAdd(ShopBrandPhoto shopBrand)
        {
            return View();
        }


        [HttpPost]
        [ActionName("PhotoAdd")]
        public ActionResult PhotoAddPost(ShopBrandPhoto Photo)
        {

            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["OpenShopPhoto"], Server.MapPath(CommonContorllers.FileUploadBrandImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSmallImgPath),
                Server.MapPath(CommonContorllers.FileUploadBrandSquareImgPath), Server.MapPath(CommonContorllers.FileUploadBrandRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                Photo.Photo = CommonContorllers.FileUploadBrandImgPath + fileName;
                Photo.PhotoSmall = CommonContorllers.FileUploadBrandSmallImgPath + fileName;
                Photo.PhotoSquare = CommonContorllers.FileUploadBrandSquareImgPath + fileName;
                Photo.PhotoRectangle = CommonContorllers.FileUploadBrandRectangleImgPath + fileName;
            }
            Photo.BrandId = _brandId;
            Photo.Recsts = 1;
            this._IShopBrandService.InsertPhoto(Photo);

            return Redirect("~/Customer/OpenShopPhoto?Id=" + _brandId);
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
            var entity = this._IShopBrandService.GetPhotoById(id);
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
        public ActionResult PhotoEditPost(ShopBrandPhoto Photo)
        {

            var entity = this._IShopBrandService.GetPhotoById(Photo.Id);
            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["OpenShopPhoto"], Server.MapPath(CommonContorllers.FileUploadBrandImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSmallImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSquareImgPath), Server.MapPath(CommonContorllers.FileUploadBrandRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                entity.Photo = CommonContorllers.FileUploadBrandImgPath + fileName;
                entity.PhotoSmall = CommonContorllers.FileUploadBrandSmallImgPath + fileName;
                entity.PhotoSquare = CommonContorllers.FileUploadBrandSquareImgPath + fileName;
                entity.PhotoRectangle = CommonContorllers.FileUploadBrandRectangleImgPath + fileName;
            }
            this._IShopBrandService.UpdatePhoto(entity);

            return Redirect("~/Customer/OpenShopPhoto?Id=" + _brandId);
        }

        public void PhotoDelete(int id, string state)
        {
            var entity = this._IShopBrandService.GetPhotoById(id);

            var recsts = state == "delete" ? -1 : 0;
            entity.Recsts = recsts;
            this._IShopBrandService.UpdatePhoto(entity);
            TempData["SelRecsts"] = entity.Recsts;
        }

        public void PhotoUpdate(int id)
        {
            var entity = this._IShopBrandService.GetPhotoById(id);
            if (entity != null)
            {
                entity.Recsts = 1;
                this._IShopBrandService.UpdatePhoto(entity);
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
