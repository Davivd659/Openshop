using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Common;

namespace YG.SC.Model
{
	public class OrderSearchResult
	{
		/// <summary>
		/// 订单编号。
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// 订单时间。
		/// </summary>
		public DateTime OrderTime { get; set; }

		/// <summary>
		/// 订单类型。
		/// </summary>
		public CommonEnum.OrderType OrderType { get; set; }

		/// <summary>
		/// 标题。
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 业务详情地址。
		/// </summary>
		public string DetailUrl { get; set; }

		/// <summary>
		/// 联系人。
		/// </summary>
		public string Contact { get; set; }

		/// <summary>
		/// 联系电话。
		/// </summary>
		public string Mobile { get; set; }

		/// <summary>
		/// 金额。
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// 状态。
		/// </summary>
		public CommonEnum.OrderStatus Status { get; set; }

		/// <summary>
		/// 业务数据类型。
		/// </summary>
		public CommonEnum.BusinessDataType BusinessDataType { get; set; }

		/// <summary>
		/// 业务数据编号。
		/// </summary>
		public int BusinessDataId { get; set; }
	}
}
