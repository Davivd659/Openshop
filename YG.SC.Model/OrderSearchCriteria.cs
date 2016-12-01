using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class OrderSearchCriteria
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keys { get; set; }
    }
}
