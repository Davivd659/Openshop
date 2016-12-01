using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model.AdPosition
{
    /// <summary>
    /// 广告排期
    /// </summary>
    public class AdPositionViewModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        [DisplayName("类型")]
        public string Type { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [DisplayName("位置")]
        public string Position { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [DisplayName("图片")]
        public string Pic { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        [DisplayName("关键字")]
        public string KeyWords { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        [DisplayName("图片链接")]
        public string Url { get; set; }
        /// <summary>
        /// 有效开始时间
        /// </summary>
        [DisplayName("有效开始时间")]
        public string StartDateTime { get; set; }
    }
}
