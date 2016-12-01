using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
    public class CartGoodsService:ICartGoodsService
    {
        private readonly IRepository<O_CartGoods> _IO_CartGoodsRepository;
        public CartGoodsService(IRepository<O_CartGoods> CartGoodsRepository)
        {
            _IO_CartGoodsRepository = CartGoodsRepository;
        }
        public void Insert(O_CartGoods model)
        {
            _IO_CartGoodsRepository.Insert(model);
            _IO_CartGoodsRepository.SaveChanges();
        }

        public void Update(O_CartGoods model)
        {
            _IO_CartGoodsRepository.Update(model);
            _IO_CartGoodsRepository.SaveChanges();
        }

        public O_CartGoods GetById(int id)
        {
            return _IO_CartGoodsRepository.GetById(id);
        }

        public Tuple<O_CartGoods[], Model.PagerEntity> SearchCart(CartSearchCriteria criteria)
        {
            int top = 6;
            if (criteria.PageSize > 0)
            { top = criteria.PageSize; }

            var idx = (criteria.PageIndex - 1) < 0 ? 0 : (criteria.PageIndex - 1);

            var query = _IO_CartGoodsRepository.Table;
            if (!string.IsNullOrEmpty(criteria.GoodsName))
            {
                query = query.Where(ｍ => ｍ.FKgoods.Name == criteria.GoodsName);
            }
            int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public void Dispose()
        {
            this._IO_CartGoodsRepository.Dispose();
        }
    }
}
