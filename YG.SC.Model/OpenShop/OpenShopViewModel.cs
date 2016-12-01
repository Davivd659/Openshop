using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YG.SC.Model
{
    public class OpenShopViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入商家名称")]
        public string Name { get; set; }
        [Required(ErrorMessage = "请输入商家简称")]
        public string Abbreviation { get; set; }
        [Required(ErrorMessage = "请输入商家介绍")]
        public string Introduction { get; set; }

        public string Rmark { get; set; }

        [Required(ErrorMessage = "请上传logo")]
        public string Logo { get; set; }
        public string QRcode { get; set; }
        [Required(ErrorMessage = "请上传营业执照扫描件")]
        public string BLS { get; set; }
        [Required(ErrorMessage = "请上传商家法人身份证正面")]
        public string CIC { get; set; }
        [Required(ErrorMessage = "请输入商家网址")]
        public string Url { get; set; }

        public string VidoUrl { get; set; }

        public int Recsts { get; set; }
        public int Districtid { get; set; }
        public int Rangeid { get; set; }

        public System.DateTime CreateTime { get; set; }

        public Nullable<int> Type { get; set; }
        [Required(ErrorMessage = "请输入联系人电话")]
        public string Mobile { get; set; }

        public int viewclick { get; set; }
        [Required(ErrorMessage = "请输入商家地址")]
        public string Address { get; set; }

        public bool Isguarantee { get; set; }

        public string Size { get; set; }
        [DisplayName("类型")]
        public string TypeId { get; set; }
        [Required(ErrorMessage = "请输入联系人姓名")]
        public string Customer { get; set; }
        [Required(ErrorMessage = "请输入账户密码")]
        public string Password { get; set; }
    }
}
