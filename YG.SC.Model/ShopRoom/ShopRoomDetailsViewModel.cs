using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model.ShopRoom
{
    public class ShopRoomDetailsViewModel
    {
        public string Id { get; set; }
        public string RName { get; set; }
        #region 首页
        /// <summary>
        /// 行业
        /// </summary>
        //[DisplayName("行业")]
        //public string Trade { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        //[DisplayName("区域")]
        //public string Region { get; set; }
        /// <summary>
        /// 商圈
        /// </summary>
        //[DisplayName("商圈")]
        //public string TradingArea { get; set; }
        /// <summary>
        /// 环线
        /// </summary>
        //[DisplayName("环线")]
        //public string CircleLine { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        [DisplayName("售价(元/㎡)")]
        public decimal SalePrice { get; set; }
        /// <summary>
        /// 租价
        /// </summary>
        [DisplayName("租价(元/㎡.天)")]
        public decimal RentalPrice { get; set; }
        /// <summary>
        /// 地铁
        /// </summary>
        /// <summary>
        /// 状态：出租，出售 
        /// </summary>
        [DisplayName("状态")]
        public string State { get; set; }
        public string StateId { get; set; }
        /// <summary>
        /// 开盘时间
        /// </summary>
        [DisplayName("发布时间")]
        public DateTime AddTime { get; set; }
        #endregion
        public string city { get; set; }
        public string district { get; set; }
        public string hotarea { get; set; }

    }
}
