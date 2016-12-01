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
using YG.SC.OpenShop.Models.project;
using YG.SC.Service.IService;
using System.Text;
using System.Text.RegularExpressions;
using YG.SC.Common.AttributeExtention;
using YG.SC.OpenShop.Models;


namespace YG.SC.OpenShop.Controllers
{
    public class DemandController : BaseController
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
        private readonly IShopProjectService _IShopProjectService;

        public DemandController(IShopPostingsService iShopPostingsService, IShopAttributesService iShopAttributesService, IShopProjectService iShopProjectService)
        {
            _iShopPostingsService = iShopPostingsService;
            _iShopAttributesService = iShopAttributesService;
            _IShopProjectService = iShopProjectService;
        }

        protected override void Dispose(bool disposing)
        {
            this._IShopProjectService.Dispose();
            this._iShopAttributesService.Dispose();
            this._iShopPostingsService.Dispose();
            base.Dispose(disposing);
        }



        //
        // GET: /Demand/

        public ActionResult Index()
        {
            return View();
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

            ViewBag.pageCount = 0;
            ViewBag.pager = GetPageIndexFilter(queryString, PageIndex, PageCount);
            var tuijian = new ProjectSearchCriteria();
            tuijian.Projecthot = 1;
            var tuijianResult = _IShopProjectService.SearchProject(tuijian);
            var arraylist = Mapper.Map<ShopProject[], ProjectDetailsViewModel[]>(tuijianResult.Item1).OrderByDescending(p => p.CreatTime).Take(3).ToList();
            ViewBag.array = arraylist;
            return View();
        }

        public ViewResult Item(int id = 1)
        {
            var model = this._iShopPostingsService.GetById(id);
            var tuijian = new ProjectSearchCriteria();
            tuijian.Projecthot = 1;
            var tuijianResult = _IShopProjectService.SearchProject(tuijian);
            var arraylist = Mapper.Map<ShopProject[], ProjectDetailsViewModel[]>(tuijianResult.Item1).OrderByDescending(p => p.CreatTime).Take(3).ToList();
            ViewBag.array = arraylist;
            return View(model);
        }

        public ViewResult Issue()
        {
            //意向类型使用的扩展控件需要先定义选项。
            List<CodeDescription> codes = new List<CodeDescription>();
            codes.Add(new CodeDescription("False", "求租", "意向类型"));
            codes.Add(new CodeDescription("True", "求购", "意向类型"));
            CodeManager.codes = codes.ToArray();
            // 区域列表 
            List<ShopAttributeValues> AreaItemList = _iShopAttributesService.GetListByAttributeId(AttrQuyuId);
            // 物业类型
            List<ShopAttributeValues> TypeItem = _iShopAttributesService.GetListByAttributeId(AttrTypeId);
            ViewBag.AreaList = AreaItemList == null ? new List<SelectListItem>() : (from m in AreaItemList
                                                                                    select new SelectListItem
                                                                                    {
                                                                                        Text = m.ValueStr,
                                                                                        Value = m.ValueStr
                                                                                    }).ToList();
            ViewBag.TypeList = TypeItem == null ? new List<SelectListItem>() : (from m in TypeItem
                                                                                select new SelectListItem
                                                                                {
                                                                                    Text = m.ValueStr,
                                                                                    Value = m.ValueStr
                                                                                }).ToList();

            return View("Issue");
        }


        [HttpPost]
        public ViewResult Issue(Demand model)
        {
            // 区域列表 
            List<ShopAttributeValues> AreaItemList = _iShopAttributesService.GetListByAttributeId(AttrQuyuId);
            // 物业类型
            List<ShopAttributeValues> TypeItem = _iShopAttributesService.GetListByAttributeId(AttrTypeId);
            ViewBag.AreaList = AreaItemList == null ? new List<SelectListItem>() : (from m in AreaItemList
                                                                                    select new SelectListItem
                                                                                    {
                                                                                        Text = m.ValueStr,
                                                                                        Value = m.ValueStr
                                                                                    }).ToList();
            ViewBag.TypeList = TypeItem == null ? new List<SelectListItem>() : (from m in TypeItem
                                                                                select new SelectListItem
                                                                                {
                                                                                    Text = m.ValueStr,
                                                                                    Value = m.ValueStr
                                                                                }).ToList();
            if (!ModelState.IsValid)
            {
                return View("Issue");
            }
            ShopPostings postModel = new ShopPostings();
            postModel.PIntent = model.Type;
            postModel.PName = model.Title;
            postModel.city = "北京";
            postModel.Format = model.Format;
            postModel.district = model.Area;
            postModel.square = model.Square;
            postModel.price = model.Price;
            postModel.Ptype = model.PType;
            postModel.Pinfo = model.Details;
            postModel.Endtiem = model.EndDate;
            postModel.Contacts = model.Contact;
            postModel.Mobile = model.Mobile;
            postModel.Addtiem = DateTime.Now;
            _iShopPostingsService.Insert(postModel);
            return View("Succeed");
        }

        public ViewResult Succeed()
        {
            return View();
        }


        #region 公共方法
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="StatusID"></param>
        /// <param name="QuyuID"></param>
        /// <param name="IDType"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
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

            sb.Append("<span><img src='../Images/icon/demandlist1.png' />意向类型</span>");

