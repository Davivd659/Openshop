using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Service.IService;

using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;

namespace YG.SC.Service
{
    public class ApplyBrandService :IApplyBrandService
    {
        private readonly IRepository<ApplyBrand> _applyBrandRepository;

        public ApplyBrandService(IRepository<ApplyBrand> applyBrandRepository)
        {
            _applyBrandRepository = applyBrandRepository;
        }

        public int GetApplyBrandCount()
        {
            var query = _applyBrandRepository.Table.Count();
            return query;
        }

        public Tuple<ApplyBrand[], PagerEntity> Search(ApplyBrandSearchCriteria criteria)
        {
            int top = 20;
            if (criteria.PageSize > 0)
            { top = criteria.PageSize; }
            var idx = (criteria.pg - 1) < 0 ? 0 : (criteria.pg - 1);
            var query =  this._applyBrandRepository .Table.Where(m => m.Status == 1);
            if (criteria.BeginTime.HasValue)
            {
                query = query.Where(m => m.ApplyTime >= criteria.BeginTime);
            }
            if (criteria.EndTime.HasValue)
            {
                query = query.Where(m => m.ApplyTime <= criteria.EndTime);
            }
            if (!string.IsNullOrEmpty(criteria.Phone))
            {
                query = query.Where(m => m.Phone.Contains(criteria.Phone));
            }
            //if (!string.IsNullOrEmpty(criteria.BrandName))
            //{
            //    query = query.Where(m => m.sh.NAME.Contains(criteria.ProjectName));
            //} 
            //if (!string.IsNullOrEmpty(criteria.Type))
            //{
            //    query = query.Where(m => m.Type == criteria.Type);
            //}
            if (!string.IsNullOrEmpty(criteria.UserName))
            {
                query = query.Where(m => m.Contract.Contains(criteria.UserName));
            }

            int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public ApplyBrand GetById(int id)
        {
            var query = _applyBrandRepository
                .GetById(id);
            return query;
        }

        public void Update(ApplyBrand sp)
        {
            _applyBrandRepository.Update(sp);
            _applyBrandRepository.SaveChanges();
        }

        public void Insert(ApplyBrand sp)
        {
            _applyBrandRepository.Insert(sp);
            _applyBrandRepository.SaveChanges();
        }

        public void Dispose()
        {
            this._applyBrandRepository.Dispose();
        }

    }
}
