using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Repository;
using YG.SC.Service.IService;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service
{
    public class CartService:ICartService
    {
        private readonly IRepository<O_Cart> _IO_CartRepository;
        public CartService(IRepository<O_Cart> O_CartRepository)
        {
            _IO_CartRepository = O_CartRepository;
        }
        public void Insert(O_Cart model)
        {
            _IO_CartRepository.Insert(model);
            _IO_CartRepository.SaveChanges();
        }

        public void Update(O_Cart model)
        {
            _IO_CartRepository.Insert(model);
            _IO_CartRepository.SaveChanges();
        }
        public O_Cart GetById(int id)
        {
            return _IO_CartRepository.GetById(id);
        }
        public Tuple<O_Cart[], Model.PagerEntity> SearchCart(CartSearchCriteria criteria)
        {
            int top = 6;
            if (criteria.PageSize > 0)
            { top = criteria.PageSize; }

            var idx = (criteria.PageIndex - 1) < 0 ? 0 : (criteria.PageIndex - 1);

            var query = _IO_CartRepository.Table;
            if (criteria.Creater > 0)
            {
                query = query.Where(ｍ => ｍ.Creater == criteria.Creater);
            }
            if (criteria.Begintime != null && criteria.Begintime != null)
            {
                query = query.Where(ｍ => (ｍ.CreateTime > criteria.Begintime && ｍ.CreateTime<criteria.Endtime));
            }
             int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }

        public void Dispose()
        {
            _IO_CartRepository.Dispose();
        }
    }
}
