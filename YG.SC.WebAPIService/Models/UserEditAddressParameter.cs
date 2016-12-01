﻿
namespace YG.SC.WebAPIService.Models
{
    /// <summary>
    /// 类名称：UserEditAddressParameter
    /// 命名空间：YG.SC.WebAPIService.Models
    /// 类功能：用户编辑地址 请求参数
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/10/8 14:58
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class UserEditAddressParameter
    {

        /// <summary>
        /// Gets or sets the UserToken of UserPerfectModel
        /// </summary>
        /// <value>
        /// The UserToken
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:03
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string UserToken { get; set; }

        /// <summary>
        /// Gets or sets the Id of UserEditAddressParameter
        /// </summary>
        /// <value>
        /// The Id
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/8 15:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int Id { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        /// <value>
        /// The Name
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:03
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Name { get; set; }

        /// <summary>
        /// 收货人电话 
        /// </summary>
        /// <value>
        /// The Phone
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:03
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Phone { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        /// <value>
        /// The AddressDetails
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string AddressDetails { get; set; }

        /// <summary>
        /// 省 id 
        /// </summary>
        /// <value>
        /// The ProvinceId
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int ProvinceId { get; set; }

        /// <summary>
        /// 市 id 
        /// </summary>
        /// <value>
        /// The CityId
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int CityId { get; set; }

        /// <summary>
        /// 区、县 id
        /// </summary>
        /// <value>
        /// The CountyId
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:05
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int CountyId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// </summary>
        /// <value>
        /// The IsDefault
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:05
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool IsDefault { get; set; }
                    
        /// <summary>
        ///0 女 1 男 
        /// </summary>
        /// <value>
        /// The Gender
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:07
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int Gender { get; set; }
    }

}