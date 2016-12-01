using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
    public class ShopPostingsService : IShopPostingsService
    {
        private readonly IRepository<ShopPostings> _PostingsRepository;

        public ShopPostingsService(IRepository<ShopPostings> PostingsRepository)
        {
            _PostingsRepository = PostingsRepository;
        }

        public Tuple<YG.SC.DataAccess.ShopPostings[], PagerEntity> GetSearch(ShopPostingsCriteria criteria)
        {
            int top = criteria.PageSize;
            int pg = criteria.PageIndex;

            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            var query = _PostingsRepository.Table;
            //if (criteria.isSale.ToLower() == "true")
            //{
            //    query = query.Where(m => m.PIntent == "True");
            //}
            //else
            //{
            //    query = query.Where(ｍ => ｍ.PIntent == "False");
            //}
            if (!string.IsNullOrEmpty(criteria.isSale))
            {
                query = query.Where(m => m.PIntent == criteria.isSale);
            }
            if (!string.IsNullOrEmpty(criteria.distinct))
            {
                query = query.Where(m => m.district == criteria.distinct);
            }

            if (!string.IsNullOrEmpty(criteria.PType))
            {
                query = query.Where(m => m.Ptype == criteria.PType);
            }
           
            int total = query.Count();
            var array = query.OrderByDescending(m => m.Addtiem).Skip(top * idx)
                    .Take(top)
                    .ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }


        public void Dispose()
        {
            this._PostingsRepository.Dispose();
        }

        public Tuple<ShopPostings[], Model.PagerEntity> GetEntitsByName(int pg, string Name, string selRecsts)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopPostings, bool>> expressionFilter = (entity) => (string.IsNullOrEmpty(Name) || entity.PName.Contains(Name));
            if (!string.IsNullOrEmpty(selRecsts))
            {
                int Recsts = Convert.ToInt32(selRecsts);
                expressionFilter = (entity) => (1 == 1);
            }

            var total = this._PostingsRepository.Get(expressionFilter).Count();
            var array = this._PostingsRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
       
        }


        public ShopPostings GetById(int id)
        {
            return this._PostingsRepository.GetById(id);

        }

        public void Update(ShopPostings Postings)
        {
            this._PostingsRepository.Update(Postings);
            this._PostingsRepository.SaveChanges();
        }

        public void Insert(ShopPostings Postings)
        {
            this._PostingsRepository.Insert(Postings);
            this._PostingsRepository.SaveChanges();
        }


        public List<ShopPostings> GetAll()
        {
            return this._PostingsRepository.Get(item => 1 == 1).ToList();
        }

        public List<ShopPostings> GetTop(int top)
        {
            throw new NotImplementedException();
        }
    }
}
