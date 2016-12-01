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
    public class ShopAdPositionService : IShopAdPositionService
    {
        private readonly IRepository<ShopAdPosition> _ShopAdPositionRepository;
        private readonly IRepository<ShopProject> _shopProjectRepository; 

        public ShopAdPositionService(IRepository<ShopAdPosition> shopAdPositionRepository,IRepository<ShopProject> shopProjectRepository)
        {
            _ShopAdPositionRepository = shopAdPositionRepository;
            _shopProjectRepository = shopProjectRepository;
        }
        public List<ShopAdPosition> GetAll()
        {
            return _ShopAdPositionRepository.Table.Where(p => p.Status == 1).ToList();
        }

        public List<ShopAdPosition> SearchAdPosition(int positionId,int TypeId,DateTime DateTimeBegin, DateTime DateTimeEnd)
        {
            return _ShopAdPositionRepository.Table.Where(p => p.Status == 1 && p.PositionId == positionId && p.TypesId == TypeId &&
                p.StartDate >= DateTimeBegin && p.StartDate <= DateTimeEnd).OrderBy(m => m.Sort).OrderByDescending(m=>m.StartDate)
                .ToList();
        }

        /// <summary>
        ///  查询当日显示广告信息
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="TypeId"></param>
        /// <param name="DateTimeBegin"></param>
        /// <returns></returns>
        public List<ShopAdPosition> SearchAdPosition(int positionId, int TypeId, DateTime DateTimeBegin)
        {
            var currentDate = (from p in _ShopAdPositionRepository.Table
                         where p.Status == 1 && p.PositionId == positionId && p.TypesId == TypeId &&
                p.StartDate <= DateTimeBegin
                         select p.StartDate).OrderByDescending(m => m).Take(1).FirstOrDefault();

            var query = (from p in _ShopAdPositionRepository.Table
                         where p.Status == 1 && p.PositionId == positionId && p.TypesId == TypeId &&
                         p.StartDate == currentDate
                         select p)
                         .OrderBy(m => m.Sort).ToList();

            return query;
        }

        public Tuple<ShopAdPosition[], PagerEntity> GetEntitsByImageName(int pg, string adName)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopAdPosition, bool>> expressionFilter =
                (entity) =>
                    ((string.IsNullOrEmpty(adName) || entity.AdWords.Contains(adName)) && entity.Status == 1);
            var total = this._ShopAdPositionRepository.Get(expressionFilter).Count();
            var array =
                this._ShopAdPositionRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id))
                    .Skip(top * idx)
                    .Take(top)
                    .ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }

        public ShopAdPosition GetById(int id)
        {
            return _ShopAdPositionRepository.GetById(id);
        }

        public void DeleteById(int id)
        {
            _ShopAdPositionRepository.Delete(id);
            _ShopAdPositionRepository.SaveChanges();
        }
        public void Update(ShopAdPosition sp)
        {
            _ShopAdPositionRepository.Update(sp);
            _ShopAdPositionRepository.SaveChanges();
        }

        public void Insert(ShopAdPosition sp)
        {
            _ShopAdPositionRepository.Insert(sp);
            _ShopAdPositionRepository.SaveChanges();
        }

        /// <summary>
        /// 获取项目数据
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tuple<ShopProject[], PagerEntity> GetShopProjectByName(int pg, string name)
        {
            const int top = 10;
            var idx = (pg - 1) < 0 ? 0 : (pg - 1);

            Expression<Func<ShopProject, bool>> expressionFilter =
                (entity) =>
                    ((string.IsNullOrEmpty(name) || entity.NAME.Contains(name)) && entity.Status == 1);
            var total = this._shopProjectRepository.Get(expressionFilter).Count();
            var array =
                this._shopProjectRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id))
                    .Skip(top * idx)
                    .Take(top)
                    .ToArray();
            return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
        }
    }
}
