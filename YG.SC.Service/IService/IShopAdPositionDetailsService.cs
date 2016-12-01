using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public interface IShopAdPositionDetailsService
    {
        List<ShopAdPositionDetails> GetAll();
        Tuple<ShopAdPositionDetails[], PagerEntity> GetEntitsByImageName(int pg, string adName);
        ShopAdPositionDetails GetById(int id);
        void Update(ShopAdPositionDetails sp);
        void Insert(ShopAdPositionDetails sp);
    }
}
