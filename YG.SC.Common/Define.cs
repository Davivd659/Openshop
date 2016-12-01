using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Common
{
    /// <summary>
    /// 常量定义。
    /// </summary>
    public class Define
    {
        /// <summary>
        /// 每页数据量。
        /// </summary>
		public const int PAGE_SIZE = 10;

		/// <summary>
		/// 日期格式（精确到分钟）。
		/// </summary>
		public const string DATE_FORMAT_MINUTE = "yyyy-MM-dd HH:mm";

		/// <summary>
		/// 日期格式（精确到日）。
		/// </summary>
		public const string DATE_FORMAT_DAY = "yyyy-MM-dd";

		/// <summary>
		/// 订单号前缀。
		/// </summary>
		public const string ORDER_NUMBER_PERFIX = "KD";

		/// <summary>
		/// 本平台消息账号（对应Customer表的Id）。
		/// </summary>
		public const int CURRENT_PLATFORM_MESSAGE_ACCOUNT = 51;

        /// <summary>
        /// 商家类型集合分组编号。
        /// </summary>
		public const int OPEN_SHOP_TYPE_GROUP_ID = 20;
    }
}