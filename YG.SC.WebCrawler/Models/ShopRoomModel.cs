using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebCrawler.Models
{
    public class ShopRoomModel
    {
        public string zhuanrangfei { get; set; }
        public bool sale { get; set; }
        public string id { get; set; }
        public string xiaoqu { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string hotArea { get; set; }
        public string address { get; set; }
        public int houseType { get; set; }
        public decimal square { set; get; }
        public decimal price { get; set; }
        public int rooms { get; set; }
        // "lobby": 0,
        // toilet": 0,
        // "kitchen": 0,
        // "balcony": 0,
        public string title { get; set; }
        public string description { get; set; }
        public string contact { get; set; }
        public string mobile { get; set; }
        public int from { get; set; }
        public string source { get; set; }
        public long fetchDate { get; set; }

        public string type { get; set; }
        //"shop": {
        //"type": "商业街卖场",
        //"historicalOperating": "",
        


    }
}