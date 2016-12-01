using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
namespace YG.SC.Service.IService
{
   public interface ICustomerLogService:IYgService
    {
       List<CustomerLog> GetEntityByCustomer(int Customerid);
        void Insert(CustomerLog sp);
    }
}
