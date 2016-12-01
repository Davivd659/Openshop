using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Model;
using YG.SC.Model.Project;
using YG.SC.OpenShop.Models.project;
using YG.SC.Service.IService;
using YG.SC.WeiXin.Models;
using YG.SC.OpenShop.Models;

namespace YG.SC.OpenShop.Controllers
{
    public class ProjectController : Controller
    {
        private const int AttrIndustryId = 9;
        private const int AttrQuyuId = 3;
        private const int AttrSubwayId = 8;
        private const int AttrTypeId = 1;
        private const int AttrPriceRentId = 12;
        private const int AttrPriceSaleId = 11;
        private const int AttrStatus = 10;
        private const int AttrOpenTimeId = 13;

        // GET: /ShopProjectDetails/

        private readonly IShopProjectService _iShopProjectService;
        private readonly IProjectPhotoService _iProjectPhotoService;
        private readonly IShopAttributesService _iShopAttributesService;
        private readonly IProjectTeamService _iProjectTeamService;
        private readonly IApplyActiviteService _iApplyActiviteService;
        private readonly ICustomerLogService _ICustomerLogService;
        private readonly ILinkService _ilinkService;
        private readonly ICustomerService _iCustomerService;
        private readonly IJoinProjectService _iJoinProjectService;

        public ProjectController(IProjectPhotoService iProjectPhotoService,
            IShopProjectService iShopProjectService, IShopAttributesService iShopAttributesService,
            IProjectTeamService iProjectTeamService, IApplyActiviteService iApplyActiviteService,
            ICustomerLogService ICustomerLogService, ILinkService ilinkService, ICustomerService CustomerService, IJoinProjectService JoinProjectService)
        {
            _iProjectPhotoService = iProjectPhotoService;
            _iShopProjectService = iShopProjectService;
            _iShopAttributesService = iShopAttributesService;
            _iProjectTeamService = iProjectTeamService;
            _iApplyActiviteService = iApplyActiviteService;
            _ICustomerLogService = ICustomerLogService;
            _ilinkService = ilinkService;
            _iCustomerService = CustomerService;
            _iJoinProjectService = JoinProjectService;
        }

        public ActionResult Index(int id = 1)
        {
            int projectId = id; //从别的页面传过来的
            var shopProjectList = _iShopProjectService.GetById(projectId);
            var model = Mapper.Map<ShopProject, ProjectDetailsViewModel>(shopProjectList);

            var ProjectServiceList = _iProjectTeamService.GetAll().Where(p => p.ShopProjectId == projectId && p.Status == 1).ToList();
            var projectServiceList = Mapper.Map<List<ProjectService>, List<ProjectServiceViewModel>>(ProjectServiceList);
            model.ProjectServiceViewModels = projectServiceList;
            if (shopProjectList.Grouppurchases.Count > 0)
            {
                var Grouppurchases = shopProjectList.Grouppurchases.OrderBy(m => m.Addtime).FirstOrDefault(m => (m.Status == 0 && m.Begintime < DateTime.Now && m.Endtime > DateTime.Now));
                ViewBag.jzsj = Grouppurchases.Endtime.ToString();
                model.GrouppurchaseId = Grouppurchases.Id;
                ViewBag.Groupcount = Grouppurchases.ApplyActivities.Count();
            }
            ViewBag.LinkTypes = this._ilinkService.GetAll();
            // 项目图片
            int pg = 1;
            var projectPhoto = this._iProjectPhotoService.GetEntitsByImageName(projectId);
            ViewBag.PhotosList = projectPhoto;
            CustomerLog log = new CustomerLog()
            {
                addtime = System.DateTime.Now,
                Customer = YG.SC.OpenShop.UserContext.Current.Id,
                ip = Request.UserHostAddress,
                Targetsubject = 1,
                ProjectId = projectId
            };

            _ICustomerLogService.Insert(log);
            return View(model);
        }

