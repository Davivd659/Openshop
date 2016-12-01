using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Repository;
using YG.SC.Service.IService;
using YG.SC.DataAccess;
using System.Linq.Expressions;

namespace YG.SC.Service
{
    public class HotareaService : IHotareaService
    {
        private readonly IRepository<hotarea> _hotareaRepository;
        public HotareaService(IRepository<hotarea> hotareaRepository)
        {
            _hotareaRepository = hotareaRepository;
        }
        public DataAccess.hotarea[] GetByParentId(int id)
        {

            Expression<Func<hotarea, bool>> expressionFilter = (entity) => (entity.ParentId == id);
            return _hotareaRepository.Get(expressionFilter).ToArray();
        }

        public void Dispose()
        {
            this._hotareaRepository.Dispose();
        }
    }
}
