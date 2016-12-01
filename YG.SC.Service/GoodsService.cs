using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Repository;
using YG.SC.Service.IService;
using YG.SC.DataAccess;

namespace YG.SC.Service
{
    public class GoodsService : IGoodsService
    {
        private readonly IRepository<G_Name> _IG_NameRepository;
        public GoodsService(IRepository<G_Name> G_NameRepository)
        {
            _IG_NameRepository = G_NameRepository;
        }
        public void InsertUnit(DataAccess.G_Name model)
        {
            this._IG_NameRepository.Insert(model);
            this._IG_NameRepository.SaveChanges();
        }

        public void UpdateUnit(DataAccess.G_Name model)
        {
            this._IG_NameRepository.Update(model);
            this._IG_NameRepository.SaveChanges();
        }

        public void Dispose()
        {
            this._IG_NameRepository.Dispose();

        }


        public G_Name GetById(int Id)
        {
            return this._IG_NameRepository.GetById(Id);
        }
    }
}