            string[] regStatusArray = new string[] { regkey + @"=DI(\d+\&?)" };
            //不限
            // sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regStatusArray, StatusID, "类型"));

            //列表
            foreach (var AttrValueItem in StatusRane)
            {
                if (AttrValueItem.Id == StatusID)
                {
                    sb.AppendFormat("<a class=\"red\" href=\"{1}\">{0}</a>", AttrValueItem.ValueStr, GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
                else
                {
                    sb.AppendFormat("<a class=\"\" href=\"{1}\">{0}</a>", AttrValueItem.ValueStr, GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append(" </li>");
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

            sb.Append("<span><img src=\"../Images/icon/demandlist2.png\" />物业类型</span>");
            string[] typeArray = new string[] { @"type=DI(\d+\&?)" };
            string _typeLink = GetHrefLinkReset(queryString, typeArray, IDType, "不限");
            sb.AppendFormat(_typeLink);
            //列表
            foreach (var AttrValueItem in TypeItemList)
            {
                if (AttrValueItem.Id == IDType)
                {
                    sb.AppendFormat("<a class=\"red\" href=\"{1}\">{0}</a>", AttrValueItem.ValueStr, GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
                else
                {
                    sb.AppendFormat("<a class=\"\" href=\"{1}\">{0}</a>", AttrValueItem.ValueStr, GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append(" </li>");


            #endregion

            #region 区域
            regkey = "quyu";
            sb.Append("<li>");
            // span title 
            // string AreaTitle = "全部区域";
            var AreaItemList = _iShopAttributesService.GetListByAttributeId(AttrQuyuId);
            var AreaItem = AreaItemList.Where(ｍ => ｍ.Id == QuyuID).FirstOrDefault();
            //if (AreaItem != null)
            //{
            //    AreaTitle = AreaItem.ValueStr;
            //}

            sb.Append("<span><img src=\"../Images/icon/demandlist3.png\" />意向区域</span><div style='float:left;width:1000px;'>");
            string[] quyuArray = new string[] { @"quyu=DI(\d+\&?)" };
            string _resetLink = GetHrefLinkReset(queryString, quyuArray, QuyuID, "不限");
            sb.AppendFormat(_resetLink);
            //列表
            foreach (var AttrValueItem in AreaItemList)
            {
                if (AttrValueItem.Id == QuyuID)
                {
                    sb.AppendFormat("<a class=\"red\"  href=\"{1}\">{0}</a>", AttrValueItem.ValueStr, GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
                else
                {
                    sb.AppendFormat("<a class=\"\" href=\"{1}\">{0}</a>", AttrValueItem.ValueStr, GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append(" </div></li>");


            #endregion

           

            string clearlink = sbclear.ToString();
            if (!string.IsNullOrEmpty(clearlink))
            {
            }
            return sb.ToString();
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PageTotal"></param>
        /// <returns></returns>
        private string GetPageIndexFilter(string queryString, int pageIndex, int PageTotal)
        {
            string regkey = "PageIndex";
            string currentPageLink = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", pageIndex, "");
            StringBuilder sb = new StringBuilder();

            if (pageIndex != 1)
            {
                string _link = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", pageIndex - 1, "");
                sb.Append("<span class=\"rpbtn\"><a href=\"" + _link + "\">上一页</a></span>");
            }
            else
            {
                sb.Append("<span class=\"pbtn\"><a>上一页</a></span>");
            }
            if (pageIndex - 2 > 0)
            {
                for (int i = pageIndex - 2; i < pageIndex; i++)
                {
                    string _link = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", i, "");
                    sb.AppendFormat("<span class=\"ortherNo\"><a href=\"{0}\">" + i + "</a></span>", _link);
                }
            }
            else
            {
                for (int i = 1; i < pageIndex; i++)
                {
                    string _link = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", i, "");
                    sb.AppendFormat("<span class=\"ortherNo\"><a href=\"{0}\">" + i + "</a></span>", _link);
                }
            }
            //以上为pageNo前边内容
            sb.Append("<span class=\"nowNo\"><a>" + pageIndex + "</a></span>");
            //以下为pageNo前边内容
            if (pageIndex + 2 < PageTotal)
            {
                for (int i = pageIndex + 1; i < pageIndex + 3; i++)
                {
                    string _link = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", i, "");
                    sb.AppendFormat("<span class=\"ortherNo\"><a href=\"{0}\">" + i + "</a></span>", _link);
                }
            }
            else
            {
                for (int i = pageIndex + 1; i < PageTotal + 1; i++)
                {
                    string _link = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", i, "");
                    sb.AppendFormat("<span class=\"ortherNo\"><a href=\"{0}\">" + i + "</a></span>", _link);
                }
            }
            if (pageIndex < PageTotal)
            {
                string _link = GetHrefLink(queryString, regkey + @"=(\d+)", "PageIndex=", pageIndex + 1, "");
                sb.Append("<span class=\"rpbtn\"><a href=\"" + _link + "\">下一页</a></span>");
            }
            else
            {
                sb.Append("<span class=\"pbtn\"><a>下一页</a></span>");
            }

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
            sb.Append(link.Replace("?&", "?"));

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

            sb.AppendFormat("<a href=\"{1}\" class=\"needsclick\">不限</a>", _Active, link);

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
