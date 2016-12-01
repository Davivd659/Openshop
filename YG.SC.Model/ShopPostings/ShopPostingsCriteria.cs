using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class ShopPostingsCriteria
    {

        public string city { get; set; }
        public string distinct { get; set; }
        public string PType { get; set; }
        // public string type { get; set; }
        /// <summary>
        /// 求租，求购
        /// </summary>
        public string isSale { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int? AreaId { get; set; }
        public int? PriceRentId { get; set; }
        public int? PriceSaleId { get; set; }
        

        public string Keys { get; set; }
    
    }
}
