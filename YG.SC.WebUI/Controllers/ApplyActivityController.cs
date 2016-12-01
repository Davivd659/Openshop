using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Service.IService;

namespace YG.SC.WebUI.Controllers
{
    public class ApplyActivityController : WebBaseController
    {
        //
        // GET: /ApplyActivity/

        private readonly IApplyActiviteService _ApplyActiviteService;
        private readonly IProjectTeamService _iProjectTeamService;
        public ApplyActivityController(IApplyActiviteService applyActiviteService, IProjectTeamService iProjectTeamService)
        {
            _ApplyActiviteService = applyActiviteService;
            _iProjectTeamService = iProjectTeamService;
        }

        public ActionResult Index(int id = 0)
        {
            Grouppurchase seach = _iProjectTeamService.TeamGetById(id);
            ApplyActivitySearchCriteria criteria = new ApplyActivitySearchCriteria();
            string pg = Request.Params["pg"] == null ? "0" : Request.Params["pg"];
            string ProjectName = Request.Params["ProjectName"] == null ? "" : Request.Params["ProjectName"];
            string UserName = Request.Params["UserName"] == null ? "" : Request.Params["UserName"];
            string Phone = Request.Params["Phone"] == null ? "" : Request.Params["Phone"];
            if (int.Parse(pg) > 0)
            {
                criteria.pg = int.Parse(pg);
            }
            else
            {
                criteria.pg = 1;
            }
            criteria.PageSize = 10;
            criteria.UserName = UserName;
            criteria.ProjectName = ProjectName;
            criteria.Phone = Phone;
            if (seach != null)
            {
                ViewBag.ProjectName = seach.ShopProject.NAME;
                criteria.GrouppurchaseId = seach.Id;
                criteria.BeginTime = seach.Begintime;
                criteria.EndTime = seach.Endtime;
            }
            var model = this._ApplyActiviteService.Search(criteria);
            return View(model);
        }

    }
}
