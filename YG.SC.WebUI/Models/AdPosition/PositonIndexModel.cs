using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Model.AdPosition;

namespace YG.SC.WebUI.Models.AdPosition
{
    public class PositonIndexModel
    {
        public Tuple<ShopAdPosition[], PagerEntity> ShopAdPositionList { get; set; }
        public Tuple<ShopProject[], PagerEntity> ShopProjectList { get; set; }

        public PositonIndexViewModel ViewModel { get; set; }

  
        public int TypeId { get; set; }
        /// <summary>
        /// 位置
        /// </summary> 
        public int PositionId { get; set; }
        /// <summary>
        /// 日期
        /// </summary> 
        public DateTime PositonDate { get; set; }
    }
}