using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.OpenShop.Models
{
    public class HomeIndexModel
    {
        public List<YG.SC.DataAccess.ShopAdPosition> AdHaoDian { get; set; }
        public List<YG.SC.DataAccess.ShopAdPosition> AdXuanZhi { get; set; }
        public List<YG.SC.DataAccess.ShopAdPosition> ad_kaidian { get; set; }
        public List<YG.SC.DataAccess.ShopAdPosition> AdBrand { get; set; }

    }
}