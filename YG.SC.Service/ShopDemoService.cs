using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;

namespace YG.SC.Service
{
    public class ShopDemoService : YG.SC.Service.IService.IShopDemoService
    {
        private readonly IRepository<ShopDemo> _ShopDemoRepository;

        public ShopDemoService(IRepository<ShopDemo> ShopDemoRepository)
        {
            _ShopDemoRepository = ShopDemoRepository;
        }

        public void Dispose()
        {
            this._ShopDemoRepository.Dispose();
        }


        public Tuple<ShopDemo[], PagerEntity> GetEntitsByName(int pg, string Name, string selRecsts)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopDemo, bool>> expressionFilter = (entity) => (string.IsNullOrEmpty(Name) || entity.Name.Contains(Name));
            if (!string.IsNullOrEmpty(selRecsts))
            {
                int Recsts = Convert.ToInt32(selRecsts);
                // expressionFilter = (entity) => (entity.Recsts == Recsts);
            }

            var total = this._ShopDemoRepository.Get(expressionFilter).Count();
            var array = this._ShopDemoRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }
        public ShopDemo GetById(int id)
        {
            return this._ShopDemoRepository.GetById(id);

        }
        public void Update(ShopDemo demo)
        {
            this._ShopDemoRepository.Update(demo);
            this._ShopDemoRepository.SaveChanges();
        }
        public void Insert(ShopDemo demo)
        {
            this._ShopDemoRepository.Insert(demo);
            this._ShopDemoRepository.SaveChanges();
        }
    }
}
