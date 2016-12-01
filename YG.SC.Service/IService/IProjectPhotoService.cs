using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
   public interface IProjectPhotoService:IYgService
    {
       ProjectPhoto[] GetEntitsByImageName(int ShopProjectId);
       Tuple<ProjectPhoto[], PagerEntity> GetEntitsByImageName(int pg,int ShopProjectId, string imageName);
       ProjectPhoto GetById(int id);
       void Update(ProjectPhoto P);
       void Insert(ProjectPhoto P);
    }
}
