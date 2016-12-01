using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;

namespace YG.SC.Service.IService
{
    public interface IShopAttributesService : IYgService
    {
        List<ShopAttributes> GetAll();
        List<ShopAttributeValues> GetListByAttributeId(int attributeId);
        ShopAttributes GetById(int id);
        ShopAttributeValues GetShopAttributeValuesById(int id);
        Tuple<ShopAttributes[], Model.PagerEntity> GetEntitsByName(int pg, string txtName, string selRecsts);
        Tuple<ShopAttributeValues[], Model.PagerEntity> GetShopAttributeValuesByName(int id, int pg, string txtName);
        void Insert(ShopAttributeValues sa);
        void Update(ShopAttributeValues sa);
        void InsertList(List<ShopBrandAttributeValues> list, int brandId = 0);
        void Insert(ShopAttributes sa);
        void Update(ShopAttributes sa);
        void UpdateList(int brandid, List<ShopBrandAttributeValues> list);
        void Delete(ShopAttributeValues sav);

        List<ShopBasPriceRentRange> GetBasPriceRentRange();
        List<ShopBasPriceSaleRange> GetBasPriceSaleRange();
        List<ShopBasOpeningTime> GetBasOpeningTimeRange();
    }
}
