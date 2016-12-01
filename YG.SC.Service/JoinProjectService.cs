using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Service.IService;
using YG.SC.DataAccess;
using YG.SC.Repository;
using YG.SC.Model;
using System.Linq.Expressions;

namespace YG.SC.Service
{
    public class JoinProjectService : IJoinProjectService
    {
        private IRepository<JoinProject> _joinProjectRepository;
        public JoinProjectService(IRepository<JoinProject> joinProjectRepository)
        {
            _joinProjectRepository = joinProjectRepository;
        }
        public void Insert(DataAccess.JoinProject model)
        {
            this._joinProjectRepository.Insert(model);
            this._joinProjectRepository.SaveChanges();
        }

        public DataAccess.JoinProject GetById(int id)
        {
            return this._joinProjectRepository.GetById(id);
        }
        JoinProject[] IJoinProjectService.GetByPhone(string phone)
        {
            Expression<Func<JoinProject, bool>> expressionFilter = (entity) => (entity.Phone == phone);
            return this._joinProjectRepository.Get(expressionFilter).ToArray();
        }
        public Tuple<DataAccess.JoinProject[], Model.PagerEntity> List(Model.JoinSearchCriteria SearchCriteria)
        {
            int top = 6;
            if (SearchCriteria.PageSize > 0)
            { top = SearchCriteria.PageSize; }

            var idx = (SearchCriteria.PageIndex - 1) < 0 ? 0 : (SearchCriteria.PageIndex - 1);

            var query = _joinProjectRepository.Table;
            if (SearchCriteria.ShopProjectId > 0)
            {
                query = query.Where(m => m.ShopProjectId == SearchCriteria.ShopProjectId);
            }
            if (!string.IsNullOrEmpty(SearchCriteria.Keys))
            {
                query = query.Where(m => (m.Name.Contains(SearchCriteria.Keys) || m.Phone.Contains(SearchCriteria.Keys)));
            }
            int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
