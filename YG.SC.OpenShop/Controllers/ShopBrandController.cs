using System.Web.Mvc;
using YG.SC.Service.IService;

namespace YG.SC.OpenShop.Controllers
{
    public class ShopBrandController : Controller
    {
        //
        // GET: /ShopBrand/
        private readonly IShopBrandService _iShopBrandService;
        private readonly IShopAttributesService _iShopAttributesServiceService;

        public ShopBrandController(IShopBrandService iShopBrandService, IShopAttributesService iShopAttributesServiceService)
        {
            _iShopBrandService = iShopBrandService;
            _iShopAttributesServiceService = iShopAttributesServiceService;
        }
        protected override void Dispose(bool disposing)
        {
            this._iShopAttributesServiceService.Dispose();
            this._iShopBrandService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index(int pg = 1, int AttributeValuesId = 0,int JoinIn=-1)
        {
            ViewBag.AttributeValuesId = AttributeValuesId;
            ViewBag.JoinIn = JoinIn;
            ViewBag.Attributes = this._iShopAttributesServiceService.GetListByAttributeId(9);
            var model = this._iShopBrandService.GetEntits(pg, AttributeValuesId,JoinIn);
            return View(model);
        }

        public ActionResult Brand(int shopBrandId = 10)
        {
            var model = this._iShopBrandService.GetById(shopBrandId);
            return View(model);
        }

    }
}
