using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.OpenShop.Models.project
{
    public class ProjectApplyModel
    {
        public int ProjectId { get; set; }
        public int GrouppurchaseId { get; set; }
        public string ApplyName { get; set; }
        public string ApplyPhone { get; set; }
        public string GroupType { get; set; }       

    }
}