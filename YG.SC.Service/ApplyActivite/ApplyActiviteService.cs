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
    public class ApplyActiviteService : IApplyActiviteService
    {
        private readonly IRepository<ApplyActivity> _ApplyActivityRepository;
        public ApplyActiviteService(IRepository<ApplyActivity> applyActivityRepository)
        {
            _ApplyActivityRepository = applyActivityRepository;
        }
        public List<ApplyActivity> GetAll()
        {
            return _ApplyActivityRepository.Table.Where(p => p.Status == 1).ToList();
        }
        public List<ApplyActivity> GetTop(int GrouppurchaseId, int top)
        {
            var query = _ApplyActivityRepository.Table.Where(p => p.Status == 1);
            if (GrouppurchaseId > 0)
            {
                query = query.Where(m => m.GrouppurchaseId == GrouppurchaseId);
            }

            var list = query.OrderByDescending(m => m.UpdateDate).Take(top).ToList();
            return list;
        }

        public int GetApplyCount(int GrouppurchaseId)
        {
            var query = _ApplyActivityRepository.Table.Where(p => p.GrouppurchaseId == GrouppurchaseId);

            return query.Count();
        }

        public Tuple<ApplyActivity[], PagerEntity> GetEntitsByImageName(int pg, string projectName)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ApplyActivity, bool>> expressionFilter =
                (entity) =>
                    ((string.IsNullOrEmpty(projectName) || entity.Name.Contains(projectName)) && entity.Status == 1);
            var total = _ApplyActivityRepository.Get(expressionFilter).Count();
            var array =
                this._ApplyActivityRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id))
                    .Skip(top * idx)
                    .Take(top)
                    .ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public Tuple<ApplyActivity[], PagerEntity> Search(ApplyActivitySearchCriteria criteria)
        {
            int top = 6;
            if (criteria.PageSize > 0)
            { top = criteria.PageSize; }
            var idx = (criteria.pg - 1) < 0 ? 0 : (criteria.pg - 1);
            var query = _ApplyActivityRepository.Table;
            if (criteria.BeginTime.HasValue)
            {
                query = query.Where(m => m.UpdateDate >= criteria.BeginTime);
            }
            if (criteria.EndTime.HasValue)
            {
                query = query.Where(m => m.UpdateDate <= criteria.EndTime);
            }
            if (!string.IsNullOrEmpty(criteria.ProjectName))
            {
                query = query.Where(m => m.Grouppurchase.ShopProject.NAME.Contains(criteria.ProjectName));
            }

            if (!string.IsNullOrEmpty(criteria.UserName))
            {
                query = query.Where(m => m.Name.Contains(criteria.UserName));
            }

            if (!string.IsNullOrEmpty(criteria.Phone))
            {
                query = query.Where(m => m.Phone.Contains(criteria.Phone));
            }
            if (criteria.GrouppurchaseId>0)
            {
                query = query.Where(m => m.GrouppurchaseId==criteria.GrouppurchaseId);
            }
            if (criteria.Status.HasValue)
            {
                query = query.Where(m => m.Grouppurchase.Status == criteria.Status.Value);
            }

            int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public ApplyActivity GetById(int id)
        {
            return _ApplyActivityRepository.GetById(id);
        }
        public ApplyActivity[] GetByPhone(string phone)
        {
            Expression<Func<ApplyActivity, bool>> expressionFilter = (entity) => (entity.Phone == phone);
            return this._ApplyActivityRepository.Get(expressionFilter).ToArray();
        }
        public void Update(ApplyActivity sp)
        {
            _ApplyActivityRepository.Update(sp);
            _ApplyActivityRepository.SaveChanges();
        }

        public void Insert(ApplyActivity sp)
        {
            _ApplyActivityRepository.Insert(sp);
            _ApplyActivityRepository.SaveChanges();
        }

        public void Dispose()
        {
            this._ApplyActivityRepository.Dispose();
        }

    }
}
