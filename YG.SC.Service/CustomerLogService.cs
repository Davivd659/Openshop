using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Service.IService;
using YG.SC.Repository;
using YG.SC.DataAccess;

namespace YG.SC.Service
{
    class CustomerLogService : ICustomerLogService
    {
        private readonly IRepository<CustomerLog> _ICustomerLogService;
        public CustomerLogService(IRepository<CustomerLog> ICustomerLogService)
        {
            _ICustomerLogService = ICustomerLogService;
        }
        public void Dispose()
        {
            this._ICustomerLogService.Dispose();
        }
        public void Insert(DataAccess.CustomerLog sp)
        {
            this._ICustomerLogService.Insert(sp);
            this._ICustomerLogService.SaveChanges();
        }

        public List<CustomerLog> GetEntityByCustomer(int Customerid)
        {
            return this._ICustomerLogService.Get(p => p.Customer == Customerid).ToList();
        }
    }
}
