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
    public class CustomerGroupOnService : ICustomerGroupOnService
    {
        private readonly IRepository<CustomerGroupOn> _CustomerGroupOnRepository;

        public CustomerGroupOnService(IRepository<CustomerGroupOn> customerGroupOnRepository)
        {
            _CustomerGroupOnRepository = customerGroupOnRepository;
        }

        public void Dispose()
        {
            this._CustomerGroupOnRepository.Dispose();
        }

        public CustomerGroupOn GetEntityByName(string name)
        {
            return this._CustomerGroupOnRepository.Get(item => item.NAME == name).FirstOrDefault();
        }
    }
}
