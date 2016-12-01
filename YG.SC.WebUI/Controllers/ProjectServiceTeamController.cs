using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper.Internal;
using YG.SC.Common;
using YG.SC.Common.AttributeExtention;
using YG.SC.DataAccess;
using YG.SC.Model.Project;
using YG.SC.Service.IService;

namespace YG.SC.WebUI.Controllers
{
    /// <summary>
    /// 项目服务团队
    /// </summary>
    public class ProjectServiceTeamController : Controller
    {
        //
        // GET: /ProjectServiceTeam/
        private readonly IShopProjectService _IShopProjectService;
        private readonly IProjectPhotoService _IProjectPhotoService;
        private readonly IProjectTeamService _iProjectTeamService;
        public ProjectServiceTeamController(IProjectPhotoService iProjectPhotoService, IShopProjectService iShopProjectService, IProjectTeamService iProjectTeamService)
        {
            _IProjectPhotoService = iProjectPhotoService;
            _IShopProjectService = iShopProjectService;
            _iProjectTeamService = iProjectTeamService;
        }
        public ActionResult Index(int pg = 1, string txtProjectName = "")
        {
            ViewBag.ShopProject = _iProjectTeamService.GetAll();
            var model = _iProjectTeamService.GetEntitsByImageName(pg, txtProjectName);
            return View(model);
        }

        public ActionResult Add()
        {
            var model = new ProjectServiceViewModel();
            var selectitemlist = new List<SelectListItem>();
            var projectlist = _IShopProjectService.GetAll();
            foreach (var project in projectlist)
            {
                var selectitem = new SelectListItem();
                selectitem.Value = project.Id.ToString();
                selectitem.Text = project.NAME;
                selectitemlist.Add(selectitem);
            }
            model.ProjectList = selectitemlist;
            return View(model);
        }
        public ActionResult AddServiceTeam(FormCollection collection)
        {
            ProjectService projectService = new ProjectService();
            //projectService.
            var model = projectService;
            _iProjectTeamService.Insert(model);
 
            return View(model);
        }

        public ActionResult AddTeam(FormCollection collection)
        {
            var name = collection["Name"];
            var projectId = collection["ProjectList"];
            var isVip = collection["IsVip"];
            var picUrl = Request.Files[0];

            ProjectService ps = new ProjectService();
            if (isVip != null && isVip.Contains("true"))
                ps.IsMvp = true;
            else
                ps.IsMvp = false;
            ps.Name = name;
            ps.ShopProjectId = Convert.ToInt32(projectId);
            ps.Status = 1;

            var fileName = UploadImgUtility.UpLoadBannerImage(picUrl, Server.MapPath(CommonContorllers.FileUploadProjectPhotoSmallPath), Server.MapPath(CommonContorllers.FileUploadProjectPhotoPath));
            ps.PicUrl = CommonContorllers.FileUploadProjectPhotoPath + fileName;
            
            _iProjectTeamService.Insert(ps);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = new ProjectServiceViewModel();
            var selectitemlist = new List<SelectListItem>();
            var projectlist = _IShopProjectService.GetAll();
            
            
            var team = _iProjectTeamService.GetById(id);
            model.Id = team.Id;
            if (team.IsMvp != null) 
                model.IsVip = team.IsMvp.Value;
            model.Name = team.Name;

            foreach (var project in projectlist)
            {
                var selectitem = new SelectListItem();
                if (project.Id == team.ShopProjectId)
                {
                    selectitem.Selected = true;
                    model.ProjectListId = project.Id.ToString();
                }
                selectitem.Value = project.Id.ToString();
                selectitem.Text = project.NAME;
                selectitemlist.Add(selectitem);
            }
            model.ProjectList = selectitemlist;
            
            return View(model);
        }

        public ActionResult EditTeam(FormCollection collection)
        {
            var id = collection["Id"];
            var model = _iProjectTeamService.GetById(Convert.ToInt32(id));
            var name = collection["Name"];
            var projectId = collection["ProjectList"];
            var isVip = collection["IsVip"];
            var picUrl = Request.Files[0];
            model.Name = name;
            model.ShopProjectId = Convert.ToInt32(projectId);
            if (isVip != null && isVip.Contains("true"))
                model.IsMvp = true;
            else
                model.IsMvp = false;
            var fileName = UploadImgUtility.UpLoadBannerImage(picUrl, Server.MapPath(CommonContorllers.FileUploadProjectPhotoSmallPath), Server.MapPath(CommonContorllers.FileUploadProjectPhotoPath));
            model.PicUrl = CommonContorllers.FileUploadProjectPhotoPath + fileName;
            _iProjectTeamService.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id,string state)
        {
            var entity = _iProjectTeamService.GetById(id);
            entity.Status = 0;
            _iProjectTeamService.Update(entity);
            return RedirectToAction("Index");
        }
    }
}
