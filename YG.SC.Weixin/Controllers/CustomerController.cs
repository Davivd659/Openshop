using System.Web.Mvc;
using AutoMapper;
using YG.SC.DataAccess;
using YG.SC.Model.Project;
using YG.SC.Service.IService;

namespace YG.SC.WeiXin.Controllers
{
    public class CustomerController : BaseController
    {
        //
        // GET: /Customer/
        private readonly ICustomerService _iCustomerService;

        public CustomerController(ICustomerService iCustomerService)
        {
            _iCustomerService = iCustomerService;
        }
        protected override void Dispose(bool disposing)
        {
            this._iCustomerService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index(int CustomerId)
        {
            var query = this._iCustomerService.GetEntityById(CustomerId);
            var customer = Mapper.Map<Customer, CustomerModel>(query);

            return View(customer);
        }
        public ActionResult Focus(int CustomerId)
        {
            return View();
        }
        
        public ActionResult Tuan(int CustomerId)
        {
            return View();
        }
    }
}
