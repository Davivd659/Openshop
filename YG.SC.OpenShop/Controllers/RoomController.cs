using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using YG.SC.Service.IService;
using YG.SC.Model;
using YG.SC.DataAccess;

namespace YG.SC.OpenShop.Controllers
{
    public class RoomController : Controller
    {
        //
        // GET: /Room/
        private const int AttrIndustryId = 9;
        private const int AttrQuyuId = 3;
        private const int AttrSubwayId = 8;
        private const int AttrTypeId = 1;
        private const int AttrPriceRentId = 12;
        private const int AttrPriceSaleId = 11;
        private const int AttrStatus = 10;
        private const int AttrOpenTimeId = 13;

        private readonly IShopRoomService _iShopRoomService;
        private readonly IShopAttributesService _iShopAttributesService;
        private readonly ICustomerLogService _ICustomerLogService;

        public RoomController(IShopAttributesService iShopAttributesService, IShopRoomService iShopRoomService, ICustomerLogService iCustomerLogService)
        {
            _ICustomerLogService = iCustomerLogService;
            _iShopRoomService = iShopRoomService;
            _iShopAttributesService = iShopAttributesService;
        }


        protected override void Dispose(bool disposing)
        {
            this._iShopAttributesService.Dispose();
            this._iShopRoomService.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Search()
        {
            ViewBag.Title = "找商铺";
            string str_QuYu = Request.QueryString["quyu"] ?? null;// 城区
            string str_PriceRent = Request.QueryString["pricerent"] ?? null;// pricerent 
            string str_PriceSale = Request.QueryString["pricesale"] ?? null; // sale
            string str_Status = Request.QueryString["status"] ?? null; // 出租出售
            string str_OpenTime = Request.QueryString["opentime"] ?? null; // 开盘时间

            int IDQuYu = GetID(str_QuYu);
            int IDPriceRent = GetID(str_PriceRent);
            int IDPriceSale = GetID(str_PriceSale);
            int IDStatus = GetID(str_Status);
            int IDPrice = 0;
            string isSale = "";
            int OpenTimeId = GetID(str_OpenTime);
            if (IDStatus == 145)
            {
                //  出租 
                IDStatus = 145;
                IDPrice = IDPriceRent;
                IDPriceSale = 0;
                isSale = "false";
            }
            else if (IDStatus == 144)
            {
                IDPrice = IDPriceSale;
                IDPriceRent = 0;
                isSale = "true";
            }
            string queryString = Request.Url.PathAndQuery;
            ViewBag.Filter = FilterBuilder(IDQuYu, IDPrice, IDStatus, OpenTimeId, queryString);
            // search 
            int PageIndex = 1; // 页码从1 开始 
            int.TryParse(Request.QueryString["PageIndex"], out PageIndex);
            if (PageIndex == 0) { PageIndex = 1; }

            var searchCriteria = new ShopRoomCriteria();
            searchCriteria.PageIndex = PageIndex;
            searchCriteria.PageSize = 5;

            // searchCriteria.AreaId = IDQuYu;
            searchCriteria.PriceRentId = IDPriceRent;
            searchCriteria.PriceSaleId = IDPriceSale;
            searchCriteria.isSale = isSale;
            searchCriteria.OpenTimeId = GetDaysByOpenTime(OpenTimeId);

            searchCriteria.Keys = Request["keys"];
            ViewBag.Keys = searchCriteria.Keys;

            var AreaItemList = _iShopAttributesService.GetListByAttributeId(AttrQuyuId);
            var AreaItem = AreaItemList.Where(ｍ => ｍ.Id == IDQuYu).FirstOrDefault();
            if (AreaItem != null)
            {
                searchCriteria.distinct = AreaItem.ValueStr;
            }

            var searchResult = _iShopRoomService.GetSearch(searchCriteria);

            ViewBag.searchResult = searchResult;

            // 分页
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

            return View();
        }

        /// <summary>
        /// 瀑布流分页部分视图。
        /// </summary>
        /// <param name="keys">关键词。</param>
        /// <param name="quyu">区域。</param>
        /// <param name="status">状态。</param>
        /// <param name="pricerent">租金区间。</param>
        /// <param name="pricesale">售价区间。</param>
        /// <param name="opentime">开盘时间。</param>
        /// <param name="PageIndex">页码。</param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult SearchView(string keys, int? quyu, int? status, int? priceRent, int? priceSale, int? opentime, int? PageIndex)
        {
            var searchCriteria = new ShopRoomCriteria();
            searchCriteria.Keys = keys == "undefined" ? string.Empty : keys;
            searchCriteria.AreaId = quyu;
            string isSale;
            GetStatusAndPrice(status, out isSale, ref priceRent, ref priceSale);
            searchCriteria.isSale = isSale;
            searchCriteria.PriceRentId = priceRent.Value;
            searchCriteria.PriceSaleId = priceSale.Value;
            searchCriteria.OpenTimeId = GetDaysByOpenTime(opentime);
            searchCriteria.PageIndex = PageIndex ?? 1;
            searchCriteria.PageSize = 5;
            var searchResult = _iShopRoomService.GetSearch(searchCriteria);
            ViewBag.searchResult = searchResult;
            return PartialView();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult room(int id)
        {
            DataAccess.ShopRoom model = new DataAccess.ShopRoom();
            if (id > 0)
            {
                model = this._iShopRoomService.GetById(id);

                var searchCriteria = new ShopRoomCriteria();
                searchCriteria.PageIndex = 1;
                searchCriteria.PageSize = 10;
                searchCriteria.city = model.city;
                searchCriteria.distinct = model.district;
                var searchResult = _iShopRoomService.GetSearch(searchCriteria);
                ViewBag.searchResult = searchResult;
                CustomerLog log = new CustomerLog()
                {
                    Customer = YG.SC.OpenShop.UserContext.Current.Id,
                    addtime = DateTime.Now,
                    Targetsubject = 2,
                    ip = Request.UserHostAddress,
                    ProjectId = model.Id
                };
                this._ICustomerLogService.Insert(log);
            }
            return View(model);
        }

        #region 公共方法

        /// <summary>
        /// 获取状态和价格。
        /// </summary>
        /// <param name="status">状态代码（144：出售；145：出租）。</param>
        /// <param name="isSale">状态输出参数，空：不限；false：出租；true：出售。</param>
        /// <param name="priceRent">租金区间。</param>
        /// <param name="priceSale">售价区间。</param>
        private void GetStatusAndPrice(int? status, out string isSale, ref int? priceRent, ref int? priceSale)
        {
            isSale = "";
            priceRent = 0;
            priceSale = 0;
            if (!status.HasValue)
            {
                return;
            }
            if (status == 145)//出租。
            {
                priceSale = 0;
                isSale = "false";
            }
            else if (status == 144)//出售。
            {
                priceRent = 0;
                isSale = "true";
            }
        }

        /// <summary>
        /// 获取天数。
        /// </summary>
        /// <param name="openTimeId">开盘时间区间。</param>
        /// <returns></returns>
        private static int GetDaysByOpenTime(int? openTimeId)
        {
            if (!openTimeId.HasValue)
            {
                return 0;
            }
            int days = 0;
            if (openTimeId == 158)
            {
                days = 30;
            }
            else if (openTimeId == 159)
            {
                days = 90;
            }
            else if (openTimeId == 160)
            {
                days = 182;
            }
            else if (openTimeId == 161)
            {
                days = 365;
            }
            return days;
        }
        private string FilterBuilder(int QuyuID, int PriceID, int StatusID, int OpenTimeId, string queryString)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbclear = new StringBuilder();
            #region 区域
            string regkey = "quyu";
            sb.Append("<div class=\"project_st_sl\"><div class=\"project_st_sl_nav \"><div class=\"project_st_sl_nav_area\"></div></div><div class=\"project_st_sl_ul\"><div class=\"project_st_sl_li\">");
            string[] regQuYuArray = new string[] { regkey + @"=DI(\d+\&?)" };
            //不限
            sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regQuYuArray, QuyuID, "不限"));
            var QuYuRange = _iShopAttributesService.GetListByAttributeId(AttrQuyuId);
            //列表
            foreach (var AttrValueItem in QuYuRange)
            {
                if (AttrValueItem.Id == QuyuID)
                {
                    sb.AppendFormat("<a class='on'>{0}</a>", AttrValueItem.ValueStr);
                    string strclear = GetHrefLinkClear(queryString, regQuYuArray, QuyuID, AttrValueItem.ValueStr);
                    sbclear.Append(strclear);
                }
                else
                {
                    sb.AppendFormat("{0}", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append("</div></div><div class=\"clear\"></div></div>");
            #endregion


            #region 状态 144 出售， 145 出租
            regkey = "status";
            sb.Append("<div class=\"project_st_sl\"><div class=\"project_st_sl_nav \"><div class=\"project_st_sl_nav_status\"></div></div><div class=\"project_st_sl_ul\"><div class=\"project_st_sl_li\">");
            string[] regStatusArray = new string[] { regkey + @"=DI(\d+\&?)" };
            //不限
            sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regStatusArray, StatusID, "不限"));
            var StatusRane = _iShopAttributesService.GetListByAttributeId(AttrStatus);
            //列表
            foreach (var AttrValueItem in StatusRane)
            {
                if (AttrValueItem.Id == StatusID)
                {
                    sb.AppendFormat("<a class='on'>{0}</a>", AttrValueItem.ValueStr);
                    string strclear = GetHrefLinkClear(queryString, regStatusArray, StatusID, AttrValueItem.ValueStr);
                    sbclear.Append(strclear);
                }
                else
                {
                    sb.AppendFormat("{0}", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append("</div></div><div class=\"clear\"></div></div>");
            #endregion

            // 价格 
            #region 价格
            if (StatusID > 0)
            {
                regkey = "pricesale";
                int AttrPriceId = AttrPriceSaleId;
                if (StatusID == 145)
                {
                    regkey = "pricerent";
                    AttrPriceId = AttrPriceRentId;
                }
                sb.Append("<div class=\"project_st_sl\"><div class=\"project_st_sl_nav \"><div class=\"project_st_sl_nav_price\"></div></div><div class=\"project_st_sl_ul\"><div class=\"project_st_sl_li\">");
                string[] regPriceArray = new string[] { regkey + @"=DI(\d+\&?)" };
                //不限
                sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regPriceArray, PriceID, "不限"));
                if (StatusID == 145)
                {
                    // rent 
                    var PriceRane = _iShopAttributesService.GetBasPriceRentRange();
                    //列表
                    foreach (var AttrValueItem in PriceRane)
                    {
                        if (AttrValueItem.Id == PriceID)
                        {
                            sb.AppendFormat("<a class='on'>{0}</a>", AttrValueItem.RPName);
                            string strclear = GetHrefLinkClear(queryString, regPriceArray, PriceID, AttrValueItem.RPName);
                            sbclear.Append(strclear);
                        }
                        else
                        {
                            sb.AppendFormat("{0}", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.RPName));
                        }
                    }
                }
                else
                {
                    // sale 
                    //列表
                    var PriceRane = _iShopAttributesService.GetBasPriceSaleRange();
                    foreach (var AttrValueItem in PriceRane)
                    {
                        if (AttrValueItem.PRID == PriceID)
                        {
                            sb.AppendFormat("<a class='on'>{0}</a>", AttrValueItem.PRName);
                            string strclear = GetHrefLinkClear(queryString, regPriceArray, PriceID, AttrValueItem.PRName);
                            sbclear.Append(strclear);
                        }
                        else
                        {
                            sb.AppendFormat("{0}", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.PRID, AttrValueItem.PRName));
                        }
                    }
                }
                sb.Append("</div></div><div class=\"clear\"></div></div>");
            }
            #endregion


