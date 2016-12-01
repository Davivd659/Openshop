using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public interface IShopRoomService : IYgService
    {
        Tuple<YG.SC.DataAccess.ShopRoom[], PagerEntity> GetSearch(ShopRoomCriteria criteria);
        //ShopRoom GetByShopId(string shopid);

        List<ShopRoom> GetAll();
        List<ShopRoom> GetTop(int top);
        Tuple<ShopRoom[], PagerEntity> GetEntitsByImageName(int pg, string RName);
        ShopRoom GetById(int id);
        void Update(ShopRoom sp);
        void Insert(ShopRoom sp);

        Tuple<ShopRoom[], PagerEntity> GetEntitsByShopRoomName(int pg, string RName);

    }
}
