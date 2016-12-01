using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using YG.SC.Service.IService;
using System.Linq;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Model.Project;
using YG.SC.WeiXin.Models.project;
using YG.SC.Service.IService;
using YG.SC.WeiXin.Models;
using YG.SC.WeiXin.Models.ShopBrand;
using System;


namespace YG.SC.WeiXin.Controllers
{
    public class ShopBrandController : BaseController
    {
        //
        // GET: /ShopBrand/
        private readonly IShopBrandService _iShopBrandService;
        private readonly IShopAttributesService _iShopAttributesService;
        private readonly IApplyBrandService _iapplyBrandService;
        private const int AttrIndustryId = 9;

        public ShopBrandController(IShopBrandService iShopBrandService, IShopAttributesService iShopAttributesServiceService,
            IApplyBrandService applyBrandServie)
        {
            _iShopBrandService = iShopBrandService;
            _iShopAttributesService = iShopAttributesServiceService;
            _iapplyBrandService = applyBrandServie;
        }
        protected override void Dispose(bool disposing)
        {
            this._iShopAttributesService.Dispose();
            this._iShopBrandService.Dispose();
            this._iapplyBrandService.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult List(int pg = 1,string Keys ="",int AttributeValuesId = 0)
        {
            ViewBag.AttributeValuesId = AttributeValuesId;
            ViewBag.Attributes = this._iShopAttributesService.GetListByAttributeId(AttrIndustryId);
            var model = this._iShopBrandService.GetEntits(pg, AttributeValuesId, Keys);

            return View(model);
        }

        public string GetAttrValName(int AttrId)
        {
            var shopAttr = _iShopAttributesService.GetShopAttributeValuesById(AttrId);
            return shopAttr.ValueStr;
        }

        public ActionResult Index(int id = 1)
        {
            ViewBag.Attributes = this._iShopAttributesService.GetListByAttributeId(AttrIndustryId);
            var model = this._iShopBrandService.GetById(id);
            return View(model);
        }

        public ActionResult Img(int id = 1)
        {
            var model = this._iShopBrandService.GetById(id);
            return View(model);
        }

        public ActionResult Supplier(int id = 1)
        {
            var model = this._iShopBrandService.GetById(id);
            return View(model);
        }

        public ActionResult ApplyBrand(int id = 1)
        {
            return View();
        }

        public ActionResult Brand(int shopBrandId = 10)
        {
            var model = this._iShopBrandService.GetById(shopBrandId);
            return View(model);
        }


        [HttpPost]
        public ActionResult ApplyBrand(ApplyBrandModel model)
        {
            var ApplyBrand = new ApplyBrand
            {
                Contract = model.UserName,
                Phone = model.Phone,
                BrandId = model.BrandId,
                Status = 1
            };

            ApplyBrand.ApplyTime = DateTime.Now;
            _iapplyBrandService.Insert(ApplyBrand);

            var jsonResult = new JsonResult();
            jsonResult.Data = "true";
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }
        [HttpGet]
        [ActionName("ApplyBrand")]
        public ActionResult ApplyBrandPage(int id)
        {
            ViewBag.BrandId = id;
            ViewBag.ApplyCount = _iapplyBrandService.GetApplyBrandCount();
            return View();
        }

        private string FilterBuilder(int AttributeValuesId, string queryString)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbclear = new StringBuilder();

            string regkey = "";

            #region 类型
            regkey = "AttributeValuesId";
            sb.Append("<li>");
            // span title 
            string TypeTitle = "行业";
            var TypeItemList = _iShopAttributesService.GetListByAttributeId(AttrIndustryId);
            var TypeItem = TypeItemList.Where(ｍ => ｍ.Id == AttributeValuesId).FirstOrDefault();
            if (TypeItem != null)
            {
                TypeTitle = TypeItem.ValueStr;
            }

            sb.Append("<span class=\"current-condition\">" + TypeTitle + "</span> " +
                          "<i class=\"icon-arrow\"></i>");
            sb.Append("<div id=\"subMenuArea\" class=\"submenu\"><ul class=\"clearfix submenu-type-ul\">");
            string[] TypeArray = new string[] { @"AttributeValuesId=DI(\d+\&?)" };
            string _TyperesetLink = GetHrefLinkReset(queryString, TypeArray, AttributeValuesId, "行业不限");
            sb.AppendFormat(_TyperesetLink);
            //列表
            foreach (var AttrValueItem in TypeItemList)
            {
                if (AttrValueItem.Id == AttributeValuesId)
                {
                    sb.AppendFormat("<li class=\"active\">{0}</li>", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
                else
                {
                    sb.AppendFormat("<li class=\"\">{0}</li>", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append(" </ul></div></li>");

            #endregion

            // 热门排序 

            string clearlink = sbclear.ToString();
            if (!string.IsNullOrEmpty(clearlink))
            {
            }
            return sb.ToString();
        }

        #region 列表-分页-辅助方法
        private string GetPageIndexFilter(string queryString, int pageIndex, int PageTotal)
        {
            string regkey = "PageIndex";
            string currentPageLink = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", pageIndex, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<div data-role=\"widget-pagination\">");
            int _prePg = pageIndex - 1;
            if (_prePg >= 1)
            {
                string _link = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", _prePg, "");
                sb.Append("<a href=\"" + _link + "\">上一页</a>");
            }
            else
            {
                sb.Append("<a class=\"widget-pagination-disable\">上一页</a>");
            }
            sb.Append("<div><a class=\"widget-pagination-current-page\" href=\"" + currentPageLink + "\">" + pageIndex + "/" + PageTotal + "</a>");
            sb.Append("<select class=\"widget-pagination-pages needsclick\" id=\"pageCounts\">");

            for (int i = 1; i <= PageTotal; i++)
            {
                string isSelected = "";
                int _pg = i;
                if (i == pageIndex)
                {
                    isSelected = "selected=\"selected\"";
                }
                string _link = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", i, "");
                sb.Append(string.Format("<option {0} value=\"{1}\">{2}</option>", isSelected, _link, _pg + "/" + PageTotal));
            }
            sb.Append("</select>");
            sb.Append("</div>");

            string nextPg = "";
            if (pageIndex + 1 <= PageTotal)
            {
                nextPg = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", pageIndex + 1, "");
                sb.Append(string.Format("<a href=\"{0}\">下一页</a>", nextPg));
            }
            else
            {
                sb.Append("<a class=\"widget-pagination-disable\">下一页</a>");
            }

            sb.Append("</div>");

            return sb.ToString();
        }

        private int GetID(string idStr)
        {
            if (string.IsNullOrEmpty(idStr))
            {
                return 0;
            }
            string _id = idStr.Replace("DI", "");
            int id = 0;
            bool tryParse = int.TryParse(_id, out id);
            return id;
        }

        private string GetHrefLinkCommon(string queryString, string regStr, string salt, int id, string targetName)
        {
            queryString = Regex.Replace(queryString, @"PageIndex=(\d+\.?\d*|\.\d+)", "");

            StringBuilder sb = new StringBuilder();
            string link = null;

            Regex reg = new Regex(regStr);
            salt = salt + id.ToString();
            if (reg.IsMatch(queryString))
            {
                link = reg.Replace(queryString, salt);
            }
            else
            {
                if (queryString.IndexOf("?") == -1)
                {
                    link = queryString + "?" + salt;
                }
                else
                {
                    if (queryString.IndexOf("?") == queryString.Length - 1)
                    {
                        link = queryString + salt;
                    }
                    else
                    {
                        link = queryString + "&" + salt;
                    }
                }
            }
            //<a class="needsclick" href="http://m.to8to.com/bj/company/">出租</a> 
            sb.AppendFormat("<a class='needsclick' href='{0}'>{1}</a>", link.Replace("?&", "?"), targetName);

            return sb.ToString();
        }

        private string GetHrefLinkDouble(string queryString, string regStr, string regRemoveStr, string salt, int id, string targetName)
        {
            queryString = Regex.Replace(queryString, @"PageIndex=(\d+\.?\d*|\.\d+)", "");

            Regex regRemove = new Regex(regRemoveStr);

            queryString = regRemove.Replace(queryString, "");

            StringBuilder sb = new StringBuilder();
            string link = null;

            Regex reg = new Regex(regStr);
            salt = salt + id.ToString();

            if (reg.IsMatch(queryString))
            {
                link = reg.Replace(queryString, salt);
            }
            else
            {
                if (queryString.IndexOf("?") == -1)
                {
                    link = queryString + "?" + salt;
                }
                else
                {
                    link = queryString + "&" + salt;
                }
            }

            sb.AppendFormat("<a href='{0}' class='needsclick'>{1}</a>", link, targetName);

            return sb.ToString();
        }

        private string GetHrefLinkReset(string queryString, string[] regStrArray, int id, string targetName)
        {
            /*<li class=""><a href="/project/list?status=DI144&amp;&amp;&amp;&amp;quyu=DI21" class="needsclick">海淀</a></li>*/
            queryString = Regex.Replace(queryString, @"PageIndex=(\d+\.?\d*|\.\d+)", "");

            StringBuilder sb = new StringBuilder();
            string link = null;
            string _Active = "";
            if (id == 0)
            {
                _Active = "active";
            }

            Regex reg = null;
            link = queryString;
            foreach (string regStr in regStrArray)
            {
                reg = new Regex(regStr);
                link = reg.Replace(link, "");
            }

            if (link.LastIndexOf('?') == (link.Length - 1))
            {
                //link = link.Replace("?", "");
                link = link.Substring(0, link.LastIndexOf('?'));
            }

            if (link.LastIndexOf('&') == (link.Length - 1))
            {
                //link = link.Replace("&", "");
                link = link.Substring(0, link.LastIndexOf('&'));
            }

            sb.AppendFormat("<li class=\"{0}\"><a href=\"{1}\" class=\"needsclick\">不限</a></li>", _Active, link);

            return sb.ToString();
        }

        private string GetHrefLinkClear(string queryString, string[] regStrArray, int id, string targetName)
        {
            queryString = Regex.Replace(queryString, @"PageIndex=(\d+\.?\d*|\.\d+)", "");

            StringBuilder sb = new StringBuilder();
            string link = null;

            if (id == 0)
            {
                sb.Append("");
            }
            else
            {
                Regex reg = null;
                link = queryString;
                foreach (string regStr in regStrArray)
                {
                    reg = new Regex(regStr);
                    link = reg.Replace(link, "");
                }
                if (link.LastIndexOf('?') == (link.Length - 1))
                {
                    link = link.Substring(0, link.LastIndexOf('?'));
                }
                if (link.LastIndexOf('&') == (link.Length - 1))
                {
                    link = link.Substring(0, link.LastIndexOf('&'));
                }
                sb.AppendFormat("<a class='onck' href='{0}'><divc>{1}</divc><span>×</span></a>", link, targetName);
            }
            return sb.ToString();
        }

        private string GetHrefLink(string queryString, string regStr, string salt, int id, string targetName)
        {
            queryString = Regex.Replace(queryString, @"[&?]PageIndex=(\d+\.?\d*|\.\d+)", "");

            StringBuilder sb = new StringBuilder();
            string link = null;

            Regex reg = new Regex(regStr);
            salt = salt + id.ToString();
            if (reg.IsMatch(queryString))
            {
                link = reg.Replace(queryString, salt);
            }
            else
            {
                if (queryString.IndexOf("?") == -1)
                {
                    link = queryString + "?" + salt;
                }
                else
                {
                    if (queryString.IndexOf("?") == queryString.Length - 1)
                    {
                        link = queryString + salt;
                    }
                    else
                    {
                        link = queryString + "&" + salt;
                    }
                }
            }
            sb.AppendFormat("{0}", link.Replace("?&", "?"));

            return sb.ToString();
        }
        #endregion 


    }
}
