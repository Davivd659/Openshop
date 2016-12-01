using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Model.Project;
using YG.SC.WeiXin.Models.project;
using YG.SC.Service.IService;
using System.Text;
using System.Text.RegularExpressions;

using YG.SC.WeiXin.Models; 

namespace YG.SC.WeiXin.Controllers
{
    public class PostingsController : BaseController
    {
        private const int AttrIndustryId = 9;
        private const int AttrQuyuId = 3;
        private const int AttrSubwayId = 8;
        private const int AttrTypeId = 1;
        private const int AttrPriceRentId = 12;
        private const int AttrPriceSaleId = 11;
        private const int AttrStatus = 10;
        private const int AttrOpenTimeId = 13;

        private readonly IShopPostingsService _iShopPostingsService;
        private readonly IShopAttributesService _iShopAttributesService;

        public PostingsController(IShopPostingsService iShopPostingsService, IShopAttributesService iShopAttributesService)
        {
            _iShopPostingsService = iShopPostingsService;
            _iShopAttributesService = iShopAttributesService;
        }


        protected override void Dispose(bool disposing)
        {
            this._iShopAttributesService.Dispose();
            this._iShopPostingsService.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult List()
        {
            ViewBag.Title = "找需求";
            string str_QuYu = Request.QueryString["quyu"] ?? null;// 城区
            string str_type = Request.QueryString["type"] ?? null;// 物业类型
            string str_Status = Request.QueryString["status"] ?? null; // 出租出售

            int IDQuYu = GetID(str_QuYu);
            int IDType = GetID(str_type);
            int IDStatus = GetID(str_Status);
            string isSale = "";
            if (IDStatus == 0 || IDStatus == 197)
            {
                //  求租
                IDStatus = 197;
                isSale = "false";
            }
            else if (IDStatus == 198)
            {
                isSale = "true";
            }
            string queryString = Request.Url.PathAndQuery;
            ViewBag.Filter = FilterBuilder(IDStatus, IDQuYu, IDType, queryString);
            // search 
            int PageIndex = 1; // 页码从1 开始 
            int.TryParse(Request.QueryString["PageIndex"], out PageIndex);
            if (PageIndex == 0) { PageIndex = 1; }

            var searchCriteria = new ShopPostingsCriteria();
            searchCriteria.PageIndex = PageIndex;
            searchCriteria.PageSize = 6;

            // searchCriteria.AreaId = IDQuYu;
            searchCriteria.isSale = isSale;
            searchCriteria.Keys = Request["keys"];
            ViewBag.Keys = searchCriteria.Keys;

            var AreaItemList = _iShopAttributesService.GetListByAttributeId(AttrQuyuId);
            var AreaItem = AreaItemList.Where(ｍ => ｍ.Id == IDQuYu).FirstOrDefault();
            if (AreaItem != null)
            {
                searchCriteria.distinct = AreaItem.ValueStr;
            }

            var TypeItemList = _iShopAttributesService.GetListByAttributeId(AttrTypeId);
            var TypeItem = TypeItemList.Where(ｍ => ｍ.Id == IDType).FirstOrDefault();
            if (TypeItem != null)
            {
                searchCriteria.PType = TypeItem.ValueStr;
            }

            var searchResult = _iShopPostingsService.GetSearch(searchCriteria);

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

        public ActionResult Index(int id=1)
        {
            var model = this._iShopPostingsService.GetById(id);
            return View(model);
           
        }

        public ActionResult Add()
        {
            // 区域列表 
            var AreaItemList = _iShopAttributesService.GetListByAttributeId(AttrQuyuId);
            // 物业类型
            var TypeItem = _iShopAttributesService.GetListByAttributeId(AttrTypeId);

            ViewBag.areaList = (from m in AreaItemList
                                select new SelectListItem
                                {
                                    Text = m.ValueStr ,
                                    Value = m.ValueStr
                                }).ToList();
            ViewBag.TypeList = (from m in TypeItem
                                select new SelectListItem
                                {
                                    Text = m.ValueStr,
                                    Value = m.ValueStr
                                }).ToList();

            return View();

        }
        [HttpPost]
        [ActionName("Add")]
        public ActionResult AddPostings(PostingModel addModel)
        {
            var postModel = Mapper.Map<PostingModel, ShopPostings>(addModel);
            if (addModel.yixiang == "True")
            {
                postModel.PIntent = "True";
            }
            else
            {
                postModel.PIntent = "False";
            }

            postModel.city = "北京";
            postModel.Addtiem = DateTime.Now;
            postModel.hotarea = "";

            _iShopPostingsService.Insert(postModel);

            return Redirect("List");
        }


        #region 公共方法
        private string FilterBuilder(int StatusID, int QuyuID, int IDType, string queryString)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbclear = new StringBuilder();

            string regkey = "";

            #region 状态 198 求购， 197 求租
            regkey = "status";
            sb.Append("<li>");
            // span title 
            string statusTitle = "类型";
            var StatusRane = _iShopAttributesService.GetListByAttributeId(21);
            var statusItem = StatusRane.Where(ｍ => ｍ.Id == StatusID).FirstOrDefault();
            if (statusItem != null)
            {
                statusTitle = statusItem.ValueStr;
            }

            sb.Append("<span class=\"current-condition\">" + statusTitle + "</span> " +
                          "<i class=\"icon-arrow\"></i>");
            sb.Append("<ul id=\"subMenuOrder\" class=\"submenu\">");

            string[] regStatusArray = new string[] { regkey + @"=DI(\d+\&?)" };
            //不限
            // sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regStatusArray, StatusID, "类型"));

            //列表
            foreach (var AttrValueItem in StatusRane)
            {
                if (AttrValueItem.Id == StatusID)
                {
                    sb.AppendFormat("<li class=\"active\">{0}</li>", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
                else
                {
                    sb.AppendFormat("<li class=\"\">{0}</li>", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append(" </ul></li>");
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

            //// 物业类型  
            #region 
            regkey = "type";
            sb.Append("<li>");
            // span title 
            string typeTitle = "物业类型";
            var TypeItemList = _iShopAttributesService.GetListByAttributeId(AttrTypeId);
            var TypeItem = TypeItemList.Where(ｍ => ｍ.Id == IDType).FirstOrDefault();
            if (TypeItem != null)
            {
                typeTitle = TypeItem.ValueStr;
            }

            sb.Append("<span class=\"current-condition\">" + typeTitle + "</span> " +
                          "<i class=\"icon-arrow\"></i>");
            sb.Append("<div id=\"subMenuArea\" class=\"submenu\"><ul class=\"clearfix submenu-type-ul\">");
            string[] typeArray = new string[] { @"type=DI(\d+\&?)" };
            string _typeLink = GetHrefLinkReset(queryString, typeArray, IDType, "不限");
            sb.AppendFormat(_typeLink);
            //列表
            foreach (var AttrValueItem in TypeItemList)
            {
                if (AttrValueItem.Id == IDType)
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

        #endregion


    }
}
