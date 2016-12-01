using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YG.SC.Model;
using YG.SC.Service.IService;
using YG.SC.DataAccess;

namespace YG.SC.OpenShop.Controllers
{
    public class MallController : Controller
    {
        private readonly IObjectService _objectService;
        private readonly IHotareaService _HotareaService;
        private readonly ILinkService _ilinkService;
        private readonly ICartGoodsService _icartGoodsService;
        private readonly ICartService _icartService;
        public MallController(IObjectService objectService, IHotareaService HotareaService, ILinkService ilinkService, ICartGoodsService cartGoodsService, ICartService cartService)
        {
            _objectService = objectService;
            _HotareaService = HotareaService;
            _ilinkService = ilinkService;
            _icartGoodsService = cartGoodsService;
            _icartService = cartService;
        }
        protected override void Dispose(bool disposing)
        {
            this._objectService.Dispose();
            this._HotareaService.Dispose();
            this._ilinkService.Dispose();
            this._icartGoodsService.Dispose();
            this._icartService.Dispose();
            base.Dispose(disposing);
        }
        //
        // GET: /Mall/

        public ActionResult Home()
        {
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.ParentId = -1;
            ViewBag.CategoryList = _objectService.SearchCategory(SearchCriteria).Item1.OrderBy(m => m.Sort).ToList();
            ViewBag.Hotare = _HotareaService.GetByParentId(1);
            ViewBag.LinkTypes = this._ilinkService.GetAll();
            return View();
        }
        public ActionResult list(int id = 0, int Parentid = 0)
        {
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.ParentId = Parentid;
            SearchCriteria.Id = id;
            if (id > 0 || Parentid > 0)
            {
                ViewBag.GoodsList = _objectService.SearchCategory(SearchCriteria).Item1.FirstOrDefault();
            }
            CategorySearchCriteria Criteria = new CategorySearchCriteria();
            Criteria.ParentId = -1;
            ViewBag.CategoryList = _objectService.SearchCategory(Criteria).Item1.OrderBy(m => m.Sort).ToList();
            return View();
        }

        public ActionResult Add()
        {
            CategorySearchCriteria SearchCriteria = new CategorySearchCriteria();
            SearchCriteria.ParentId = -1;
            ViewBag.CategoryList = _objectService.SearchCategory(SearchCriteria).Item1.OrderBy(m => m.Sort).ToList();
            ViewBag.Hotare = _HotareaService.GetByParentId(1);
            return View();
        }
        public ActionResult Carts()
        {
            return View();
        }
        [UserAuthorize]
        public string AddCarts(O_CartGoods model)
        {
            O_CartGoods  Cartgoods = new O_CartGoods();
            CartSearchCriteria SearchCriteria = new CartSearchCriteria();
            SearchCriteria.Flag = 2;
            SearchCriteria.Creater=UserContext.Current.Id;
            var cars = this._icartService.SearchCart(SearchCriteria).Item1.FirstOrDefault();
            if (cars != null)
            {
                O_Cart Newcar = new O_Cart()
                {
                    Creater = UserContext.Current.Id,
                    Flag = 2,
                    CreateTime = DateTime.Now
                };
                this._icartService.Insert(Newcar);
                Cartgoods.CartId = Newcar.Id;
            }
            else 
            {
                Cartgoods.CartId = cars.Id;
            }
            Cartgoods.GoodsCount = model.GoodsCount;
            Cartgoods.GoodsId = model.GoodsId; 
            Cartgoods.Price = model.Price;
            this._icartGoodsService.Insert(Cartgoods);
            return "ok";
        }
        public JsonResult GetNodelist(int id)
        {
            var attrsTypes = _HotareaService.GetByParentId(id);
            var typelist = (from m in attrsTypes
                            select new SelectListItem
                            {
                                Text = m.name,
                                Value = m.id.ToString()
                            }).ToList();
            return Json(typelist, JsonRequestBehavior.DenyGet);

        }
        public static List<C_Object> getGoods(C_Object goods)
        {
            List<C_Object> reslut = new List<C_Object>();
            if (goods.Type == 9)
            {
                reslut.Add(goods);
            }
            else
            {
                reslut.Concat(Getgood(goods, reslut));
            }
            return reslut;
        }
        private static List<C_Object> Getgood(C_Object goods, List<C_Object> reslut)
        {
            foreach (var gs in goods.Subset)
            {
                if (gs.Type == 9)
                {
                    reslut.Add(gs);
                }
                else
                {
                    Getgood(gs, reslut);
                }
            }
            return reslut;
        }

    }
}
