using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model.AdPosition
{
    /// <summary>
    /// 广告排期列表页
    /// </summary>
    public class PositonIndexViewModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string TypeId { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [DisplayName("位置")]
        public string PositionId { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [DisplayName("日期")]
        public DateTime PositonDate { get; set; }


    }
}
