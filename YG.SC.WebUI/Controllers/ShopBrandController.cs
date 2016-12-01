using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LitJson;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Service.IService;
using YG.SC.WebUI.Filters;

namespace YG.SC.WebUI.Controllers
{
    public class ShopBrandController : WebBaseController
    {
        //
        // GET: /ShopBrand/

        private static int _shopBrandId;
        private readonly IShopBrandService _IShopBrandService;
        private readonly IShopAttributesService _iShopAttributesServiceService;

        public ShopBrandController(IShopBrandService iShopBrandService, IShopAttributesService iShopAttributesServiceService)
        {
            _IShopBrandService = iShopBrandService;
            _iShopAttributesServiceService = iShopAttributesServiceService;
        }

        protected override void Dispose(bool disposing)
        {
            this._IShopBrandService.Dispose();
            this._iShopAttributesServiceService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index(int pg = 1, int ShopProjectId = 0, string txtName = "")
        {

            var model = this._IShopBrandService.GetEntitsByName(pg, ShopProjectId, txtName);
            return View(model);
        }

        [HttpGet]
        public ActionResult Add(ShopBrand shopBrand)
        {
            var model = this._iShopAttributesServiceService.GetListByAttributeId(9);
            return View(model);
        }


        [HttpPost]
        [ActionName("Add")]
        public ActionResult AddPost(ShopBrand shopBrand)
        {


            var fileLogoName = UploadImgUtility.UploadImage(Request.Files["Logo"], Server.MapPath(CommonContorllers.FileUploadLogoImgPath));

            var fileQRCodeName = UploadImgUtility.UploadImage(Request.Files["QRCode"], Server.MapPath(CommonContorllers.FileUploadQRCodeImgPath));
            if (!string.IsNullOrEmpty(fileLogoName))
            {
                shopBrand.Logo = CommonContorllers.FileUploadLogoImgPath + fileLogoName;
            }
            if (!string.IsNullOrEmpty(fileQRCodeName))
            {
                shopBrand.QRCode = CommonContorllers.FileUploadQRCodeImgPath + fileQRCodeName;
            }
           
            shopBrand.CreatTime = DateTime.Now;
            shopBrand.Recsts = 1;
            this._IShopBrandService.Insert(shopBrand);

            //增加业态
            List<ShopBrandAttributeValues> listattr = new List<ShopBrandAttributeValues>();
            var attributesvalue = Request["selAttribute"];
            if (!string.IsNullOrEmpty(attributesvalue))
            {
                foreach (var item in attributesvalue.Split(','))
                {
                    ShopBrandAttributeValues bv = new ShopBrandAttributeValues()
                    {
                        BrandId = shopBrand.Id,
                        Recsts = 1,
                        AttributesId=Convert.ToInt32(item.Split('-')[0]),
                        AttributeValuesId = Convert.ToInt32(item.Split('-')[1]),
                    };
                    listattr.Add(bv);
                }
            }
            this._iShopAttributesServiceService.InsertList(listattr);
            return RedirectToAction("Index");
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
        public ActionResult Edit(int id = 0)
        {
            var entity = this._IShopBrandService.GetById(id);
          ViewBag.AttriButes = this._iShopAttributesServiceService.GetListByAttributeId(9);
            
            return View(entity);
        }

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="shopBrand"></param>
        /// <returns>
        /// ActionResult
        /// </returns>
        [HttpPost]
        [ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult EditPost(ShopBrand shopBrand)
        {

            var entity = this._IShopBrandService.GetById(shopBrand.Id);

            var fileLogoName = UploadImgUtility.UploadImage(Request.Files["LogoImg"], Server.MapPath(CommonContorllers.FileUploadLogoImgPath));

            var fileQRCodeName = UploadImgUtility.UploadImage(Request.Files["QRCodeImg"], Server.MapPath(CommonContorllers.FileUploadQRCodeImgPath));
            if (!string.IsNullOrEmpty(fileLogoName))
            {
                entity.Logo = CommonContorllers.FileUploadLogoImgPath + fileLogoName;
            }
            if (!string.IsNullOrEmpty(fileQRCodeName))
            {
                entity.QRCode = CommonContorllers.FileUploadQRCodeImgPath + fileQRCodeName;
            }
            entity.Name = shopBrand.Name;
            entity.Rmark = shopBrand.Rmark;
            entity.Url = shopBrand.Url;
            entity.VidoUrl = shopBrand.VidoUrl;
            entity.JoinIn = shopBrand.JoinIn;
            this._IShopBrandService.Update(entity);


            List<ShopBrandAttributeValues> listattr = new List<ShopBrandAttributeValues>();
            var attributesvalue = Request["selAttribute"];
            if (!string.IsNullOrEmpty(attributesvalue))
            {
                foreach (var item in attributesvalue.Split(','))
                {
                    ShopBrandAttributeValues bv = new ShopBrandAttributeValues()
                    {
                        BrandId = shopBrand.Id,
                        Recsts = 1,
                        AttributesId = Convert.ToInt32(item.Split('-')[0]),
                        AttributeValuesId = Convert.ToInt32(item.Split('-')[1]),
                    };
                    listattr.Add(bv);
                }
            }
            this._iShopAttributesServiceService.UpdateList(entity.Id, listattr);

            return RedirectToAction("Index");
        }

        public void Delete(int id, string state)
        {
            var entity = this._IShopBrandService.GetById(id);

            var recsts = state == "delete" ? -1 : 0;
            entity.Recsts = recsts;
            this._IShopBrandService.Update(entity);
            TempData["SelRecsts"] = entity.Recsts;
        }

        public void Update(int id)
        {
            var entity = this._IShopBrandService.GetById(id);
            if (entity != null)
            {
                entity.Recsts = 1;
                entity.CreatTime = DateTime.Now;
                this._IShopBrandService.Update(entity);
                TempData["SelRecsts"] = entity.Recsts;
            }

        }
        public ActionResult Item(int id = 0)
        {
            var entity = this._IShopBrandService.GetById(id);
            return View(entity);
        }
        [HttpPost]
        [ActionName("Item")]
        public ActionResult Itemaudit(ShopBrand moudel)
        {
            var brand = this._IShopBrandService.GetById(moudel.Id);
            brand.IsAudited = 1;
            this._IShopBrandService.Update(brand);
            ViewBag.msg = "审核通过";
            return View(brand);
        }
        #region//品牌店铺列表
        public ActionResult ShopBranch(int pg = 1, int Id = 0, string txtName = "")
        {
            _shopBrandId = Id;
            var model = this._IShopBrandService.GetBranchByBrandId(pg, Id, txtName);
            return View(model);
        }

        [HttpGet]
        public ActionResult BranchAdd(ShopBrandBranch shopBrand)
        {

            return View();
        }


        [HttpPost]
        [ActionName("BranchAdd")]
        public ActionResult BranchAddPost(ShopBrandBranch branch)
        {
            branch.BrandId = _shopBrandId;

            branch.Recsts = 1;
            this._IShopBrandService.BranchInsert(branch);

            return Redirect("ShopBranch?Id=" + _shopBrandId);
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
        public ActionResult BranchEdit(int id = 0)
        {
            var entity = this._IShopBrandService.GetBranchById(id);
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
        [ActionName("BranchEdit")]
        public ActionResult BranchEditPost(ShopBrandBranch branch)
        {

            var entity = this._IShopBrandService.GetBranchById(branch.Id);
            entity.BranchName = branch.BranchName;
            entity.BranchAddress = branch.BranchAddress;
            entity.BranchPhone = branch.BranchPhone;
            this._IShopBrandService.BranchUpdate(entity);

            return Redirect("/ShopBrand/ShopBranch?Id=" + branch.BrandId);
        }

        public void BranchDelete(int id, string state)
        {
            var entity = this._IShopBrandService.GetBranchById(id);

            var recsts = state == "delete" ? -1 : 0;
            entity.Recsts = recsts;
            this._IShopBrandService.BranchUpdate(entity);
            TempData["SelRecsts"] = entity.Recsts;
        }

        public void BranchUpdate(int id)
        {
            var entity = this._IShopBrandService.GetBranchById(id);
            if (entity != null)
            {
                entity.Recsts = 1;
                this._IShopBrandService.BranchUpdate(entity);
                TempData["SelRecsts"] = entity.Recsts;
            }

        }
        #endregion//
        #region//品牌图片
        public ActionResult BrandPhoto(int pg = 1, int Id = 0, string txtName = "")
        {
            _shopBrandId = Id;
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

            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["BrandPhoto"], Server.MapPath(CommonContorllers.FileUploadBrandImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSmallImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSquareImgPath), Server.MapPath(CommonContorllers.FileUploadBrandRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                Photo.Photo = CommonContorllers.FileUploadBrandImgPath + fileName;
                Photo.PhotoSmall = CommonContorllers.FileUploadBrandSmallImgPath + fileName;
                Photo.PhotoSquare = CommonContorllers.FileUploadBrandSquareImgPath + fileName;
                Photo.PhotoRectangle = CommonContorllers.FileUploadBrandRectangleImgPath + fileName;
            }
            Photo.BrandId = _shopBrandId;
            Photo.Recsts = 1;
            this._IShopBrandService.InsertPhoto(Photo);

            return Redirect("/ShopBrand/BrandPhoto?Id=" + _shopBrandId);
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
            var fileName = UploadImgUtility.UploadBrangImg(Request.Files["BrandPhoto"], Server.MapPath(CommonContorllers.FileUploadBrandImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSmallImgPath), Server.MapPath(CommonContorllers.FileUploadBrandSquareImgPath), Server.MapPath(CommonContorllers.FileUploadBrandRectangleImgPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                entity.Photo = CommonContorllers.FileUploadBrandImgPath + fileName;
                entity.PhotoSmall = CommonContorllers.FileUploadBrandSmallImgPath + fileName;
                entity.PhotoSquare = CommonContorllers.FileUploadBrandSquareImgPath + fileName;
                entity.PhotoRectangle = CommonContorllers.FileUploadBrandRectangleImgPath + fileName;
            }
            this._IShopBrandService.UpdatePhoto(entity);

            return Redirect("/ShopBrand/BrandPhoto?Id=" + _shopBrandId);
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

        //public ActionResult EditRmark(int id)
        //{
        //    var entity = this._IShopBrandService.GetById(id);

        //    return View(entity);
        //}
        #region//富文本编辑器
        //[ValidateInput(false)]
        //public ActionResult Process(int id,string data)
        //{
        //    var entity = this._IShopBrandService.GetById(id);
        //    entity.Rmark = data;
        //    this._IShopBrandService.Update(entity);
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
