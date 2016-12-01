using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;

namespace YG.SC.Service.IService
{
    public class ProjectPhotoService : IProjectPhotoService
    {
        private readonly IRepository<ProjectPhoto> _ProjectPhotoRepository;

        public ProjectPhotoService(IRepository<ProjectPhoto> projectPhotoRepository)
        {
            _ProjectPhotoRepository = projectPhotoRepository;
        }

        public void Dispose()
        {
            this._ProjectPhotoRepository.Dispose();
        }

        public Tuple<ProjectPhoto[], Model.PagerEntity> GetEntitsByImageName(int pg, int ShopProjectId, string imageName)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ProjectPhoto, bool>> expressionFilter = (entity) => (ShopProjectId == 0 || entity.ShopProjectId == ShopProjectId) && (string.IsNullOrEmpty(imageName) || entity.PhotoName.Contains(imageName)) && (entity.Recsts != -1);
            var total = this._ProjectPhotoRepository.Get(expressionFilter).Count();
            var array = this._ProjectPhotoRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }



        public ProjectPhoto GetById(int id)
        {
            return
                this._ProjectPhotoRepository.GetById(id);
        }


        public void Update(ProjectPhoto P)
        {
            this._ProjectPhotoRepository.Update(P);
            this._ProjectPhotoRepository.SaveChanges();
        }


        public void Insert(ProjectPhoto P)
        {
            this._ProjectPhotoRepository.Insert(P);
            this._ProjectPhotoRepository.SaveChanges();
        }

        public ProjectPhoto[] GetEntitsByImageName(int ShopProjectId)
        {
            Expression<Func<ProjectPhoto, bool>> expressionFilter = (entity) => (ShopProjectId == 0 || entity.ShopProjectId == ShopProjectId) && (entity.Recsts != -1);
            return this._ProjectPhotoRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).ToArray();
        }
    }
}
