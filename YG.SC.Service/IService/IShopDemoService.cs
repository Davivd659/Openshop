using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YG.SC.DataAccess;
using YG.SC.Model;
namespace YG.SC.Service.IService
{
    public interface IShopDemoService : IYgService
    {
        Tuple<ShopDemo[], PagerEntity> GetEntitsByName(int pg, string Name, string selRecsts);
        ShopDemo GetById(int id);
        void Update(ShopDemo demo);
        void Insert(ShopDemo demo);

    }
}
