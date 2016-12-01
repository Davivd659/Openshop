using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;
namespace YG.SC.Service.IService
{
    public interface ICartService:IYgService
    {
        void Insert(O_Cart model);
        void Update(O_Cart model);
        O_Cart GetById(int id);
        Tuple<O_Cart[], PagerEntity> SearchCart(CartSearchCriteria criteria);
    }
}
