using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public interface IShopPostingsService : IYgService
    {
        Tuple<YG.SC.DataAccess.ShopPostings[], PagerEntity> GetSearch(ShopPostingsCriteria criteria);
        //ShopRoom GetByShopId(string shopid);

        List<ShopPostings> GetAll();
        List<ShopPostings> GetTop(int top);

        Tuple<ShopPostings[], PagerEntity> GetEntitsByName(int pg, string PName, string selRecsts);
        ShopPostings GetById(int id);
        void Update(ShopPostings Postings);
        void Insert(ShopPostings Postings);
    }
}
