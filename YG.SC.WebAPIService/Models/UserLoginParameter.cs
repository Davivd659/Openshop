
namespace YG.SC.WebAPIService.Models
{
    public class UserLoginParameter
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        /// <value>
        /// The Phone
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 18:55
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <value>
        /// The Code
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 18:55
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Code { get; set; }
    }
}