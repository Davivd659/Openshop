using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class GrouppurchaseSearchCriteria
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public string Applicant { get; set; }
        public string Moble { get; set; }
    }
}
