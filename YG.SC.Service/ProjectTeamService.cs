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
    public class ProjectTeamService : IProjectTeamService
    {
        private readonly IRepository<ProjectService> _ProjectServiceRepository;
        private readonly IRepository<Grouppurchase> _GrouppurchaseRepository;
       private readonly IRepository<ShopProjectMain> _ShopProjectMainRepository;

       public ProjectTeamService(IRepository<ProjectService> shopProjectRepository, IRepository<ShopProjectMain> shopProjectMainRepository, IRepository<Grouppurchase> grouppurchaseRepository)
       {
           _ProjectServiceRepository = shopProjectRepository;
           _ShopProjectMainRepository = shopProjectMainRepository;
           _GrouppurchaseRepository=grouppurchaseRepository;
       }

        public List<ProjectService> GetAll()
        {
            return _ProjectServiceRepository.Table.Where(p => p.Status == 1).ToList();
        }

        public Tuple<ProjectService[], Model.PagerEntity> GetEntitsByImageName(int pg, string projectName)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ProjectService, bool>> expressionFilter =
                (entity) =>
                    ((string.IsNullOrEmpty(projectName) || entity.Name.Contains(projectName)) && entity.Status == 1);
            var total = this._ProjectServiceRepository.Get(expressionFilter).Count();
            var array =
                this._ProjectServiceRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id))
                    .Skip(top*idx)
                    .Take(top)
                    .ToArray();
            return Tuple.Create(array, new PagerEntity {Total = total, PageIndex = idx + 1, Top = top});
        }

        public ProjectService GetById(int id)
        {
            return _ProjectServiceRepository.GetById(id);
        }

        public void Update(ProjectService sp)
        {
            _ProjectServiceRepository.Update(sp);
            _ProjectServiceRepository.SaveChanges();
        }

        public void Insert(ProjectService sp)
        {
            _ProjectServiceRepository.Insert(sp);
            _ProjectServiceRepository.SaveChanges();
        }


        public void TeamInsert(Grouppurchase moudel)
        {
            _GrouppurchaseRepository.Insert(moudel);
            _GrouppurchaseRepository.SaveChanges();
        }

        public void TeamUpdate(Grouppurchase moudel)
        {
            _GrouppurchaseRepository.Update(moudel);
            _GrouppurchaseRepository.SaveChanges();
        }

        Grouppurchase IProjectTeamService.TeamGetById(int id)
        {
            return _GrouppurchaseRepository.GetById(id);
        }
        public Tuple<Grouppurchase[], PagerEntity> SearchProject(GrouppurchaseSearchCriteria criteria)
        {
            int top = 6;
            if (criteria.PageSize > 0)
            { top = criteria.PageSize; }

            var idx = (criteria.PageIndex - 1) < 0 ? 0 : (criteria.PageIndex - 1);

            var query = _GrouppurchaseRepository.Table;
            if (!string.IsNullOrEmpty(criteria.ProjectName))
            {
                query = query.Where(m => m.ShopProject.NAME.Contains(criteria.ProjectName));
            }
            if (criteria.ProjectId>0)
            {
                query = query.Where(m => m.ShopProjectId==criteria.ProjectId);
            }
            int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

    }
}
