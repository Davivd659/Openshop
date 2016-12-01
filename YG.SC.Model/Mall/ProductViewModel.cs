using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class ProductViewModel
    {

        public int Id { get; set; }

        public int Type { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public string ParentChain { get; set; }

        public string ChildrenSet { get; set; }
        public string Unit { get; set; }
        public string Image { get; set; }

        public string Description { get; set; }

        public int Creater { get; set; }

        public System.DateTime CreateTime { get; set; }

        public int LastModifyUser { get; set; }

        public System.DateTime LastModifyTime { get; set; }

        public Nullable<int> Status { get; set; }

        public bool IsDelete { get; set; }

        public int Sort { get; set; }


    }
}
