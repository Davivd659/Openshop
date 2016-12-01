using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Common;

namespace YG.SC.Model
{
	public class OrderSearchFilter : PagerSearchCriteria
	{
		/// <summary>
		/// 资金流向。
		/// </summary>
		public CommonEnum.FundFlow? FundFlow { get; set; }

		/// <summary>
		/// 订单类型。
		/// </summary>
		public CommonEnum.OrderType? OrderType { get; set; }

		/// <summary>
		/// 订单日期左边界（含）。
		/// </summary>
		public DateTime? OrderTimeLeft { get; set; }

		/// <summary>
		/// 订单日期右边界（含）。
		/// </summary>
		public DateTime? OrderTimeRight { get; set; }

		/// <summary>
		/// 关键词（订单号/客户姓名/客户电话）。
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// 用户角色。
		/// </summary>
		public CommonEnum.GroupOfCustomer Role { get; set; }

		/// <summary>
		/// 用户编号。
		/// </summary>
		public int UserId { get; set; }
	}
}
