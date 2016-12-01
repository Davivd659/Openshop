using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    /// <summary>
    /// 活动申请
    /// </summary>
    public interface IApplyActiviteService : IYgService
    {
        List<ApplyActivity> GetAll();
        List<ApplyActivity> GetTop(int projectId,int top);
        Tuple<ApplyActivity[], PagerEntity> GetEntitsByImageName(int pg, string projectName);
        Tuple<ApplyActivity[], PagerEntity> Search(ApplyActivitySearchCriteria searchCriteria);
        
        ApplyActivity GetById(int id);
        ApplyActivity[] GetByPhone(string phone);
        int GetApplyCount(int projectid);
        void Update(ApplyActivity sp);
        void Insert(ApplyActivity sp);
    }
}
