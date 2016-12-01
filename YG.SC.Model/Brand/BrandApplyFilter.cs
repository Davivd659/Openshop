using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Common;

namespace YG.SC.Model
{
    /// <summary>
    /// 品牌申请查询筛选。
    /// </summary>
    public class BrandApplyFilter : PagerSearchCriteria
    {
        /// <summary>
        /// 申请日期左边界（含）。
        /// </summary>
        public DateTime? ApplyTimeLeft { get; set; }

        /// <summary>
        /// 申请日期右边界（含）。
        /// </summary>
        public DateTime? ApplyTimeRight { get; set; }

        /// <summary>
        /// 申请结果。
        /// </summary>
        public CommonEnum.StatusOfBrandApply? Status { get; set; }

        /// <summary>
        /// 品牌名称。
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 申请人。
        /// </summary>
        public int ApplyUserId { get; set; }

        /// <summary>
        /// 审批人。
        /// </summary>
        public int AuditUserId { get; set; }

        /// <summary>
        /// 关键词。
        /// </summary>
        public string Key { get; set; }
    }
}