        [HttpPost]
        public ActionResult Apply(ProjectApplyModel model)
        {
            var shopProjectList = _iShopProjectService.GetById(model.ProjectId);
            var jsonResult = new JsonResult();
            if (shopProjectList != null)
            {
                var applyActivity = new ApplyActivity
                {
                    Name = model.ApplyName,
                    Phone = model.ApplyPhone,
                    Type = model.GroupType,
                    GrouppurchaseId = model.GrouppurchaseId,
                    UpdateDate = DateTime.Now,
                    Status = 1,
                    Grouppurchase = shopProjectList.Grouppurchases.OrderBy(m => m.Addtime).FirstOrDefault(m => (m.Status == 0 && m.Begintime < DateTime.Now && m.Endtime > DateTime.Now))
                };
                var apply = this._iApplyActiviteService.GetByPhone(model.ApplyPhone);
                if (apply.Count() == 0)
                {
                    _iApplyActiviteService.Insert(applyActivity);
                    jsonResult.Data = "true";
                }
                else
                {
                    jsonResult.Data = "0";
                }

            }
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
            return jsonResult;
        }
        [HttpGet]
        public ActionResult ApplyCustomerList(int projectId)
        {
            int topNum = 10;
            var ApplyList = _iApplyActiviteService.GetTop(projectId, topNum);

            var itemList = Mapper.Map<List<ApplyActivity>, List<ApplyModelItem>>(ApplyList);

            ViewBag.ApplyList = itemList;

            return View();
        }
        public ActionResult Search()
        {
            ViewBag.Title = "我要开店--开店ing，一站式开店服务平台";
            // string str_SaleOrRent = Request.QueryString["SaleOrRent"] ?? null;// //出租或出售 1:出租;  2:出售 默认为1 出租  
            string str_Industry = Request.QueryString["industry"] ?? null;// 行业
            string str_QuYu = Request.QueryString["quyu"] ?? null;// 城区
            string str_SubWay = Request.QueryString["subway"] ?? null;// 地铁
            string str_Type = Request.QueryString["type"] ?? null;//（类型）
            string str_PriceRent = Request.QueryString["pricerent"] ?? null;// pricerent 
            string str_PriceSale = Request.QueryString["pricesale"] ?? null; // sale
            string str_Status = Request.QueryString["status"] ?? null; // 出租出售
            string str_OpenTime = Request.QueryString["opentime"] ?? null; // 开盘时间

            // int IDSaleOrRent = GetID(str_SaleOrRent);
            int IDIndustry = GetID(str_Industry);
            int IDQuYu = GetID(str_QuYu);
            int IDSubWay = GetID(str_SubWay);
            int IDType = GetID(str_Type);
            int IDPriceRent = GetID(str_PriceRent);
            int IDPriceSale = GetID(str_PriceSale);
            int IDStatus = GetID(str_Status);
            int OpenTimeId = GetID(str_OpenTime);
            int IDPrice = 0;
            if (IDStatus == 145)
            {
                //  出租 
                IDPrice = IDPriceRent;
                IDPriceSale = 0;
            }
            else if (IDStatus == 144)
            {
                IDPrice = IDPriceSale;
                IDPriceRent = 0;
            }
            string queryString = Request.Url.PathAndQuery;
            ViewBag.Filter = FilterBuilder(IDIndustry, IDQuYu, IDSubWay, IDType, IDPrice, IDStatus, OpenTimeId, queryString);
            // search 
            int PageIndex = 1;
            int.TryParse(Request.QueryString["PageIndex"], out PageIndex);
            var searchCriteria = new ProjectSearchCriteria();
            searchCriteria.PageIndex = PageIndex;
            searchCriteria.PageSize = 6;

            searchCriteria.HangYeId = IDIndustry;
            searchCriteria.AreaId = IDQuYu;
            searchCriteria.SubWayId = IDSubWay;
            searchCriteria.WuYeleixingId = IDType;
            searchCriteria.PriceRentId = IDPriceRent;
            searchCriteria.PriceSaleId = IDPriceSale;
            searchCriteria.StatusID = IDStatus;
            if (OpenTimeId == 158)
            {
                searchCriteria.OpenTimeId = 30;
            }
            else if (OpenTimeId == 159)
            {
                searchCriteria.OpenTimeId = 90;
            }
            else if (OpenTimeId == 160)
            {
                searchCriteria.OpenTimeId = 182;
            }
            else if (OpenTimeId == 161)
            {
                searchCriteria.OpenTimeId = 365;
            }
            else
            {
                searchCriteria.OpenTimeId = 0;
            }
            searchCriteria.Keys = Request["keys"];

            ViewBag.Keys = searchCriteria.Keys;
            var searchResult = _iShopProjectService.SearchProject(searchCriteria);
            var arraylist = Mapper.Map<ShopProject[], ProjectDetailsViewModel[]>(searchResult.Item1);

            ViewBag.searchResult = searchResult;
            ViewBag.array = arraylist;

            return View();
        }

