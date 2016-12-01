using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public interface IApplyBrandService : IYgService
    {
        int GetApplyBrandCount();

        Tuple<ApplyBrand[], PagerEntity> Search(ApplyBrandSearchCriteria searchCriteria);

        ApplyBrand GetById(int id);
        void Update(ApplyBrand sp);
        void Insert(ApplyBrand sp);
    }
}
