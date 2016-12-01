using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
    public class ShopAttributesService : IShopAttributesService
    {
        private readonly IRepository<ShopAttributes> _shopAttributesRepository;
        private readonly IRepository<ShopAttributeValues> _shopAttributeValuesRepository;
        private readonly IRepository<ShopBrandAttributeValues> _shopBrandAttributeValuesRepository;
        private readonly IRepository<ShopBasPriceRentRange> _shopBasPriceRentRangeRepository;
        private readonly IRepository<ShopBasPriceSaleRange> _shopBasPriceSaleRangeRepository;
        private readonly IRepository<ShopBasOpeningTime> _shopBasOpeningTimeRepository;
        public ShopAttributesService(IRepository<ShopAttributes> shopAttributesRepository, IRepository<ShopAttributeValues> shopAttributeValuesRepository, IRepository<ShopBrandAttributeValues> shopBrandAttributeValuesRepository, IRepository<ShopBasPriceRentRange> shopBasPriceRentRangeRepostory, IRepository<ShopBasPriceSaleRange> shopBasPriceSaleRangeRepostory, IRepository<ShopBasOpeningTime> shopBasOpeningTimeRepository)
        {
            _shopAttributesRepository = shopAttributesRepository;
            _shopAttributeValuesRepository = shopAttributeValuesRepository;
            _shopBrandAttributeValuesRepository = shopBrandAttributeValuesRepository;
            _shopBasPriceRentRangeRepository = shopBasPriceRentRangeRepostory;
            _shopBasPriceSaleRangeRepository = shopBasPriceSaleRangeRepostory;
            _shopBasOpeningTimeRepository = shopBasOpeningTimeRepository;
        }

        public List<ShopAttributes> GetAll()
        {
            return _shopAttributesRepository.Table.ToList();
        }

        public List<ShopAttributeValues> GetListByAttributeId(int attributeId)
        {
            var query = _shopAttributeValuesRepository.Get(p => p.AttributeId == attributeId);
            if (query != null)
            {
                return query.ToList();
            }
            return null;
        }

        public List<ShopBasPriceRentRange> GetBasPriceRentRange()
        {
            return _shopBasPriceRentRangeRepository.Table.OrderBy(m => m.RPCustomerOrder).ToList();
        }
        public List<ShopBasPriceSaleRange> GetBasPriceSaleRange()
        {
            return _shopBasPriceSaleRangeRepository.Table.OrderBy(m => m.PRCustomOrder).ToList();
        }
        public List<ShopBasOpeningTime> GetBasOpeningTimeRange()
        {
            return _shopBasOpeningTimeRepository.Table.OrderBy(m => m.CustomOrder).ToList();
        }

        public void Dispose()
        {
            this._shopAttributesRepository.Dispose();
            this._shopAttributeValuesRepository.Dispose();
            this._shopBrandAttributeValuesRepository.Dispose();
        }


        public void InsertList(List<ShopBrandAttributeValues> list, int brandId = 0)
        {
            if (list != null && list.Count > 0)//确认列表有数据。
            {
                brandId = list[0].BrandId;//以其中一条数据的品牌为当前品牌。
            }
            //删除当前品牌所有数据。
            IEnumerable<ShopBrandAttributeValues> existList = _shopBrandAttributeValuesRepository.Get(a => a.BrandId == brandId);
            if (existList != null)
            {
                foreach (ShopBrandAttributeValues existItem in existList)
                {
                    _shopBrandAttributeValuesRepository.Delete(existItem);
                }
            }
            //添加输入参数中的新数据。
            list.ForEach(item => this._shopBrandAttributeValuesRepository.Insert(item));
            this._shopBrandAttributeValuesRepository.SaveChanges();
        }


        public void UpdateList(int brandid, List<ShopBrandAttributeValues> list)
        {
            var thislist = this._shopBrandAttributeValuesRepository.Get(item => item.BrandId == brandid).ToList();
            List<ShopBrandAttributeValues> oldlist = new List<ShopBrandAttributeValues>();
            oldlist.AddRange(thislist);
            List<ShopBrandAttributeValues> oldlists = new List<ShopBrandAttributeValues>();
            oldlists.AddRange(list);
            list.ForEach(item =>
                {
                    foreach (var t in thislist)
                    {

                        if (t.BrandId == item.BrandId && t.AttributeValuesId == item.AttributeValuesId)
                        {
                            if (t.Recsts != 1)
                            {
                                t.Recsts = 1;
                                this._shopBrandAttributeValuesRepository.Update(t);
                            }
                            oldlist.Remove(t);
                            oldlists.Remove(item);
                        }
                    }
                });
            foreach (var item in oldlist)
            {
                item.Recsts = 0;
                this._shopBrandAttributeValuesRepository.Update(item);
            }
            foreach (var item in oldlists)
            {
                this._shopBrandAttributeValuesRepository.Insert(item);
            }
            this._shopBrandAttributeValuesRepository.SaveChanges();
        }


        public Tuple<ShopAttributes[], Model.PagerEntity> GetEntitsByName(int pg, string txtName, string selRecsts)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopAttributes, bool>> expressionFilter = (entity) => (string.IsNullOrEmpty(txtName) || entity.AttributeName.Contains(txtName));
            //if (!string.IsNullOrEmpty(selRecsts))
            //{
            //    int Recsts = Convert.ToInt32(selRecsts);
            //    expressionFilter = (entity) => (entity.Recsts == Recsts);
            //}

            var total = this._shopAttributesRepository.Get(expressionFilter).Count();
            var array = this._shopAttributesRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }


        public void Insert(ShopAttributes sa)
        {
            this._shopAttributesRepository.Insert(sa);
            this._shopAttributesRepository.SaveChanges();
        }


        public ShopAttributes GetById(int id)
        {
            return this._shopAttributesRepository.GetById(id);

        }


        public void Update(ShopAttributes sa)
        {
            this._shopAttributesRepository.Update(sa);
            this._shopAttributesRepository.SaveChanges();
        }


        public Tuple<ShopAttributeValues[], PagerEntity> GetShopAttributeValuesByName(int id, int pg, string txtName)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopAttributeValues, bool>> expressionFilter = (entity) => (id == 0 || entity.AttributeId == id) && (string.IsNullOrEmpty(txtName) || entity.ValueStr.Contains(txtName));


            var total = this._shopAttributeValuesRepository.Get(expressionFilter).Count();
            var array = this._shopAttributeValuesRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }

        public void Insert(ShopAttributeValues sa)
        {
            this._shopAttributeValuesRepository.Insert(sa);
            this._shopAttributeValuesRepository.Dispose();
        }

        public void Update(ShopAttributeValues sa)
        {
            this._shopAttributeValuesRepository.Update(sa);
            this._shopAttributeValuesRepository.Dispose();
        }


        public void Delete(ShopAttributeValues sav)
        {
            this._shopAttributeValuesRepository.Delete(sav);
            this._shopAttributeValuesRepository.SaveChanges();
        }


        public ShopAttributeValues GetShopAttributeValuesById(int id)
        {
            return this._shopAttributeValuesRepository.GetById(id);

        }
    }
}
