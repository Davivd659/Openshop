using System;

namespace YG.SC.WebAPIService.Models
{
   
    public class OrderPartModel
    {
        /// <summary>
        /// 订单号
        /// </summary>
        /// <value>
        /// The OrderNumber
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 16:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string OrderNumber { get; set; }
        /// <summary>
        ///订单状态
        /// </summary>
        /// <value>
        /// The OrderStatus
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 16:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string OrderStatus { get; set; }
        /// <summary>
        /// 订单状态名称
        /// </summary>
        /// <value>
        /// The name of the order status.
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 16:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string OrderStatusName { get; set; }
        /// <summary>
        ///订单创建时间
        /// </summary>
        /// <value>
        /// The CreateTime
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 16:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 餐厅（scmuser中的company 用户公司）
        /// </summary>
        /// <value>
        /// The Shop
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/11/27 15:42
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Shop { get; set; }
    }
}