
using YG.SC.Service.IService;
using YG.US.Common;

namespace YG.SC.WebUI.Controllers
{
    using System;
    using System.Web.Mvc;
    using YG.SC.Common;
    using YG.SC.DataAccess;


    /// <summary>
    /// 类名称：AdPictureController
    /// 命名空间：YG.SC.WebUI.Controllers
    /// 类功能：Banner图控制器
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/10/23 11:22
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class ProjectPhotoController : WebBaseController
    {
        private static int _shopProjectId;
        private readonly IShopProjectService _IShopProjectService;
        private readonly IProjectPhotoService _IProjectPhotoService;
        public ProjectPhotoController(IProjectPhotoService iProjectPhotoService, IShopProjectService iShopProjectService)
        {
            _IProjectPhotoService = iProjectPhotoService;
            _IShopProjectService = iShopProjectService;
        }

        /// <summary>
        /// 释放非托管资源和托管资源（后者为可选项）。
        /// </summary>
        /// <param name="disposing">若为 true，则同时释放托管资源和非托管资源；若为 false，则仅释放非托管资源。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/15 16:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            this._IProjectPhotoService.Dispose();
            this._IShopProjectService.Dispose();
            base.Dispose(disposing);
        }
      
        
        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="pg">The pg</param>
        /// <param name="txtImageName">Name of the text image.</param>
        /// <param name="selRecsts">The selRecsts</param>
        /// <returns>
        /// The ActionResult
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/22 16:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public ActionResult Index(int pg = 1,int ShopProjectId=0, string txtImageName = "")
        {
            _shopProjectId = ShopProjectId;
            //ViewBag.SelRecsts = TempData["SelRecsts"] ?? selRecsts;
            //ViewBag.ShopProject = this._IShopProjectService.GetAll();
            var model = this._IProjectPhotoService.GetEntitsByImageName(pg,ShopProjectId, txtImageName);
            return View(model);
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="sctAdPicture">The sctAdPicture</param>
        /// <returns>
        /// The ActionResult
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/22 16:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Add(ProjectPhoto sctAdPicture)
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
        public ActionResult AddPost(ProjectPhoto projectPhoto)
        {
            var fileName = UploadImgUtility.UpLoadBannerImage(Request.Files["PictureImg"], Server.MapPath(CommonContorllers.FileUploadProjectPhotoSmallPath), Server.MapPath(CommonContorllers.FileUploadProjectPhotoPath));
            projectPhoto.PhotoUrl = CommonContorllers.FileUploadProjectPhotoPath + fileName;
            projectPhoto.PhotoUrlSmall = CommonContorllers.FileUploadProjectPhotoSmallPath +fileName;
            projectPhoto.ShopProjectId = _shopProjectId;
            projectPhoto.CreatTime = DateTime.Now;
            projectPhoto.Recsts = 1;
            this._IProjectPhotoService.Insert(projectPhoto);

            return Redirect("Index?ShopProjectId="+_shopProjectId);
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
            var entity = this._IProjectPhotoService.GetById(id);
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
        public ActionResult EditPost(ProjectPhoto projectPhoto)
        {

            var entity = this._IProjectPhotoService.GetById(projectPhoto.Id);

            var fileName = UploadImgUtility.UpLoadBannerImage(Request.Files["PictureImg"], Server.MapPath(CommonContorllers.FileUploadProjectPhotoSmallPath), Server.MapPath(CommonContorllers.FileUploadProjectPhotoPath));
            if (!string.IsNullOrEmpty(fileName))
            {
                entity.PhotoUrl = CommonContorllers.FileUploadProjectPhotoPath + fileName;
                entity.PhotoUrlSmall = CommonContorllers.FileUploadProjectPhotoSmallPath + fileName;
            }
            entity.PhotoName = projectPhoto.PhotoName;
            entity.Type = projectPhoto.Type;
            entity.Recsts = projectPhoto.Recsts;
            entity.Sort = projectPhoto.Sort;
            this._IProjectPhotoService.Update(entity);

            return Redirect("/ProjectPhoto/Index?ShopProjectId=" + entity.ShopProjectId);
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id">The Id</param>
        /// <param name="state">The state</param>
        /// 创建者：边亮
        /// 创建日期：2014/10/17 14:35
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Delete(int id, string state)
        {
            var entity = this._IProjectPhotoService.GetById(id);

            var recsts = state == "delete" ? -1: 0;
            entity.Recsts = recsts;
            this._IProjectPhotoService.Update(entity);
            TempData["SelRecsts"] = entity.Recsts;
        }
        /// <summary>
        /// 修改图片为有效
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="state">The state</param>
        /// 创建者：边亮
        /// 创建日期：2014/10/28 11:50
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Update(int id)
        {
            var entity = this._IProjectPhotoService.GetById(id);
            if (entity != null)
            {
                entity.Recsts = 1;
                entity.CreatTime = DateTime.Now;
                this._IProjectPhotoService.Update(entity);
                TempData["SelRecsts"] = entity.Recsts;
            }

        }
    }
}
