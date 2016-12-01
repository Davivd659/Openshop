using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public  interface  IOpenShopService:IYgService
    {

        bool GetEntitsByName(string Name);
        bool GetEntitsByAbbreviation(string Abbreviation);
        Tuple<OpenShop[], PagerEntity> GetEntitsByName(int pg, int Type, string Name);
        OpenShop GetById(int id);
        void Update(OpenShop sb);
        void Insert(OpenShop sb);
        List<OpenShop> GetAllOpenShop();

        List<ShopProject> GetAllShopProject();

        Tuple<OpenShopPhoto[], PagerEntity> GetPhotoByBrandId(int pg, int id, string name);
        OpenShopPhoto GetPhotoById(int id);
        void UpdatePhoto(OpenShopPhoto sb);
        void InsertPhoto(OpenShopPhoto sb);

        Tuple<OpenShopShopProject[], PagerEntity> GetEntitsByName(int pg, string Name, string selRecsts);
        OpenShopShopProject GetOpenShopProjectById(int id);
        void Update(OpenShopShopProject osp);
        void Insert(OpenShopShopProject osp);

        Tuple<OpenShop[], PagerEntity> SearchOpenShop(OpenShopSearchCriteria criteria);
    }
}
