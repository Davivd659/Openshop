using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class OpenShopSearchCriteria
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keys { get; set; }
        /// <summary>
        /// 类型（装修帮、家具帮、注册帮）
        /// </summary>
        public int? TypeId { get; set; }
        public int? AreaId { get; set; }
        public int? HotId { get; set; }

    }
}
