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
    public class ShopProjectService : IShopProjectService
    {
        private readonly IRepository<ShopProject> _ShopProjectRepository;
        private readonly IRepository<ShopProjectMain> _ShopProjectMainRepository;
        private readonly IRepository<ShopProjectAttributesValues> _shopProjectAttributesValueRepository;

        private readonly IRepository<ShopBasOpeningTime> _basOpeningTimeRepository;
        private readonly IRepository<ShopBasPriceSaleRange> _basPriceSaleRangRepository;
        private readonly IRepository<ShopBasPriceRentRange> _basPriceRentRangRepository;

        public ShopProjectService(IRepository<ShopProject> shopProjectRepository, IRepository<ShopProjectMain> shopProjectMainRepository
           , IRepository<ShopProjectAttributesValues> shopProjectAttributesValueRepository
           , IRepository<ShopBasOpeningTime> basOpeningTimeRepository
           , IRepository<ShopBasPriceRentRange> basPriceRentRangeRepository
           , IRepository<ShopBasPriceSaleRange> basPriceSaleRangeRepository
            )
        {
            _ShopProjectRepository = shopProjectRepository;
            _ShopProjectMainRepository = shopProjectMainRepository;
            _shopProjectAttributesValueRepository = shopProjectAttributesValueRepository;
            _basOpeningTimeRepository = basOpeningTimeRepository;
            _basPriceRentRangRepository = basPriceRentRangeRepository;
            _basPriceSaleRangRepository = basPriceSaleRangeRepository;
        }

        public List<ShopProject> GetAll()
        {
            return this._ShopProjectRepository.Get(p => p.Status == 1).ToList();
        }

        public List<ShopProject> GetTop(int top)
        {
            return this._ShopProjectRepository.Table.Where(m => m.Status == 1).OrderByDescending(p => p.Id).Take(top).ToList();
        }

        public void Dispose()
        {
            this._ShopProjectRepository.Dispose();
        }

        public void UpdateProjectAttributes(int attributeId, string AttrValues, int ProjectId)
        {
            /*,[AttrId],[AttrValueId],[IsEnable],[ShopProjectId]*/

            List<int> listId = new List<int>();
            if (!string.IsNullOrEmpty(AttrValues))
            {
                string[] array = AttrValues.Split(',');

                foreach (var str in array)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        int atid = 0;
                        int.TryParse(str, out atid);
                        if (atid > 0)
                        {
                            listId.Add(Convert.ToInt32(str));
                        }
                    }
                }
            }

            var ItemList = _shopProjectAttributesValueRepository.Table.Where(m => m.ShopProjectId == ProjectId
                && m.AttrId == attributeId).ToList();
            if (ItemList.Count > 0)
            {
                foreach (var item in ItemList)
                {
                    if (listId.Contains(item.AttrValueId.Value))
                    {
                        item.IsEnable = true;
                    }
                    else
                    {
                        item.IsEnable = false;
                    }
                }
            }
            // 新增 
            int[] oldIdlist = (from m in ItemList select m.AttrValueId.Value).ToArray();
            var addList = listId.Except(oldIdlist);
            foreach (var toAddId in addList)
            {
                ShopProjectAttributesValues newMode = new ShopProjectAttributesValues();
                newMode.AttrId = attributeId;
                newMode.AttrValueId = toAddId;
                newMode.IsEnable = true;
                newMode.ShopProjectId = ProjectId;
                _shopProjectAttributesValueRepository.Insert(newMode);
            }
            _shopProjectAttributesValueRepository.SaveChanges();
        }

        public ShopProject GetById(int id)
        {
            return _ShopProjectRepository.GetById(id);
        }

        public void Update(ShopProject sp)
        {
            _ShopProjectRepository.Update(sp);
            _ShopProjectRepository.SaveChanges();
        }

        public void Insert(ShopProject sp)
        {
            _ShopProjectRepository.Insert(sp);
            _ShopProjectRepository.SaveChanges();
        }


        public Tuple<ShopProject[], PagerEntity> GetEntitsByImageName(int pg, string projectName)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopProject, bool>> expressionFilter =
                (entity) =>
                    ((string.IsNullOrEmpty(projectName) || entity.NAME.Contains(projectName)) && entity.Status == 1);
            var total = _ShopProjectRepository.Get(expressionFilter).Count();
            var array =
                _ShopProjectRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id))
                    .Skip(top * idx)
                    .Take(top)
                    .ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public Tuple<ShopProject[], PagerEntity> SearchProject(ProjectSearchCriteria criteria)
        {
            int top = 6;
            if (criteria.PageSize > 0)
            { top = criteria.PageSize; }

            var idx = (criteria.PageIndex - 1) < 0 ? 0 : (criteria.PageIndex - 1);

            var query = _ShopProjectRepository.Table.Where(m => m.Status == 1);
            if (!string.IsNullOrEmpty(criteria.Keys))
            {
                query = query.Where(m => m.NAME.Contains(criteria.Keys));
            }
            // 行业
            if (criteria.HangYeId.HasValue && criteria.HangYeId.Value > 0)
            {
                query = query.Where(m => m.ShopProjectAttributesValues.Any(t => t.ShopProjectId == m.Id
                    && t.AttrValueId == criteria.HangYeId && t.IsEnable == true));
            }
            if (criteria.AreaId.HasValue && criteria.AreaId.Value > 0)
            {
                query = query.Where(m => m.ShopProjectMain.Any(t => t.RegionId == criteria.AreaId));
            }
            if (criteria.SubWayId.HasValue && criteria.SubWayId.Value > 0)
            {
                // query = query.Where(m => m.ShopProjectMain.Any(t => t.MetroId == criteria.SubWayId.Value));
                query = query.Where(m => m.ShopProjectAttributesValues.Any(t => t.ShopProjectId == m.Id
                    && t.AttrValueId == criteria.SubWayId && t.IsEnable == true));
            }
            // 业态 
            if (criteria.WuYeleixingId.HasValue && criteria.WuYeleixingId.Value > 0)
            {
                query = query.Where(m => m.ShopProjectAttributesValues.Any(t => t.ShopProjectId == m.Id
                    && t.AttrValueId == criteria.WuYeleixingId && t.IsEnable == true));

            }// 出租、出售
            if (criteria.StatusID.HasValue && criteria.StatusID.Value > 0)
            {
                //query = query.Where(m => m.ShopProjectAttributesValues.Any(t => t.ShopProjectId == m.Id
                //&&t.AttrValueId == criteria.StatusID && t.IsEnable == true));// 价格
                string StateId = criteria.StatusID.ToString();

                query = query.Where(m => m.ShopProjectMain.Any(p => p.StateId == StateId));
                if (criteria.StatusID == 144)
                {
                    // 出售价格
                    var basPrice = _basPriceSaleRangRepository.Table.Where(m => m.PRID == criteria.PriceSaleId).FirstOrDefault();
                    if (basPrice != null)
                    {
                        decimal priceEnd = Convert.ToDecimal(basPrice.PREnd);
                        decimal priceStart = Convert.ToDecimal(basPrice.PRFrom);
                        query = query.Where(m => m.ShopProjectMain.Any(t => t.SalePrice.Value >= priceStart && t.SalePrice <= priceEnd));
                    }
                }
                else
                {
                    // 出租价格
                    var basPrice = _basPriceRentRangRepository.Table.Where(m => m.Id == criteria.PriceRentId).FirstOrDefault();
                    if (basPrice != null)
                    {
                        decimal priceEnd = Convert.ToDecimal(basPrice.RPEnd);
                        decimal priceStart = Convert.ToDecimal(basPrice.RPFrom);
                        query = query.Where(m => m.ShopProjectMain.Any(t => t.RentalPrice.Value >= priceStart && t.RentalPrice <= priceEnd));
                    }
                }
            }
            // 开盘时间
            if (criteria.OpenTimeId > 0)
            {
                DateTime dateBegin = DateTime.Now.AddDays(-criteria.OpenTimeId);
                query = query.Where(m => m.ShopProjectMain.Any(t => t.OpenDate > dateBegin));
            }
            if (criteria.Projecthot > 0)
            {
                query = query.Where(m => m.Projecthot==criteria.Projecthot);
            }
            int total = query.Count();
            var array = query.OrderByDescending(m => m.Id).Skip(idx * top).Take(top).ToArray();

            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });

        }


        public List<ShopProjectMain> GetShopProjectMain()
        {
            throw new NotImplementedException();
        }

        public Tuple<ShopProject[], PagerEntity> GetEntitsByShopProjectMainName(int pg, string Name)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopProject, bool>> expressionFilter = (entity) => (string.IsNullOrEmpty(Name) || entity.NAME.Contains(Name)) && (entity.Status > 0);
            var total = this._ShopProjectRepository.Get(expressionFilter).Count();
            var array = this._ShopProjectRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id)).Skip(top * idx).Take(top).ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public ShopProjectMain GetShopProjectMainById(int id)
        {
            return _ShopProjectMainRepository.GetById(id);
        }

        public void Update(ShopProjectMain sp)
        {
            _ShopProjectMainRepository.Update(sp);
            _ShopProjectMainRepository.SaveChanges();
        }

        public void Insert(ShopProjectMain sp)
        {
            _ShopProjectMainRepository.Insert(sp);
            _ShopProjectMainRepository.SaveChanges();
        }
    }
}
