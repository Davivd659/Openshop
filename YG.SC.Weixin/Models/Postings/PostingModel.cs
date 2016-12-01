using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WeiXin.Models
{
    public class PostingModel
    {
        /// <summary>
        /// 意向
        /// </summary>
        public string yixiang { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string PName { get; set; }
        /// <summary>
        /// 业态 
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string district { get; set; }

        public string price { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public string square { get; set; }

        public bool PIntent { get; set; }
        /// <summary>
        /// 需求详情
        /// </summary>
        public string Pinfo { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }
  
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 已向物业  写字楼 
        /// </summary>
        public string Ptype { get; set; }

        public string Addtiem { get; set; }
        public string Endtiem { get; set; }


    }
}