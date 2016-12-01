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
    public class ObjectService : IObjectService
    {
        private readonly IRepository<C_Object> _c_ObjectRepository;
        private readonly IRepository<C_File> _c_FileRepository;
        public ObjectService(IRepository<C_Object> c_ObjectRepository, IRepository<C_File> c_FileRepository)
        {
            _c_ObjectRepository = c_ObjectRepository;
            _c_FileRepository = c_FileRepository;
        }
        public void Insert(DataAccess.C_Object model)
        {
            _c_ObjectRepository.Insert(model);
            _c_ObjectRepository.SaveChanges();
        }

        public void Update(DataAccess.C_Object model)
        {
            _c_ObjectRepository.Update(model);
            _c_ObjectRepository.SaveChanges();
        }
        C_Object IObjectService.GetById(int Id)
        {
            return _c_ObjectRepository.GetById(Id);
        }

        public void Dispose()
        {
            this._c_ObjectRepository.Dispose();
            this._c_FileRepository.Dispose();
        }


        public Tuple<C_Object[], Model.PagerEntity> SearchCategory(Model.CategorySearchCriteria SearchCriteria)
        {
            int top = 20;
            if (SearchCriteria.PageSize > 0)
            { top = SearchCriteria.PageSize; }
            var idx = (SearchCriteria.PageIndex - 1) < 0 ? 0 : (SearchCriteria.PageIndex - 1);
            var query = this._c_ObjectRepository.Table;
            if (SearchCriteria.Type > 0)
            {
                query = query.Where(m => m.Type == SearchCriteria.Type);
            }
            if (SearchCriteria.Id > 0)
            {
                query = query.Where(m => m.Id == SearchCriteria.Id);
            }
            if (SearchCriteria.ParentId != 0)
            {
                query = query.Where(m => m.ParentId == SearchCriteria.ParentId);
            }
            if (!string.IsNullOrEmpty(SearchCriteria.Name))
            {
                query = query.Where(m => (m.Name == SearchCriteria.Name || m.Parent.Name == SearchCriteria.Name));
            }
            if (SearchCriteria.Status > 0)
            {
                query = query.Where(m => m.Status == SearchCriteria.Status);
            }
            int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }


        public void InsertImage(C_File model)
        {
            this._c_FileRepository.Insert(model);
            this._c_FileRepository.SaveChanges();
        }

        public void UpdateImage(C_File model)
        {
            this._c_FileRepository.Update(model);
            this._c_FileRepository.SaveChanges();
        }
        C_File IObjectService.GetImageById(int Id)
        {
            return this._c_FileRepository.GetById(Id);
        }
    }
}