            #region 开盘
            regkey = "opentime";
            sb.Append("<div class=\"project_st_sl\"><div class=\"project_st_sl_nav \"><div class=\"project_st_sl_nav_opentime\"></div></div><div class=\"project_st_sl_ul project_st_sl_ul_opentime \"><div class=\"project_st_sl_li\">");
            string[] regOpenTimeArray = new string[] { regkey + @"=DI(\d+\&?)" };
            //不限
            sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regOpenTimeArray, OpenTimeId, "不限"));
            var OpenTimeRange = _iShopAttributesService.GetListByAttributeId(AttrOpenTimeId);
            //列表
            foreach (var AttrValueItem in OpenTimeRange)
            {
                if (AttrValueItem.Id == OpenTimeId)
                {
                    sb.AppendFormat("<a class='on'>{0}</a>", AttrValueItem.ValueStr);
                    string strclear = GetHrefLinkClear(queryString, regOpenTimeArray, OpenTimeId, AttrValueItem.ValueStr);
                    sbclear.Append(strclear);
                }
                else
                {
                    sb.AppendFormat("{0}", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append("</div></div><div class=\"clear\"></div></div>");
            #endregion
            string clearlink = sbclear.ToString();
            if (!string.IsNullOrEmpty(clearlink))
            {
                sb.Append("<div class=\"project_st_sl\"><div class=\"project_st_sl_nav\"><div class=\"project_st_sl_nav_check\"></div></div><div class=\"project_st_sl_ul\"><div class=\"project_st_sl_li\">");
                sb.Append(sbclear.ToString());
                sb.Append("</div></div><div class=\"clear\"></div></div>");
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

            sb.AppendFormat("<a href='{0}'>{1}</a>", link.Replace("?&", "?"), targetName);

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
            queryString = Regex.Replace(queryString, @"PageIndex=(\d+\.?\d*|\.\d+)", "");

            StringBuilder sb = new StringBuilder();
            string link = null;

            if (id == 0)
            {
                sb.Append("<a class='on'>不限</a>");
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
                    //link = link.Replace("?", "");
                    link = link.Substring(0, link.LastIndexOf('?'));
                }

                if (link.LastIndexOf('&') == (link.Length - 1))
                {
                    //link = link.Replace("&", "");
                    link = link.Substring(0, link.LastIndexOf('&'));
                }

                sb.AppendFormat("<a href='{0}'>{1}</a>", link, targetName);
            }
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
