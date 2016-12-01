using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.DataAccess;
using YG.SC.Service.IService;

namespace YG.SC.WebUI.Controllers
{
    public class LinkController : WebBaseController
    {
        //
        // GET: /Link/

        private readonly ILinkService _IlinkService;
        public LinkController(ILinkService IlinkService)
        {
            _IlinkService = IlinkService;
        }
        public ActionResult Index(int pg = 1, string txtSearchCategoryName="",string selRecsts="")
        {
            var model = this._IlinkService.GetEntitsByName(pg, txtSearchCategoryName, selRecsts);
            //if(!string .IsNullOrEmpty(Request.QueryString["selRecsts"]))
            //{
            //    model = model.
            //}
            return View(model);
        }
        public void Delete(int id, string state)
        {
            var entity = this._IlinkService.GetById(id);

            var recsts = ((state == "delete") ? -1 : 0 );
            entity.Recsts = recsts;
            this._IlinkService.Update(entity);
            //TempData["SelRecsts"] = entity.Recsts;
        }
        public void Update(int id)
        {
            var entity = this._IlinkService.GetById(id);
            if (entity != null)
            {
                entity.Recsts = 1;
                this._IlinkService.Update(entity);
                // TempData["SelRecsts"] = entity.Recsts;
            }

        }
        [HttpGet]
        public ActionResult Add(Link link)
        {
            return View();
        }


        /// <summary>
        /// Adds the post.
        /// </summary>
        /// <param name="sctAdPicture">The sctAdPicture</param>
        /// <param name="selSkuGoods">The selSkuGoods</param>
        /// <returns>
        /// ActionResult
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/22 16:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [HttpPost]
        [ActionName("Add")]
        public ActionResult AddPost(Link link)
        {          
            link.Recsts = 1;
            this._IlinkService.Insert(link);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var entity = this._IlinkService.GetById(id);
            return View(entity);
        }

        /// <summary>
        /// Edits the post.
        /// </summary>
        /// <param name="sctAdPicture">The sctAdPicture</param>
        /// <returns>
        /// ActionResult
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/22 16:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(Link link)
        {

            var entity = this._IlinkService.GetById(link.Id);

            entity.NAME = link.NAME;
            entity.URL = link.URL;
            entity.LinkType = link.LinkType;
            entity.Recsts = link.Recsts;
            this._IlinkService.Update(entity);

            return RedirectToAction("Index");
        }
    }
}
