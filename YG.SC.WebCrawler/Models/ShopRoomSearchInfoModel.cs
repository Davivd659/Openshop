using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebCrawler.Models
{
    public class ShopRoomSearchInfoModel
    {
        public string errorMessage { get; set; }
        public ShopRoomModel shop { get; set; }
        public int errorCode { get; set; }
    }
}