
namespace YG.SC.WebAPIService.Models
{
    public class UserInfoModel
    {
        /// <summary>
        /// Gets or sets the UserToken of UserInfoModel
        /// </summary>
        /// <value>
        /// The UserToken
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/14 16:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string UserToken { get; set; }  
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <value>
        /// The Company
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/13 15:33
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Company { get; set; }
        /// <summary>
        /// true 已经完善 false 需要完善
        /// </summary>
        /// <value>
        /// The IsPerfect
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/13 15:34
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool IsPerfect { get; set; }
        /// <summary>
        ///审核状态
        /// </summary>
        /// <value>
        /// The UserStatus
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/13 15:34
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string UserStatus { get; set; }

        /// <summary>
        /// Gets or sets the UserStatusCode of UserInfoModel
        /// </summary>
        /// <value>
        /// The UserStatusCode
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/14 10:36
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int UserStatusCode { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/13 15:34
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string CustomerName { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        /// <value>
        /// The CustomerPhone
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/13 15:34
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string CustomerPhone { get; set; }
        public string Gender { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int CountyId { get; set; }
        /// <summary>
        /// 地址详情
        /// </summary>
        /// <value>
        /// The AddressDetail
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/13 15:35
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string AddressDetail { get; set; }
        /// <summary>
        ///收货详细地址
        /// </summary>
        /// <value>
        /// The CustomerAddress
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/13 15:35
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string CustomerAddress { get; set; }

    }
}