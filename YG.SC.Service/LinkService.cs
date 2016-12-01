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
    public class LinkService : ILinkService
    {
        private readonly IRepository<Link> _LinkRepository;

        public LinkService(IRepository<Link> linkRepository)
        {
            _LinkRepository = linkRepository;
        }

        public void Dispose()
        {
            this._LinkRepository.Dispose();
        }

        public Tuple<Link[], Model.PagerEntity> GetEntitsByName(int pg, string Name, string selRecsts)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<Link, bool>> expressionFilter = (entity) => (string.IsNullOrEmpty(Name) || entity.NAME.Contains(Name)) ;
            if (!string.IsNullOrEmpty(selRecsts))
            {
                int Recsts = Convert.ToInt32(selRecsts);
                expressionFilter = (entity) => (entity.Recsts == Recsts);
            }

            var total = this._LinkRepository.Get(expressionFilter).Count();
            var array = this._LinkRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
       
        }


        public Link GetById(int id)
        {
            return this._LinkRepository.GetById(id);

        }

        public void Update(Link link)
        {
            this._LinkRepository.Update(link);
            this._LinkRepository.SaveChanges();
        }

        public void Insert(Link link)
        {
            this._LinkRepository.Insert(link);
            this._LinkRepository.SaveChanges();
        }


        public List<Link> GetAll()
        {
         return    this._LinkRepository.Get(item => item.Recsts == 1).ToList();
        }
    }
}
