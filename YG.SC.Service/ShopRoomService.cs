using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
    public class ShopRoomService : IShopRoomService
    {
        private readonly IRepository<ShopRoom> shopRoomRepository;
        private readonly IRepository<ShopBasPriceSaleRange> _basPriceSaleRangRepository;
        private readonly IRepository<ShopBasPriceRentRange> _basPriceRentRangRepository;


        private readonly IRepository<ShopBasOpeningTime> _basOpeningTimeRepository;
        public ShopRoomService(IRepository<ShopRoom> _shopRoomRepository
           , IRepository<ShopBasPriceRentRange> basPriceRentRangeRepository
           , IRepository<ShopBasPriceSaleRange> basPriceSaleRangeRepository)
        {
            this.shopRoomRepository = _shopRoomRepository;
            _basPriceRentRangRepository = basPriceRentRangeRepository;
            _basPriceSaleRangRepository = basPriceSaleRangeRepository;
        }
        public Tuple<YG.SC.DataAccess.ShopRoom[], PagerEntity> GetSearch(ShopRoomCriteria criteria)
        {
            int top = criteria.PageSize;
            int pg = criteria.PageIndex;

            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            var query = shopRoomRepository.Table.Where(m => m.city == "北京");
            if (!string.IsNullOrEmpty(criteria.isSale))
            {
                query = query.Where(m => m.sale == criteria.isSale);
            }
            if (!string.IsNullOrEmpty(criteria.Keys))
            {
                query = query.Where(m => m.RName.Contains(criteria.Keys));
            }
            if (!string.IsNullOrEmpty(criteria.distinct))
            {
                query = query.Where(m => m.district == criteria.distinct);
            }
            DateTime date = new DateTime();
            if (DateTime.TryParse(criteria.AddTime, out date))
            {
                query = query.Where(m => m.AddTime == date);
            }
            //if (!string.IsNullOrEmpty(criteria.distinct))
            //{
            //    query = query.Where(m => m.district == criteria.distinct);
            //}
            //if (!string.IsNullOrEmpty(criteria.type))
            //{
            //    query = query.Where(m => m.type == criteria.type);
            //}
            //if (criteria.isSale.ToLower() == "true")
            //{
            //    query = query.Where(m => m.sale == "True");
            //}
            //else
            //{
            //    query = query.Where(ｍ => ｍ.sale == "False");
            //}
            // 开盘时间
            if (criteria.OpenTimeId > 0)
            {
                DateTime dateBegin = DateTime.Now.AddDays(-criteria.OpenTimeId);
                query = query.Where(m => m.AddTime > dateBegin);
            }
            if (!string.IsNullOrEmpty(criteria.isSale) && criteria.isSale.ToLower() == "true" && criteria.PriceSaleId > 0)
            {
                // 出售价格
                var basPrice = _basPriceSaleRangRepository.Table.Where(m => m.PRID == criteria.PriceSaleId).FirstOrDefault();
                if (basPrice != null)
                {
                    decimal priceEnd = Convert.ToDecimal(basPrice.PREnd);
                    decimal priceStart = Convert.ToDecimal(basPrice.PRFrom);
                    query = query.Where(t => t.price >= priceStart && t.price <= priceEnd);
                }
            }
            if (!string.IsNullOrEmpty(criteria.isSale) && criteria.isSale.ToLower() == "false" && criteria.PriceRentId > 0)
            {
                // 出租价格
                var basPrice = _basPriceRentRangRepository.Table.Where(m => m.Id == criteria.PriceRentId).FirstOrDefault();
                if (basPrice != null)
                {
                    decimal priceEnd = Convert.ToDecimal(basPrice.RPEnd);
                    decimal priceStart = Convert.ToDecimal(basPrice.RPFrom);
                    query = query.Where(t => t.price >= priceStart && t.price <= priceEnd);
                }

            }


            int total = query.Count();
            var array = query.OrderByDescending(m => m.AddTime).Skip(top * idx)
                    .Take(top)
                    .ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public ShopRoom GetByShopId(string shopid)
        {
            var query = shopRoomRepository.Table.Where(m => m.ShopId == shopid);

            return query.FirstOrDefault();
        }

        public void Dispose()
        {
            this.shopRoomRepository.Dispose();
        }

        public List<ShopRoom> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ShopRoom> GetTop(int top)
        {
            throw new NotImplementedException();
        }

        public Tuple<ShopRoom[], PagerEntity> GetEntitsByImageName(int pg, string RName)
        {
            throw new NotImplementedException();
        }

        public ShopRoom GetById(int id)
        {
            return shopRoomRepository.Table.Where(m => m.Id == id).FirstOrDefault();
        }

        public void Update(ShopRoom sp)
        {
            throw new NotImplementedException();
        }

        public void Insert(ShopRoom sp)
        {
            throw new NotImplementedException();
        }

        public Tuple<ShopRoom[], PagerEntity> GetEntitsByShopRoomName(int pg, string RName)
        {
            throw new NotImplementedException();
        }


    }
}
