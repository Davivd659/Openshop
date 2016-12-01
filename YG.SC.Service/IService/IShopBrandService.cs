using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public  interface  IShopBrandService:IYgService
    {
        Tuple<ShopBrand[], PagerEntity> GetEntitsByName(int pg, int ShopBrandId, string Name);
        Tuple<ShopBrand[], PagerEntity> GetEntits(int pg, int AttributeValuesId, int JionIn);
        bool GetEntitsByName(string name);
        bool GetEntitsByAbbreviation(string Abbreviation);
        Tuple<ShopBrand[], PagerEntity> GetEntits(int pg, int AttributeValuesId, string Name);


        ShopBrand GetById(int id);
        void Update(ShopBrand sb);
        void Insert(ShopBrand sb);

        Tuple<ShopBrandBranch[], PagerEntity> GetBranchByBrandId(int pg, int id, string name);
        ShopBrandBranch GetBranchById(int id);
        void BranchUpdate(ShopBrandBranch sb);
        void BranchInsert(ShopBrandBranch sb);


        Tuple<ShopBrandPhoto[], PagerEntity> GetPhotoByBrandId(int pg, int id, string name);
        ShopBrandPhoto GetPhotoById(int id);
        void UpdatePhoto(ShopBrandPhoto sb);
        void InsertPhoto(ShopBrandPhoto sb);

        Dictionary<int,string> GetApplyStatusList();

        Tuple<ApplyBrand[],PagerEntity> GetApplyBrandList(BrandApplyFilter filter);

        Tuple<ApplyBrand[], PagerEntity> GetAuditBrandList(BrandApplyFilter filter);

        void PassApply(int passApplyId);

        void RejectApply(int rejectApplyId, string rejectReason);
    }
}
