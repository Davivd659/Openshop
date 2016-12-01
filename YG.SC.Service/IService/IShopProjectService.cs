using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public  interface  IShopProjectService:IYgService
    {
        List<ShopProject> GetAll();
        List<ShopProject> GetTop(int top);
        Tuple<ShopProject[], PagerEntity> GetEntitsByImageName(int pg, string projectName);
        ShopProject GetById(int id);
        void Update(ShopProject sp);
        void Insert(ShopProject sp);

        List<ShopProjectMain> GetShopProjectMain();
        Tuple<ShopProject[], PagerEntity> GetEntitsByShopProjectMainName(int pg, string projectName);
        Tuple<ShopProject[], PagerEntity> SearchProject(ProjectSearchCriteria criteria);
        ShopProjectMain GetShopProjectMainById(int id);
        void Update(ShopProjectMain sp);
        void Insert(ShopProjectMain sp);
        void UpdateProjectAttributes(int attributeId,string AttrValues,int ProjectId);
    }
}
