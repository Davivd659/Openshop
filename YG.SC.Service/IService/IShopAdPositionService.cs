using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public interface IShopAdPositionService
    {
        List<ShopAdPosition> GetAll();
        Tuple<ShopAdPosition[], PagerEntity> GetEntitsByImageName(int pg, string adName);
        ShopAdPosition GetById(int id);
        void Update(ShopAdPosition sp);
        void Insert(ShopAdPosition sp);
        void DeleteById(int id);

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="pg"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Tuple<ShopProject[], PagerEntity> GetShopProjectByName(int pg, string name);
        /// <summary>
        /// 查询当日显示广告信息
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="TypeId"></param>
        /// <param name="DateTimeBegin"></param>
        /// <returns></returns>
        List<ShopAdPosition> SearchAdPosition(int positionId, int TypeId, DateTime DateTimeBegin);
        List<ShopAdPosition> SearchAdPosition(int positionId,int TypeId,DateTime DateTimeBegin, DateTime DateTimeEnd);
    }
}
