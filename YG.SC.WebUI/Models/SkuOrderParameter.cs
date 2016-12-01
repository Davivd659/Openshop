
namespace YG.SC.WebUI.Models
{
    /// <summary>
    /// 类名称：SkuOrderStatusParameter
    /// 命名空间：YG.SC.WebUI.Models
    /// 类功能：修改订单状态
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/10/21 11:39
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class SkuOrderStatusParameter
    {
        /// <summary>
        /// Gets or sets the Id of SkuOrderStatusParameter
        /// </summary>
        /// <value>
        /// The Id
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/21 11:39
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the FaqDesc of SkuOrderStatusParameter
        /// </summary>
        /// <value>
        /// The FaqDesc
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/21 11:40
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string FaqDesc { get; set; }

        /// <summary>
        /// Gets or sets the OrderCd of SkuOrderStatusParameter
        /// </summary>
        /// <value>
        /// The OrderCd
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/21 11:41
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string OrderCd { get; set; }

        /// <summary>
        //用户电话
        /// </summary>
        /// <value>
        /// The Phone
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/30 15:12
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Phone { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        /// <value>
        /// The OrderNo
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/30 15:16
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string OrderNo { get; set; }
    }
}