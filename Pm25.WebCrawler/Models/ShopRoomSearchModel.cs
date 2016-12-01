using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pm25.WebCrawler.Models
{
    public class ShopRoomSearchModel
    {
        public string errorMessage { get; set; }
        public string errorCode { get; set; }
        public string lastTime { get; set; }
        public string firstTime { get; set; }

        public List<ShopRoomModel> shops { get; set; }
    }
}