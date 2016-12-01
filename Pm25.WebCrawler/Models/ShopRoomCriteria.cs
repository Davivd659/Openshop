using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pm25.WebCrawler.Models
{
    public class ShopRoomCriteria
    {
        public string city { get; set; }
        public int from { get; set; }
        public int PageNo { get; set; }
        public int perPageCount { get; set; }
    }
}