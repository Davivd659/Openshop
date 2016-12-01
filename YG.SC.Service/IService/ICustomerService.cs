using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Model;

namespace YG.SC.Service.IService
{
    public interface ICustomerService:IYgService
    {
        Customer GetEntityByName(string name);
        Customer GetEntityByNameAndPassword(string name,string Password);
        Customer GetEntityById(int id);
        bool GetEntityByName(string name,string password);
        void Insert(Customer cs);
        void Update(Customer cs);
        Tuple<Customer[], PagerEntity> GetEntitsByName(int pg, string projectName);
        List<Customer> GetAll();
        DataAccess.Customer GetCustomerByMobile(string mobile);
        void SendSmsSaveLog(SmsLog smsmodel);

		Customer GetEntitsByGroupAndCompanyId(Common.CommonEnum.GroupOfCustomer groupOfCustomer, int p);
	}
}
