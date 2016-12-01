using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WeiXin.Models.project
{
    public class ApplyModelItem
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public byte Status { get; set; }
        public System.DateTime UpdateDate { get; set; }

        public string ProjectName { get; set; }
    }
}