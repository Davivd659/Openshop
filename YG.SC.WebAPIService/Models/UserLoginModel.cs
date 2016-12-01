
namespace YG.SC.WebAPIService.Models
{
    public class UserLoginModel
    {
        /// <summary>
        /// Gets or sets the UserToken of UserLoginModel
        /// </summary>
        /// <value>
        /// The UserToken
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/13 10:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string UserToken { get; set; }

        /// <summary>
        /// 是否完善过信息true 完善过 false 没有完善过
        /// </summary>
        /// <value>
        /// The IsPerfect
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/13 10:50
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool IsPerfect { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        /// <value>
        /// The UserStatus
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/13 10:50
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string UserStatus { get; set; }

        /// <summary>
        /// 用户状态Code
        /// </summary>
        /// <value>
        /// The StatusCode
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/14 10:30
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int UserStatusCode { get; set; }
    }
}