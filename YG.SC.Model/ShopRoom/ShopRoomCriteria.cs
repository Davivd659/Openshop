using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class ShopRoomCriteria
    {
        public string city { get; set; }
        public string distinct { get; set; }

        // public string type { get; set; }
        /// <summary>
        /// 出租，出售
        /// </summary>
        public string isSale { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int? AreaId { get; set; }
        public int PriceRentId { get; set; }
        public int PriceSaleId { get; set; }

        public string Keys { get; set; }
        public string AddTime { get; set; }
        public int? CustomerId { get; set; }
        public int? isCert { get; set; }
        public double OpenTimeId { get; set; }
    }
}
