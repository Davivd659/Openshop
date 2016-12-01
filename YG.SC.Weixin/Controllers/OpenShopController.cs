using System.Web.Mvc;
using YG.SC.Service.IService;
using System;
using YG.SC.Model;
using YG.SC.Common;
using System.Web;
using System.Text;

using System.Linq;
using System.Text.RegularExpressions;

namespace YG.SC.WeiXin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenShopController : Controller
    {
        private const int AttrTypeId = 20;
        private const int AttrQuyuId = 3;
        //
        // GET: /OpenShop/
        private readonly IOpenShopService _iOpenShopService;
        private readonly IShopAdPositionService _iShopAdPositionService;
        private readonly IShopAttributesService _iShopAttributesService;
        private const int AttrIndustryId = 20;
        public OpenShopController(IOpenShopService iOpenShopService, IShopAdPositionService shopPositionService, IShopAttributesService iShopAttributesService)
        {
            _iOpenShopService = iOpenShopService;
            _iShopAdPositionService = shopPositionService;
            _iShopAttributesService = iShopAttributesService;
        }

        protected override void Dispose(bool disposing)
        {
            this._iOpenShopService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult List(int pg = 1, string name = "", int AttributeValuesId = 20)
        {
            string str_QuYu = Request.QueryString["quyu"] ?? null;// 城区
            string str_TypeId = Request.QueryString["typeid"] ?? null; // 类型：装修帮、注册帮

            int IDQuYu = GetID(str_QuYu);
            int IDTypeId = GetID(str_TypeId);
            int HotId = 0;
            string queryString = Request.Url.PathAndQuery;
            ViewBag.Filter = FilterBuilder(IDTypeId, IDQuYu, HotId, queryString);
            // search 
            int PageIndex = 1; // 页码从1 开始 
            int.TryParse(Request.QueryString["PageIndex"], out PageIndex);
            if (PageIndex == 0) { PageIndex = 1; }

            var searchCriteria = new OpenShopSearchCriteria();
            searchCriteria.PageIndex = PageIndex;
            searchCriteria.PageSize = 6;

            searchCriteria.AreaId = IDQuYu;
            searchCriteria.TypeId = IDTypeId;
            searchCriteria.HotId = 0;
            searchCriteria.Keys = Request["keys"];

            ViewBag.Keys = searchCriteria.Keys;

            var searchResult = this._iOpenShopService.SearchOpenShop(searchCriteria);

            int PageCount = 1;
            if (searchResult.Item2.Total == 0)
            {
                PageCount = 0;
            }
            else if (searchResult.Item2.Total <= searchResult.Item2.Top)
            {
                PageCount = 1;
            }
            else
            {
                int mote = searchResult.Item2.Total % searchResult.Item2.Top;
                if (mote == 0)
                {
                    PageCount = searchResult.Item2.Total / searchResult.Item2.Top;
                }
                else
                {
                    PageCount = searchResult.Item2.Total / searchResult.Item2.Top + 1;
                }
            }
            ViewBag.pager = GetPageIndexFilter(queryString, PageIndex, PageCount);
            ViewBag.AttributeValuesId = AttributeValuesId;
            ViewBag.Attributes = this._iShopAttributesService.GetListByAttributeId(AttrIndustryId);

            return View(searchResult);
        }

        public ActionResult OpenShop(int id = 1)
        {
            ViewBag.Attributes = this._iShopAttributesService.GetListByAttributeId(AttrIndustryId);
            var model = this._iOpenShopService.GetById(id);

            return View(model);
        }

        public ActionResult img(int id = 1)
        {
            var model = this._iOpenShopService.GetById(id);

            return View(model);
        }

        private string FilterBuilder(int TypeID, int QuyuID, int HotId, string queryString)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbclear = new StringBuilder();

            string regkey = "";

            #region 状态 144 出售， 145 出租
            //regkey = "typeid";
            //sb.Append("<li>");
            //// span title 
            //string statusTitle = "类型";
            //var StatusRane = _iShopAttributesService.GetListByAttributeId(AttrTypeId);
            ////var statusItem = ( from m in StatusRane 
            ////                       where  m.Id == TypeID select m).FirstOrDefault();
            ////if (statusItem != null)
            ////{
            ////    statusTitle = statusItem.ValueStr;
            ////}

            //sb.Append("<span class=\"current-condition\">" + statusTitle + "</span> " +
            //              "<i class=\"icon-arrow\"></i>");
            //sb.Append("<ul id=\"subMenuOrder\" class=\"submenu\">");

            //string[] regStatusArray = new string[] { regkey + @"=DI(\d+\&?)" };
            ////不限
            //// sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regStatusArray, StatusID, "类型"));

            ////列表
            //foreach (var AttrValueItem in StatusRane)
            //{
            //    if (AttrValueItem.Id == TypeID)
            //    {
            //        sb.AppendFormat("<li class=\"active\">{0}</li>", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
            //    }
            //    else
            //    {
            //        sb.AppendFormat("<li class=\"\">{0}</li>", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
            //    }
            //}
            //sb.Append(" </ul></li>");
            #endregion

            #region 类型
            regkey = "typeid";
            sb.Append("<li>");
            // span title 
            string TypeTitle = "类型";
            var TypeItemList = _iShopAttributesService.GetListByAttributeId(AttrTypeId);
            var TypeItem = TypeItemList.Where(ｍ => ｍ.Id == TypeID).FirstOrDefault();
            if (TypeItem != null)
            {
                TypeTitle = TypeItem.ValueStr;
            }

            sb.Append("<span class=\"current-condition\">" + TypeTitle + "</span> " +
                          "<i class=\"icon-arrow\"></i>");
            sb.Append("<div id=\"subMenuArea\" class=\"submenu\"><ul class=\"clearfix submenu-type-ul\">");
            string[] TypeArray = new string[] { @"typeid=DI(\d+\&?)" };
            string _TyperesetLink = GetHrefLinkReset(queryString, TypeArray, TypeID, "不限");
            sb.AppendFormat(_TyperesetLink);
            //列表
            foreach (var AttrValueItem in TypeItemList)
            {
                if (AttrValueItem.Id == TypeID)
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

            #region 区域
            regkey = "quyu";
            sb.Append("<li>");
            // span title 
            string AreaTitle = "全部区域";
            var AreaItemList = _iShopAttributesService.GetListByAttributeId(AttrQuyuId);
            var AreaItem = AreaItemList.Where(ｍ => ｍ.Id == QuyuID).FirstOrDefault();
            if (AreaItem != null)
            {
                AreaTitle = AreaItem.ValueStr;
            }

            sb.Append("<span class=\"current-condition\">" + AreaTitle + "</span> " +
                          "<i class=\"icon-arrow\"></i>");
            sb.Append("<div id=\"subMenuArea\" class=\"submenu\"><ul class=\"clearfix submenu-type-ul\">");
            string[] quyuArray = new string[] { @"quyu=DI(\d+\&?)" };
            string _resetLink = GetHrefLinkReset(queryString, quyuArray, QuyuID, "不限");
            sb.AppendFormat(_resetLink);
            //列表
            foreach (var AttrValueItem in AreaItemList)
            {
                if (AttrValueItem.Id == QuyuID)
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


    }
}
