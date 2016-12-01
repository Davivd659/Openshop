using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
   public interface ICartGoodsService:IYgService
    {

        void Insert(O_CartGoods model);
        void Update(O_CartGoods model);
        O_CartGoods GetById(int id);
        Tuple<O_CartGoods[], PagerEntity> SearchCart(CartSearchCriteria criteria);
    }
}
