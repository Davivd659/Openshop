using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebAPIService.Models
{
    public class CheckGoods
    {
        public int GoodsId { get; set; }
        public string  GoodsName { get; set; }
        public decimal Price { get; set; }
        public string Recsts { get; set; }
    }
}