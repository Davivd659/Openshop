using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YG.SC.DataAccess;

namespace YG.SC.OpenShop.Models
{
    public class Brand
    {
        /// <summary>
        /// 品牌编号。
        /// </summary>
        [DisplayName("品牌编号")]
        [Required]
        public int? Id { get; set; }

        /// <summary>
        /// 品牌名称。
        /// </summary>
        [DisplayName("品牌名称")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 业态。
        /// </summary>
        [DisplayName("业态")]
        public string Format { get; set; }

        /// <summary>
        /// 业态列表。
        /// </summary>
        public ICollection<ShopBrandAttributeValues> ShopBrandAttributeValues { get; set; }

        /// <summary>
        /// Logo。
        /// </summary>
        [DisplayName("Logo")]
        public string Logo { get; set; }

        /// <summary>
        /// 二维码。
        /// </summary>
        [DisplayName("二维码")]
        public string QRCode { get; set; }

        /// <summary>
        /// 网址。
        /// </summary>
        [DisplayName("网址")]
        [Required]
        public string WebUrl { get; set; }

        /// <summary>
        /// 视频地址。
        /// </summary>
        [DisplayName("视频地址")]
        [Required]
        public string VideoUrl { get; set; }

        /// <summary>
        /// 是否可加盟。
        /// </summary>
        [DisplayName("是否可加盟")]
        public bool IsAllowJoin { get; set; }

        /// <summary>
        /// 是否有效。
        /// </summary>
        [DisplayName("是否有效")]
        public bool IsValid { get; set; }

        /// <summary>
        /// 品牌介绍。
        /// </summary>
        [DisplayName("品牌介绍")]
        public string Introduce { get; set; }

    }
}