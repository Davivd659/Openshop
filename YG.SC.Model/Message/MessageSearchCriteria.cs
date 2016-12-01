using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Common;

namespace YG.SC.Model
{
    public class MessageSearchCriteria : PagerSearchCriteria
    {
        /// <summary>
        /// 消息内容（模糊）。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 接收者姓名（模糊）。
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 发送日期左边界（含）。
        /// </summary>
        public DateTime? SendDateLeft { get; set; }

        /// <summary>
        /// 发送日期右边界（含）。
        /// </summary>
        public DateTime? SendDateRight { get; set; }

        /// <summary>
        /// 消息来源。
        /// </summary>
        public CommonEnum.TypeOfMessage? MessageSource { get; set; }

        /// <summary>
        /// 关键词（商家姓名 || 商家电话）。
        /// </summary>
        public string Key { get; set; }

        public int UserId { get; set; }

        public CommonEnum.GroupOfCustomer Role { get; set; }
    }
}
