using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YG.SC.OpenShop.Models
{
    public class Demand
    {
        /// <summary>
        /// 意向类型。
        /// </summary>
        [DisplayName("意向类型")]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        [DisplayName("标题")]
        [Required(ErrorMessage = "忘记输入标题了哦~")]
        public string Title { get; set; }

        /// <summary>
        /// 自身业态。
        /// </summary>
        [DisplayName("自身业态")]
        [Required(ErrorMessage = "忘记输入业态了哦~")]
        public string Format { get; set; }

        /// <summary>
        /// 意向区域。
        /// </summary>
        [DisplayName("意向区域")]
        [Required(ErrorMessage = "忘记选择意向区域了哦~")]
        public string Area { get; set; }

        /// <summary>
        /// 需求面积。
        /// </summary>
        [DisplayName("需求面积")]
        [Required(ErrorMessage = "忘记添加需求面积了哦~")]
        public string Square { get; set; }

        /// <summary>
        /// 意向价格。
        /// </summary>
        [DisplayName("意向价格")]
        [Required(ErrorMessage = "忘记输入价格了哦~")]
        public string Price { get; set; }

        /// <summary>
        /// 意向物业。
        /// </summary>
        [DisplayName("意向物业")]
        [Required(ErrorMessage = "忘记选择意向物业了哦~")]
        public string PType { get; set; }

        /// <summary>
        /// 需求详情。
        /// </summary>
        [DisplayName("需求详情")]
        [Required(ErrorMessage = "忘记输入需求详情了哦~")]
        public string Details { get; set; }

        /// <summary>
        /// 截止日期。
        /// </summary>
        [DisplayName("截止日期")]
        [Required(ErrorMessage = "忘记输入截止日期了哦~")]
        [EndDate]
        //[Range(typeof(DateTime), "20150729", "2050-12-31", ErrorMessage = "截止日期不能早于今天。")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 联系人。
        /// </summary>
        [DisplayName("联系人")]
        [Required(ErrorMessage = "忘记输入联系人了哦~")]
        public string Contact { get; set; }

        /// <summary>
        /// 手机号码。
        /// </summary>
        [DisplayName("手机号码")]
        [Required(ErrorMessage = "忘记输入手机号码了哦~")]
        [RegularExpression(@"1\d{10}", ErrorMessage = "手机号码写错了哦~")]
        public string Mobile { get; set; }
    }
}