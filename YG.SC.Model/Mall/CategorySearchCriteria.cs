using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class CategorySearchCriteria
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Id { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public string ParentChain { get; set; }

        public string ChildrenSet { get; set; }

        public string Description { get; set; }

        public int? Status { get; set; }
    }
}
