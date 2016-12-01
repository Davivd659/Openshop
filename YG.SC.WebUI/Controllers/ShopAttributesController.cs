using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.DataAccess;
using YG.SC.Service.IService;

namespace YG.SC.WebUI.Controllers
{
    public class ShopAttributesController : Controller
    {
        //
        // GET: /ShopAttributes/
        private static int _AttributesId;
        private readonly IShopAttributesService _iShopAttributesService;

        public ShopAttributesController(IShopAttributesService iShopAttributesService)
        {
            _iShopAttributesService = iShopAttributesService;
        }

        protected override void Dispose(bool disposing)
        {
            this._iShopAttributesService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index(int pg = 1, string txtName = "", string selRecsts = "")
        {
            var model = this._iShopAttributesService.GetEntitsByName(pg, txtName, selRecsts);
            //if(!string .IsNullOrEmpty(Request.QueryString["selRecsts"]))
            //{
            //    model = model.
            //}
            return View(model);
        }
        //public void Delete(int id)
        //{
        //    var entity = this._IlinkService.GetById(id);

        //    var recsts = ((state == "delete") ? -1 : 0);
        //    entity.Recsts = recsts;
        //    this._IlinkService.Update(entity);
        //    //TempData["SelRecsts"] = entity.Recsts;
        //}

        [HttpGet]
        public ActionResult Add(ShopAttributes sa)
        {
            return View();
        }


        [HttpPost]
        [ActionName("Add")]
        public ActionResult AddPost(ShopAttributes sa)
        {
            sa.TypeId = 1;
            sa.UsageMode = 1;
            sa.UseAttributeImage = true;
            sa.DisplaySequence = 100;
            this._iShopAttributesService.Insert(sa);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var entity = this._iShopAttributesService.GetById(id);
            return View(entity);
        }

     
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(ShopAttributes sa)
        {

            var entity = this._iShopAttributesService.GetById(sa.Id);

            entity.AttributeName = sa.AttributeName;
            entity.DisplaySequence = 100;
            entity.TypeId =1;
            entity.UsageMode = 1;
            entity.UseAttributeImage = true;
            this._iShopAttributesService.Update(entity);

            return RedirectToAction("Index");
        }
        //----------------ShopAttributesValue----------

        public ActionResult ShopAttributesValues(int AttributesId, int pg = 1, string txtName = "")
        {
            _AttributesId = AttributesId;
            var model = this._iShopAttributesService.GetShopAttributeValuesByName(AttributesId, pg, txtName);
      
            return View(model);
        }
        public void Delete(int id)
        {
            var entity = this._iShopAttributesService.GetShopAttributeValuesById(id);

            
            this._iShopAttributesService.Delete(entity);
            //TempData["SelRecsts"] = entity.Recsts;
        }

        [HttpGet]
        public ActionResult AddSAV(ShopAttributeValues sav)
        {
            return View();
        }


        
        [HttpPost]
        [ActionName("AddSAV")]
        public ActionResult AddSAVPost(ShopAttributeValues sav)
        {
       
            sav.DisplaySequence = 100;
            sav.AttributeId = _AttributesId;
            this._iShopAttributesService.Insert(sav);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditSAV(int id = 0)
        {
            var entity = this._iShopAttributesService.GetShopAttributeValuesById(id);
            return View(entity);
        }

        [HttpPost]
        [ActionName("EditSAV")]
        public ActionResult EditSAVPost(ShopAttributeValues sav)
        {
            var entity = this._iShopAttributesService.GetShopAttributeValuesById(sav.Id);

            entity.ValueStr = sav.ValueStr;
            entity.DisplaySequence = 100;
         
            this._iShopAttributesService.Update(entity);

            // return RedirectToAction("Index");
            // ShopAttributesValues?AttributesId=9 
            return RedirectToAction("ShopAttributesValues", new { AttributesId = entity.AttributeId });
        }
    }
}
