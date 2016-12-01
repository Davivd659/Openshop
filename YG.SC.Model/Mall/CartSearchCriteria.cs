using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model
{
    public class CartSearchCriteria
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int Id { get; set; }

        public int Creater { get; set; }

        public DateTime Begintime { get; set; }

        public DateTime Endtime { get; set; }
        public int Flag { get; set; }


        #region 购物车商品
        public int CartId { get; set; }

        public int GoodsId { get; set; }

        public string GoodsName { get; set; }
        public decimal GoodsCount { get; set; }

        public decimal Price { get; set; }
        #endregion
    }
}
