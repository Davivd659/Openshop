using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Service.IService;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.WebUI.Controllers
{
    public class ProjectTeamController : Controller
    {
        //
        // GET: /ProjectTeam/
        private readonly IShopProjectService _IProjectService;
        private readonly IProjectTeamService _iProjectTeamService;
        public ProjectTeamController(IShopProjectService iProjectService, IProjectTeamService iProjectTeamService)
        {
            _IProjectService = iProjectService;
            _iProjectTeamService = iProjectTeamService;
        }
        public ActionResult Index(int id = 0)
        {
            var project = _IProjectService.GetById(id);
            string txtName = Request.Params["txtName"] == null ? "" : Request.Params["txtName"];
            if (project != null)
            {
                ViewBag.ProjectName = project.NAME;
            }
            else
            {
                ViewBag.ProjectName = txtName;
            }
            GrouppurchaseSearchCriteria searchCriteria = new GrouppurchaseSearchCriteria();
            string pg = Request.Params["pg"] == null ? "0" : Request.Params["pg"];
            if (int.Parse(pg) > 0)
            {

                searchCriteria.PageIndex = int.Parse(pg);
            }
            else
            {

                searchCriteria.PageIndex = 1;
            }
            searchCriteria.PageSize = 6;
            searchCriteria.ProjectId = id;
            searchCriteria.ProjectName =txtName;
            var moudl = _iProjectTeamService.SearchProject(searchCriteria);
            return View(moudl);
        }
        public ActionResult Add()
        {
            var selectitemlist = new List<SelectListItem>();
            var projectlist = _IProjectService.GetAll();
            foreach (var project in projectlist)
            {
                var selectitem = new SelectListItem();
                selectitem.Value = project.Id.ToString();
                selectitem.Text = project.NAME;
                selectitemlist.Add(selectitem);
            }
            ViewBag.Project = selectitemlist;
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public ActionResult Addpost(Grouppurchase model)
        {
            model.Addtime = DateTime.Now;
            model.Lastupdate = DateTime.Now;
            model.Status = 0;
            model.progress = 0;
            _iProjectTeamService.TeamInsert(model);
            var selectitemlist = new List<SelectListItem>();
            var projectlist = _IProjectService.GetAll();
            foreach (var project in projectlist)
            {
                var selectitem = new SelectListItem();
                selectitem.Value = project.Id.ToString();
                selectitem.Text = project.NAME;
                selectitemlist.Add(selectitem);
            }
            ViewBag.Project = selectitemlist;
            ViewBag.msg = "增加成功";
            return View();
        }
        public ActionResult Edit(int id=0)
        {
            var model=_iProjectTeamService.TeamGetById(id);
            var project = _IProjectService.GetById(model.ShopProjectId);
            if (project != null)
            {
                ViewBag.ProjectName = project.NAME;
            }

            return View(model);
        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(Grouppurchase tuangou)
        {
           var  model= _iProjectTeamService.TeamGetById(tuangou.Id);
           model.Id = tuangou.Id;
           model.Name = tuangou.Name;
           model.Llimit = tuangou.Llimit;
           model.Begintime = tuangou.Begintime;
           model.Endtime = tuangou.Endtime;
            model.progress = tuangou.progress;
            model.Lastupdate = DateTime.Now;
            model.Status = tuangou.Status;
            model.progress = tuangou.progress;
            _iProjectTeamService.TeamUpdate(model);
            return Redirect("~/ProjectTeam/Index/"+model.ShopProjectId);
        }

    }
}
