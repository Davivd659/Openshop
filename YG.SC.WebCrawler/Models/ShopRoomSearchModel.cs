using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebCrawler.Models
{
    public class ShopRoomSearchModel
    {
        public string errorMessage { get; set; }
        public int errorCode { get; set; }
        public string lastTime { get; set; }
        public string firstTime { get; set; }

        public List<ShopRoomModel> shops { get; set; }
    }
}