        #region 加载图片数据
        public ActionResult Getphoto(ProjectPhoto photo)
        {
            var projectPhoto = this._iProjectPhotoService.GetEntitsByImageName(photo.ShopProjectId);

            List<ProjectPhoto> PhotosList = null;
            if (photo.Type == null)
            {
                PhotosList = projectPhoto.OrderBy(M => M.Sort).Take(4).ToList();
            }
            else
            {
                PhotosList = projectPhoto.Where(m => m.Type == photo.Type).OrderByDescending(m => m.CreatTime).ToList();
            }
            var PPhoto = from m in PhotosList
                         select new
                         {
                             PhotoName = m.PhotoName,
                             PhotoUrl = m.PhotoUrl
                         };
            var jsonResult = new JsonResult();
            jsonResult.Data = PPhoto;
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return jsonResult;
        }

        #endregion
        #region 专题页面
        public ActionResult Special(int id)
        {
            JoinProject model = new JoinProject();
            if (id > 0)
            {
                model.ShopProjectId = id;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("Special")]
        public ActionResult Specialadd(JoinProject Join)
        {
            Join.Addtime = DateTime.Now;
            var model = this._iJoinProjectService.GetByPhone(Join.Phone);
            if (model.Count() == 0)
            {
                this._iJoinProjectService.Insert(Join);
                ViewBag.msg = "添加成功";
            }
            else
            {
                ViewBag.msg = "该用户已存在";
            }

            return View(Join);
        }
        public string ajaxSend(string phone)
        {
            var randomCode = new Random().Next(100000).ToString("D6");
            CheckMobileModel model = new CheckMobileModel()
            {
                Phone = phone,
                Message = string.Format(SmsConfig.Instance.RgistetSmsText, randomCode)
            };
            YG.SC.Common.SendMessageHelper smsHelper = new Common.SendMessageHelper();
            try
            {
                string smsResult = smsHelper.SendMessage(phone, model.Message);
                YG.SC.OpenShop.PCnCache.Instance.Add(model.Phone, randomCode);
                // 更新短信通道 

                SmsLog smsModel = new SmsLog()
                {
                    PhoneNumber = model.Phone,
                    SendStatus = smsResult
                    ,
                    Content = model.Message
                };
                _iCustomerService.SendSmsSaveLog(smsModel);
                return
                    "ok";

            }
            catch (Exception ex)
            {
                return "-1";
            }

        }
        public string ValidateMobile(ValidatePhoneNumber model)
        {
            // string syscode = .Instance.Get(model.Phone, randomCode);
            string CacheValue = "";
            if (!string.IsNullOrEmpty(model.Phone))
            {
                CacheValue = YG.SC.OpenShop.PCnCache.Instance.Get(model.Phone);
            }
            if (CacheValue == model.Code)
            {
                return "ok";

            }

            return "-1";
        }
        #endregion

        #region 搜索加载数据和处理方法
        [HttpGet]
        public PartialViewResult SearchView(string keys, int? industry, int? quyu, int? subway, int? type, int? pricerent, int? pricesale
            , int? status, int? opentime, int? PageIndex)
        {
            var searchCriteria = new ProjectSearchCriteria();
            searchCriteria.PageIndex = PageIndex ?? 1;
            searchCriteria.PageSize = 6;

            searchCriteria.HangYeId = industry;
            searchCriteria.AreaId = quyu;
            searchCriteria.SubWayId = subway;
            searchCriteria.WuYeleixingId = type;
            searchCriteria.PriceRentId = pricerent;
            searchCriteria.PriceSaleId = pricesale;
            searchCriteria.StatusID = status;
            if (opentime.HasValue)
            {
                searchCriteria.OpenTimeId = opentime.Value;
            }
            searchCriteria.Keys = keys == "undefined" ? string.Empty : keys;
            var searchResult = _iShopProjectService.SearchProject(searchCriteria);

            var arraylist = Mapper.Map<ShopProject[], ProjectDetailsViewModel[]>(searchResult.Item1);

            //return Tuple.Create(array, new PagerEntity { Total = total, PageIndex = idx + 1, Top = top });
            // 这里图片路径取封面图 


            ViewBag.searchResult = searchResult;
            ViewBag.array = arraylist;

            return PartialView();
        }
        /// <summary>
        /// 输出销售价格
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string GetSalePrice(object price)
        {
            if (price == null || Convert.ToDouble(price) == 0)
            {
                return "面议";
            }
            else
            {
                double _price = Convert.ToDouble(price);
                return "" + MoneyManage.ToCHSimpleMoney(_price);
                //if (_price <500)
                //{
                //    return YG.SC.Common.MoneyManage.ToCHSimpleMoney(_price) + "";
                //}
                //else
                //{
                //    return House.Components.Common.MoneyManage.ToCHSimpleMoney(_price);
                //}
            }
        }
        /// <summary>
        /// 输出销售价格
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string GetRentPrice(object price)
        {
            if (price == null || Convert.ToDouble(price) == 0)
            {
                return "面议";
            }
            else
            {
                double _price = Convert.ToDouble(price);
                return "" + MoneyManage.ToCHSimpleMoney(_price);
                //if (_price <500)
                //{
                //    return YG.SC.Common.MoneyManage.ToCHSimpleMoney(_price) + "";
                //}
                //else
                //{
                //    return House.Components.Common.MoneyManage.ToCHSimpleMoney(_price);
                //}
            }
        }
        private string FilterBuilder(int IndustryId, int QuyuID, int SubWayID, int TypeId, int PriceID, int StatusID, int OpenTimeId, string queryString)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbclear = new StringBuilder();
            #region 行业
            sb.Append("<div class=\"project_st_sl\"><div class=\"project_st_sl_nav \"><div class=\"project_st_sl_nav_hangye\"></div></div><div class=\"project_st_sl_ul\"><div class=\"project_st_sl_li\">");
            string[] regIndustryArray = new string[] { @"industry=DI(\d+\&?)" };
            //不限
            sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regIndustryArray, IndustryId, "不限"));
            var IndustryRange = _iShopAttributesService.GetListByAttributeId(AttrIndustryId);
            //列表
            foreach (var AttrValueItem in IndustryRange)
            {
                if (AttrValueItem.Id == IndustryId)
                {
                    sb.AppendFormat("<a class='on'>{0}</a>", AttrValueItem.ValueStr);
                    string strclear = GetHrefLinkClear(queryString, regIndustryArray, IndustryId, AttrValueItem.ValueStr);
                    sbclear.Append(strclear);
                }
                else
                {
                    sb.AppendFormat("{0}", GetHrefLinkCommon(queryString, @"industry=DI(\d+)", "industry=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append("</div></div><div class=\"clear\"></div></div>");
            #endregion

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

            #region 地铁
            regkey = "subway";
            sb.Append("<div class=\"project_st_sl\"><div class=\"project_st_sl_nav \"><div class=\"project_st_sl_nav_subway\"></div></div><div class=\"project_st_sl_ul\"><div class=\"project_st_sl_li\">");
            string[] regSubwayArray = new string[] { regkey + @"=DI(\d+\&?)" };
            //不限
            sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regSubwayArray, SubWayID, "不限"));
            var SubwayRange = _iShopAttributesService.GetListByAttributeId(AttrSubwayId);
            //列表
            foreach (var AttrValueItem in SubwayRange)
            {
                if (AttrValueItem.Id == SubWayID)
                {
                    sb.AppendFormat("<a class='on'>{0}</a>", AttrValueItem.ValueStr);
                    string strclear = GetHrefLinkClear(queryString, regSubwayArray, SubWayID, AttrValueItem.ValueStr);
                    sbclear.Append(strclear);
                }
                else
                {
                    sb.AppendFormat("{0}", GetHrefLinkCommon(queryString, regkey + @"=DI(\d+)", regkey + "=DI", AttrValueItem.Id, AttrValueItem.ValueStr));
                }
            }
            sb.Append("</div></div><div class=\"clear\"></div></div>");
            #endregion

            #region 类型（业态
            regkey = "type";
            sb.Append("<div class=\"project_st_sl\"><div class=\"project_st_sl_nav \"><div class=\"project_st_sl_nav_leixing\"></div></div><div class=\"project_st_sl_ul\"><div class=\"project_st_sl_li\">");
            string[] regTyperray = new string[] { regkey + @"=DI(\d+\&?)" };
            //不限
            sb.AppendFormat("{0}", GetHrefLinkReset(queryString, regTyperray, TypeId, "不限"));
            var TypeRange = _iShopAttributesService.GetListByAttributeId(AttrTypeId);
            //列表
            foreach (var AttrValueItem in TypeRange)
            {
                if (AttrValueItem.Id == TypeId)
                {
                    sb.AppendFormat("<a class='on'>{0}</a>", AttrValueItem.ValueStr);
                    string strclear = GetHrefLinkClear(queryString, regTyperray, TypeId, AttrValueItem.ValueStr);
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

            sb.AppendFormat("<a href='{0}' class='h9'>{1}</a>", link, targetName);

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

    }

    //临时写的位置
    public class ProjectDetailsModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
        #endregion

}
