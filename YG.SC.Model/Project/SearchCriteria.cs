using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class ProjectSearchCriteria
    {
        public int PageIndex { get; set;}
        public int PageSize { get; set; }
        public string Keys { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public int? HangYeId { get; set; }
        public int? AreaId { get; set; }
        public int? SubWayId { get; set; }
        public int? WuYeleixingId { get; set; }
        public int? PriceRentId { get; set; }
        public int? PriceSaleId { get; set; }
        public double OpenTimeId { get; set; }
        public int? StatusID { get; set; }
        public int Projecthot { get; set; }

    }
}
