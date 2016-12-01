using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class ApplyBrandSearchCriteria
    {
        public int pg { get; set; }
        public int PageSize { get; set; }
        public string BrandName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }

        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
