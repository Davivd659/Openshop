using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public interface IProjectTeamService
    {
        List<ProjectService> GetAll();
        Tuple<ProjectService[], PagerEntity> GetEntitsByImageName(int pg, string projectName);
        ProjectService GetById(int id);
        void Update(ProjectService sp);
        void Insert(ProjectService sp);
        #region 处理团购的接口
        void TeamInsert(Grouppurchase moudel);
        void TeamUpdate(Grouppurchase moudel);
        Grouppurchase TeamGetById(int id);
        Tuple<Grouppurchase[], PagerEntity> SearchProject(GrouppurchaseSearchCriteria criteria);
        #endregion
    }
}
