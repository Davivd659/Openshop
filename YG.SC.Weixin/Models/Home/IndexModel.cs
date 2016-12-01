using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WeiXin.Models
{
    public class HomeIndexModel
    {
        public List<YG.SC.DataAccess.ShopAdPosition> AdHaoDian { get; set; }
        public List<YG.SC.DataAccess.ShopAdPosition> AdXuanZhi { get; set; }
        public List<YG.SC.DataAccess.ShopAdPosition> AdKaiDianLeft { get; set; }
        public List<YG.SC.DataAccess.ShopAdPosition> AdKaiDianRight { get; set; }
        public List<YG.SC.DataAccess.ShopAdPosition> AdBrand { get; set; }

    }
}