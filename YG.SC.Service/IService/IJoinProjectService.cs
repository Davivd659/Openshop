using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
namespace YG.SC.Service.IService
{
    public interface IJoinProjectService:IYgService
    {
        void Insert(JoinProject model);
        JoinProject GetById(int id);
        JoinProject[] GetByPhone(string phone);
        Tuple<JoinProject[], PagerEntity> List(JoinSearchCriteria SearchCriteria);

    }
}
