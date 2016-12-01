
namespace YG.SC.Model
{
    public class OrderStatisticsEntity
    {
        /// <summary>
        /// Gets or sets the UserId of OrderStatisticsEntity
        /// </summary>
        /// <value>
        /// The UserId
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 11:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 11:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the UserPhone of OrderStatisticsEntity
        /// </summary>
        /// <value>
        /// The UserPhone
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 11:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string UserPhone { get; set; }

        /// <summary>
        /// 订单总数
        /// </summary>
        /// <value>
        /// The OrderTotal
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 11:05
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int? OrderTotal { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        /// <value>
        /// The OrderAmount
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 11:05
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public decimal? OrderTotalAmount { get; set; }
    }
}
