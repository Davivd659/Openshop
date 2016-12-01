using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
   public interface ILinkService:IYgService
    {
       Tuple<Link[], PagerEntity> GetEntitsByName(int pg, string Name, string selRecsts);
       Link GetById(int id);
       void Update(Link link);
       void Insert(Link link);
       List<Link> GetAll();
    }
}
