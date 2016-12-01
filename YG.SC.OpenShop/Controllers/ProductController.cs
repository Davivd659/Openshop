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
    public class ProductController : BaseController
    {
        private readonly IObjectService _ObjectService;
        private readonly IGoodsService _GoodsService;

        public ProductController(IObjectService objectService, IGoodsService goodsService)
        {
            _ObjectService = objectService;
            _GoodsService = goodsService;
        }
        protected override void Dispose(bool disposing)
        {
            this._ObjectService.Dispose();
            this._GoodsService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult ProductList()
        {
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.PageIndex = 1;
            string txtName = Request.Params["txtName"] == null ? "" : Request.Params["txtName"];
            if (!string.IsNullOrEmpty(txtName))
            {
                ViewBag.txtName = txtName;
            }
            SearchCriteria.Name = txtName;
            SearchCriteria.Type = (int)CommonEnum.TypeOfDbObject.Product;
            var model = _ObjectService.SearchCategory(SearchCriteria);
            return View(model);
        }
        public ActionResult ProductAdd()
        {
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.Type = (int)CommonEnum.TypeOfDbObject.Classification;
            SearchCriteria.ParentId = -1;
            var attrsTypes = this._ObjectService.SearchCategory(SearchCriteria).Item1;
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.Id.ToString()
                            }).ToList();
            if (typelist.Count == 0)
            {
                typelist.Add(new SelectListItem() { Text = "无", Value = "-1" });
            }
            ViewBag.shopType = typelist;
            var model = new ProductViewModel();
            return View(model);
        }
        [HttpPost]
        [ActionName("ProductAdd")]
        public ActionResult AddPost(ProductViewModel model)
        {
            C_Object Cmodel = new C_Object();
            Cmodel.Name = model.Name;
            Cmodel.ParentId = model.ParentId;
            Cmodel.Sort = model.Sort;
            Cmodel.Creater = UserId;
            Cmodel.CreateTime = DateTime.Now;
            Cmodel.LastModifyTime = DateTime.Now;
            Cmodel.LastModifyUser = UserId;
            Cmodel.Status = 0;
            Cmodel.Type = (int)CommonEnum.TypeOfDbObject.Product;
            Cmodel.IsDelete = false;
            this._ObjectService.Insert(Cmodel);

            C_File imgmodel = new C_File();
            imgmodel.FileName = model.Image;
            this._ObjectService.InsertImage(imgmodel);

            G_Name Gmodel = new G_Name();
            Gmodel.Id = Cmodel.Id;
            Gmodel.Unit = model.Unit;
            Gmodel.Image = imgmodel.Id;
            this._GoodsService.InsertUnit(Gmodel);

            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.ParentId = -1;
            SearchCriteria.Type = (int)CommonEnum.TypeOfDbObject.Classification;
            var attrsTypes = this._ObjectService.SearchCategory(SearchCriteria).Item1;
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.Id.ToString()
                            }).ToList();
            ViewBag.shopType = typelist;
            ViewBag.msg = "添加成功";
            return View(model);
        }
        public ActionResult ProductEdit(int Id)
        {
            var model = this._ObjectService.GetById(Id);
            ProductViewModel Product = new ProductViewModel();
            Product.Id = model.Id;
            Product.Name = model.Name;
            Product.ParentId = model.ParentId;
            Product.Sort = model.Sort;
            Product.Unit = model.G_Name.Unit;
            Product.Image = model.G_Name.C_File.FileName;
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.ParentId = -1;
            SearchCriteria.Type = (int)CommonEnum.TypeOfDbObject.Classification;
            var attrsTypes = this._ObjectService.SearchCategory(SearchCriteria).Item1;
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.Id.ToString()
                            }).ToList();
            ViewBag.shopType = typelist;

            return View(Product);
        }
        [HttpPost]
        [ActionName("ProductEdit")]
        public ActionResult EditPost(ProductViewModel model)
        {

            var Cmodel = this._ObjectService.GetById(model.Id);
            Cmodel.Name = model.Name;
            Cmodel.ParentId = model.ParentId;
            Cmodel.Sort = model.Sort;
            Cmodel.LastModifyTime = DateTime.Now;
            Cmodel.LastModifyUser = UserId;
            this._ObjectService.Update(Cmodel);

            C_File imgmodel = this._ObjectService.GetImageById(Cmodel.G_Name.Image);
            imgmodel.FileName = model.Image;
            imgmodel.Id = Cmodel.G_Name.Image;
            this._ObjectService.UpdateImage(imgmodel);

            G_Name Gmodel = this._GoodsService.GetById(Cmodel.Id);
            Gmodel.Unit = model.Unit;
            Gmodel.Image = imgmodel.Id;
            this._GoodsService.UpdateUnit(Gmodel);

            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.ParentId = -1;
            SearchCriteria.Type = (int)CommonEnum.TypeOfDbObject.Classification;
            var attrsTypes = this._ObjectService.SearchCategory(SearchCriteria).Item1;
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.Id.ToString()
                            }).ToList();
            ViewBag.shopType = typelist;
            this._ObjectService.Update(Cmodel);
            ViewBag.msg = "修改成功";
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Postimg(ProductViewModel moudel)
        {
            string Viewname = "";
            var fileCICName = UploadImgUtility.UploadImage(Request.Files["Imagefile"], Server.MapPath(CommonContorllers.FileUploadGoodsImgPath));
            if (!string.IsNullOrEmpty(fileCICName))
            {
                moudel.Image = CommonContorllers.FileUploadGoodsImgPath + fileCICName;

            }
            if (moudel.Id > 0)
            {
                Viewname = "ProductEdit";
            }
            else
            {
                Viewname = "ProductAdd";
            }
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.Type = (int)CommonEnum.TypeOfDbObject.Classification;
            SearchCriteria.ParentId = -1;
            var attrsTypes = this._ObjectService.SearchCategory(SearchCriteria).Item1;
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.Id.ToString()
                            }).ToList();
            if (typelist.Count == 0)
            {
                typelist.Add(new SelectListItem() { Text = "无", Value = "-1" });
            }
            ViewBag.shopType = typelist;
            return View(Viewname, moudel);

        }
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult GetNodelist(int id)
        {
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.Type = (int)CommonEnum.TypeOfDbObject.Classification;
            SearchCriteria.ParentId = id;
            var attrsTypes = _ObjectService.SearchCategory(SearchCriteria).Item1;
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.Name,
                                Value = m.Id.ToString()
                            }).ToList();
            return Json(typelist, JsonRequestBehavior.DenyGet);

        }
    }
}
