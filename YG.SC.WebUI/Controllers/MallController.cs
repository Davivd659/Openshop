using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Service.IService;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Common;

namespace YG.SC.WebUI.Controllers
{
    public class MallController : WebBaseController
    {
        private readonly IObjectService _ObjectService;
        public MallController(IObjectService objectService)
        {
            _ObjectService = objectService;
        }
        protected override void Dispose(bool disposing)
        {
            this._ObjectService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult CategoryList()
        {
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            string txtName = Request.Params["txtName"] == null ? "" : Request.Params["txtName"];
            if (!string.IsNullOrEmpty(txtName))
            {
                ViewBag.txtName = txtName;
            }
            SearchCriteria.Name = txtName;
            SearchCriteria.PageIndex = 1;
            SearchCriteria.Type = (int)CommonEnum.TypeOfDbObject.Classification;
            var model = _ObjectService.SearchCategory(SearchCriteria);
            return View(model);
        }
        public ActionResult CategoryAdd()
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
            typelist.Add(new SelectListItem() { Text = "无", Value = "-1" });
            ViewBag.shopType = typelist;
            return View();
        }
        [HttpPost]
        [ActionName("CategoryAdd")]
        public ActionResult AddPost(C_Object model)
        {
            model.Creater = UserId;
            model.CreateTime = DateTime.Now;
            model.LastModifyTime = DateTime.Now;
            model.LastModifyUser = UserId;
            model.Status = 0;
            model.Type = (int)CommonEnum.TypeOfDbObject.Classification;
            model.IsDelete = false;
            this._ObjectService.Insert(model);
            if (model.ParentId != -1)
            {
                model.ParentChain = "|" + model.ParentId + "|" + model.Id + "|";
            }
            else
            {
                model.ParentChain = "|" + model.Id + "|";
            }
            this._ObjectService.Update(model);


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
            typelist.Add(new SelectListItem() { Text = "无", Value = "-1" });
            ViewBag.shopType = typelist;
            ViewBag.msg = "添加成功";
            return View(model);
        }
        public ActionResult CategoryEdit(int Id)
        {

            var model = this._ObjectService.GetById(Id);
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
            typelist.Add(new SelectListItem() { Text = "无", Value = "-1" });
            ViewBag.shopType = typelist;
            return View(model);
        }
        [HttpPost]
        [ActionName("CategoryEdit")]
        public ActionResult EditPost(C_Object model)
        {

            var Cmodel = this._ObjectService.GetById(model.Id);
            Cmodel.Name = model.Name;
            Cmodel.ParentId = model.ParentId;
            Cmodel.Sort = model.Sort;
             Cmodel.LastModifyTime = DateTime.Now;
            Cmodel.LastModifyUser = UserId;
            if (Cmodel.ParentId != -1)
            {
                Cmodel.ParentChain = "|" + model.ParentId + "|" + model.Id + "|";
            }
            else
            {
                Cmodel.ParentChain = "|" + model.Id + "|";
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
            typelist.Add(new SelectListItem() { Text = "无", Value = "-1" });
            ViewBag.shopType = typelist;
            this._ObjectService.Update(Cmodel);
            ViewBag.msg = "修改成功";
            return View(model);
        }
    }
}
