using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Repository;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
    public class ShopAdPositionDetailsService : IShopAdPositionDetailsService
    {
        private readonly IRepository<ShopAdPositionDetails> _shopAdPositionDetailsRepository;

        public ShopAdPositionDetailsService(IRepository<ShopAdPositionDetails> shopAdPositionDetailsRepository)
        {
            _shopAdPositionDetailsRepository = shopAdPositionDetailsRepository;
        }
        public List<ShopAdPositionDetails> GetAll()
        {
            return _shopAdPositionDetailsRepository.Table.Where(p => p.Status == "1").ToList();
        }

        public Tuple<ShopAdPositionDetails[], Model.PagerEntity> GetEntitsByImageName(int pg, string adName)
        {
            throw new NotImplementedException();
        }

        public ShopAdPositionDetails GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ShopAdPositionDetails sp)
        {
            throw new NotImplementedException();
        }

        public void Insert(ShopAdPositionDetails sp)
        {
            throw new NotImplementedException();
        }
    }
}
