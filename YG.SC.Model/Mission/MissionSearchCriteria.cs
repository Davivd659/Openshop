using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Common;

namespace YG.SC.Model
{
    public class MissionSearchCriteria : PagerSearchCriteria
    {
        /// <summary>
        /// 关键词（任务标题 || 接单商家名称）（模糊）。
        /// </summary>
        public string Keys { get; set; }

        /// <summary>
        /// 任务类型。
        /// </summary>
        public int? MissionType { get; set; }

        /// <summary>
        /// 用户角色。
        /// </summary>
        public CommonEnum.GroupOfCustomer Role { get; set; }

        /// <summary>
        /// 用户编号。
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// 发布时间左边界（含）。
        /// </summary>
        public string IssueDateLeft { get;set; }

        /// <summary>
        /// 发布时间右边界（含）。
        /// </summary>
        public string IssueDateRight { get; set; }

        /// <summary>
        /// 任务状态。（0：进行中；1：已完成；2：已取消。）
        /// </summary>
        public int? MissionStatus { get; set; }

		/// <summary>
		/// 确认筛选出待处理任务。
		/// </summary>
		public bool? IsPending { get; set; }

    }
}
