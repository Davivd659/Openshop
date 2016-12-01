using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class JoinSearchCriteria
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keys { get; set; }

        public int ShopProjectId { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Source { get; set; }
        public Nullable<System.DateTime> Addtime { get; set; }

    }
}
