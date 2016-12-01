using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public interface IObjectService:IYgService
    {
        void Insert(C_Object model);
        void Update(C_Object model);
        C_Object GetById(int Id);
        void InsertImage(C_File model);
        void UpdateImage(C_File model);
        C_File GetImageById(int Id);
        Tuple<C_Object[], PagerEntity> SearchCategory(CategorySearchCriteria SearchCriteria);
    }
}
