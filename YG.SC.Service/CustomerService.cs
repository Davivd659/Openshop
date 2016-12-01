using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Repository;
using YG.SC.Service.IService;

namespace YG.SC.Service
{
	public class CustomerService : ICustomerService
	{
		private readonly IRepository<Customer> _customerRepository;
		private readonly IRepository<SmsLog> _SmsLogREpository;

		public CustomerService(IRepository<Customer> customerRepository, IRepository<SmsLog> smsLongRepository)
		{
			_customerRepository = customerRepository;
			_SmsLogREpository = smsLongRepository;
		}
		public void Dispose()
		{
			this._customerRepository.Dispose();
		}
		public void SendSmsSaveLog(SmsLog smsmodel)
		{
			smsmodel.CreateTime = DateTime.Now;
			_SmsLogREpository.Insert(smsmodel);
			_SmsLogREpository.SaveChanges();
		}
		public DataAccess.Customer GetEntityByName(string name)
		{
			return this._customerRepository.Get(item => item.LoginName == name).FirstOrDefault();
		}
		public Customer GetEntityByNameAndPassword(string name, string Password)
		{
			return this._customerRepository.Get(item => (item.Mobile == name || item.LoginName == name) && item.Password == Password).FirstOrDefault();
		}
		public DataAccess.Customer GetCustomerByMobile(string mobile)
		{
			return this._customerRepository.Table.Where(item => item.Mobile == mobile).FirstOrDefault();
		}

		public bool GetEntityByName(string name, string password)
		{
			return this._customerRepository.Get(
					   item =>
					   (item.Mobile == name || item.LoginName == name) &&
					   item.Password.Trim().ToLower() == password.Trim().ToLower()
					   ).Any();
		}


		public void Insert(Customer cs)
		{
			this._customerRepository.Insert(cs);
			this._customerRepository.SaveChanges();
		}
		public void Update(Customer cs)
		{
			this._customerRepository.Update(cs);
			this._customerRepository.SaveChanges();
		}
		public Customer GetEntityById(int id)
		{
			return this._customerRepository.Get(item => item.Id == id).FirstOrDefault();
		}


		public Tuple<Customer[], PagerEntity> GetEntitsByName(int pg, string projectName)
		{
			const int top = 10;
			var idx = (pg - 1) < 0 ? 0 : (pg - 1);

			Expression<Func<Customer, bool>> expressionFilter =
				(entity) =>
					((string.IsNullOrEmpty(projectName) || entity.Name.Contains(projectName)));
			var total = this._customerRepository.Get(expressionFilter).Count();
			var array =
				_customerRepository.Get(expressionFilter, orderBy: item => item.OrderByDescending(p => p.Id))
					.Skip(top * idx)
					.Take(top)
					.ToArray();
			return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
		}

		public List<Customer> GetAll()
		{
			return _customerRepository.Get().ToList();
		}


		public Customer GetEntitsByGroupAndCompanyId(CommonEnum.GroupOfCustomer userGroup, int companyId)
		{
			return _customerRepository.Get(c => c.GroupId == (int)userGroup && c.Companyid == companyId).FirstOrDefault();
		}
	}
}
