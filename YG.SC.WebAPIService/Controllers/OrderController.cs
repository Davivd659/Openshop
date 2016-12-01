
namespace YG.SC.WebAPIService.Controllers
{
    ﻿using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Web.Http;
    using YG.SC.DataAccess;
    using YG.SC.Service.IService;
    using YG.SC.WebAPIService.Filters;
    using YG.SC.WebAPIService.Models;

    /// <summary>
    /// 订单服务 下单
    /// 命名空间：YG.SC.WebAPIService.Controllers
    /// 类功能：
    /// </summary>
    /// 创建者：边亮
    /// 创建日期：2014-09-25 11:59
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class OrderController : WebApiBaseController
    {
        /// <summary>
        /// 字段MunicipalityRegex
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/24 14:55
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private static readonly Regex MunicipalityRegex = new Regex("^[北京|上海|天津|重庆]{2}");

        /// <summary>
        /// 确认收货订单状态改为 PAID
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/15 20:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private const string PaymentStatusCd = "PAID";

        /// <summary>
        /// 确认收货订单状态改为 SUCCESS
        /// </summary>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 18:55
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private const string OrderCd = "SUCCESS";

        /// <summary>
        /// InvoiceCd 发票类型
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 18:51
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly SortedDictionary<string, string> _invoiceCdDictionary;

        /// <summary>
        /// 字段_userOAuthService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 18:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly IUserOAuthService _userOAuthService;
        /// <summary>
        /// 字段_scmUserService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 18:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly IScmUserService _scmUserService;
        /// <summary>
        /// 字段_skuGoodsService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 18:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ISkuGoodsService _skuGoodsService;
        /// <summary>
        /// 字段_skuOrderService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 18:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ISkuOrderService _skuOrderService;
        /// <summary>
        /// 字段_sysRefCdService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 18:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ISysRefCdService _sysRefCdService;


        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="userOAuthService">The userOAuthService</param>
        /// <param name="scmUserService">The scmUserService</param>
        /// <param name="skuGoodsService">The skuGoodsService</param>
        /// <param name="skuOrderService">The skuOrderService</param>
        /// <param name="sysRefCdService">The sysRefCdService</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 18:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public OrderController(IUserOAuthService userOAuthService, IScmUserService scmUserService, ISkuGoodsService skuGoodsService, ISkuOrderService skuOrderService, ISysRefCdService sysRefCdService)
        {
            this._userOAuthService = userOAuthService;
            this._scmUserService = scmUserService;
            this._skuGoodsService = skuGoodsService;
            this._skuOrderService = skuOrderService;
            this._sysRefCdService = sysRefCdService;

            this._invoiceCdDictionary = this._skuOrderService.SysRefCdInvoiceTypeSortedDictionary();
        }

        /// <summary>
        /// 释放对象使用的非托管资源，并有选择性地释放托管资源。
        /// </summary>
        /// <param name="disposing">若为 true，则同时释放托管资源和非托管资源；若为 false，则仅释放非托管资源。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/26 13:53
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            this._scmUserService.Dispose();
            this._skuGoodsService.Dispose();
            this._skuOrderService.Dispose();
            this._userOAuthService.Dispose();
            this._sysRefCdService.Dispose();

            base.Dispose(disposing);
        }
        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="orderModel">The orderModel</param>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 15:36
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage Add([FromBody] OrderAddModel orderModel)
        {
            if (orderModel == null || string.IsNullOrEmpty(orderModel.UserToken) || orderModel.MaterialPart == null || orderModel.MaterialPart.Length == 0)
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusCode = (int)ApiStatusCode.Validate.InvalidRequiredParameter,
                        StatusMsg = "请求参数不能为空"
                    }.Transform()
                };

            var orderGoodsIds = orderModel.MaterialPart.Select(item => item.Id).ToArray();
            var user = this._userOAuthService.GetUser(orderModel.UserToken);
            if (user == null)
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = "未注册用户",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };
            }
            if (user.Recsts != "0")
            {
                //0审核成功 1审核中 2审核失败
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = user.Recsts == "1" ? "审核中" : "审核失败",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };
            }

            var userId = user.Id;
            var skuGoodsDic = this._skuGoodsService.GetGoods(item => orderGoodsIds.Contains(item.Id)).ToDictionary(item => item.Id, item => item);

            var skuOrder = new SctSkuOrder
            {
                UserId = userId,
                SourceCd = SourceCd,
                UserAddressId = orderModel.AddressId,
                InvoiceCd = orderModel.Invoice,
                InvoiceName = !string.IsNullOrEmpty(orderModel.Company) ? orderModel.Company : "",
                CustomerDesc = orderModel.Remark
            };
            var skuOrderDetails = (from p in orderModel.MaterialPart
                                   let goods = skuGoodsDic[p.Id]
                                   select new SctSkuOrderDetail
                                   {
                                       GoodsId = goods.Id,
                                       GoodsTotal = p.Number,
                                       GoodsPrice = goods.Price,
                                       GoodsTotalPrice = p.Number * goods.Price,
                                       GoodsName = goods.GoodsName,
                                       GoodsSourcePrice = goods.SourcePrice,
                                       GoodsSpecification = goods.Specification
                                   }).ToArray();

            var orderId = this._skuOrderService.InsertOrder(skuOrder, skuOrderDetails);

            var order = this._skuOrderService.GetById(orderId);
            var orderDetail = new OrderDetailModel
            {
                OrderNumber = order.OrderNo,
                OrderStatus = order.OrderCd,
                OrderStatusName = this._sysRefCdService.GetEntityByRefcd(order.OrderCd).SDesc,
                CreateTime = order.InsDt,
                ReceiveName = order.CustomerName,
                ReceivePhone = order.CustomerPhone,
                ReceiveAddress = order.DeliveryAddress,
                Remark = order.CustomerDesc,
                TotalPrice = order.OrderAmount,
                DeliveryPrice = 0,
                MaterialPrice = order.OrderAmount - 0,
                MaterialArray = (from o in order.SctSkuOrderDetail
                                 select new MaterialPart
                                 {
                                     Id = o.Id,
                                     Name = o.GoodsName,
                                     Number = o.GoodsTotal,
                                     Price = o.GoodsPrice,
                                     Specification = o.GoodsSpecification
                                 }).ToArray(),
                Invoice = _invoiceCdDictionary[order.InvoiceCd],
                Company = order.InvoiceName
            };

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<OrderDetailModel>
                {
                    Result = orderDetail
                }.Transform()
            };
        }
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="userToken">The UserToken</param>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 16:12
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
        public HttpResponseMessage List([FromUri] string userToken)
        {
            if (string.IsNullOrEmpty(userToken))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusCode = (int)ApiStatusCode.Validate.InvalidRequiredParameter,
                        StatusMsg = "请求参数不能为空"
                    }.Transform()
                };
            }
            else
            {
                //var userId = this._userOAuthService.GetUserId(SourceCd, userToken);
                var array = this._skuOrderService.GetOrderListByToken(userToken).OrderByDescending(item => item.InsDt);
                var listOder = (from order in array
                                select new OrderPartModel()
                                {
                                    OrderNumber = order.OrderNo,
                                    OrderStatus = order.OrderCd,
                                    OrderStatusName = order.OrderStatusName,
                                    CreateTime = order.InsDt,
                                    Shop=order.Company??""
                                }
                    ).ToArray();
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<OrderPartModel[]>
                    {
                        Result = listOder
                    }.Transform()
                };
            }
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="userToken">The userToken</param>
        /// <param name="orderNumber">The orderNumber</param>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 17:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
        public HttpResponseMessage Detail([FromUri] string userToken, [FromUri] string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusCode = (int)ApiStatusCode.Validate.InvalidRequiredParameter,
                        StatusMsg = "请求参数不能为空"
                    }.Transform()
                };
            }

            var order = this._skuOrderService.Find(item => item.OrderNo == orderNumber);
           var scmUser= this._scmUserService.GetEntityById(order.UserId);
            var orderDetail = new OrderDetailModel
            {
                OrderNumber = order.OrderNo,
                OrderStatus = order.OrderCd,
                OrderStatusName = this._sysRefCdService.GetEntityByRefcd(order.OrderCd).SDesc,
                CreateTime = order.InsDt,
                ReceiveName = order.CustomerName,
                ReceivePhone = order.CustomerPhone,
                ReceiveAddress = MunicipalityRegex.Replace(order.DeliveryAddress, "", 1),
                Remark = order.CustomerDesc,
                TotalPrice = order.OrderAmount,
                DeliveryPrice = 0,
                MaterialPrice = order.OrderAmount - 0,
                MaterialArray = (from o in order.SctSkuOrderDetail
                                 select new MaterialPart
                                 {
                                     Id = o.Id,
                                     Name = o.GoodsName,
                                     Number = o.GoodsTotal,
                                     Price = o.GoodsPrice,
                                     Specification = o.GoodsSpecification
                                 }).ToArray(),
                Invoice = _invoiceCdDictionary[order.InvoiceCd],
                Company = order.InvoiceName,
                Shop=scmUser.Company??""
            };

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<OrderDetailModel>
                {
                    Result = orderDetail
                }.Transform()
            };
        }


        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="affirmModel">The affirmModel</param>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 18:54
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage Affirm([FromBody] AffirmModel affirmModel)
        {
            if (affirmModel == null || string.IsNullOrEmpty(affirmModel.UserToken) || string.IsNullOrEmpty(affirmModel.OrderNumber))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusCode = (int)ApiStatusCode.Validate.InvalidRequiredParameter,
                        StatusMsg = "请求参数不能为空"
                    }.Transform()
                };
            }

            var order = this._skuOrderService.Find(item => item.OrderNo == affirmModel.OrderNumber);
            if (order == null)
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest,
                        StatusMsg = "请求参数错误"
                    }.Transform()
                };
            }

            if (order.OrderCd == OrderCd)
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Success.ToString(),
                        StatusMsg = "订单已完成，无法重复修改"
                    }.Transform()
                };
            }

            order.OrderCd = OrderCd;
            order.PaymentStatusCd = PaymentStatusCd;
            this._skuOrderService.UpdateSkuOrder(order);
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<string>
                {
                    Result = ApiStatusCode.SystemResult.Success.ToString(),
                }.Transform()
            };

        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="orderNumber">The orderNumber</param>
        /// <param name="pg">The pg</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014/11/24 13:40
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
        public HttpResponseMessage ListOderDetail([FromUri] string orderNumber="", [FromUri] int pg = 1)
        {
            SctSkuOrder order = new SctSkuOrder();
            SctSkuOrder[] orders;
            if (string.IsNullOrEmpty(orderNumber))
            {
           orders=  this._skuOrderService.GetPagination(order, pg).Item1;
            }
            else
            {
                order.OrderNo = orderNumber;
                orders = this._skuOrderService.GetPagination(order, pg).Item1;
            }
            var list = new List<OrderDetailModel>();
            var userids = orders.Select(item => item.UserId).ToArray();
            var scmUsers = this._scmUserService.GetEntityByIds(userids);
            foreach (var item in orders)
            {
                var orderDetail = new OrderDetailModel
                {
                    OrderNumber = item.OrderNo,
                    OrderStatus = item.OrderCd,
                    OrderStatusName = this._sysRefCdService.GetEntityByRefcd(item.OrderCd).SDesc,
                    CreateTime = item.InsDt,
                    ReceiveName = item.CustomerName,
                    ReceivePhone = item.CustomerPhone,
                    ReceiveAddress = MunicipalityRegex.Replace(item.DeliveryAddress, "", 1),
                    Remark = item.CustomerDesc,
                    TotalPrice = item.OrderAmount,
                    DeliveryPrice = 0,
                    MaterialPrice = item.OrderAmount - 0,
                    MaterialArray = (from o in item.SctSkuOrderDetail
                                     select new MaterialPart
                                     {
                                         Id = o.Id,
                                         Name = o.GoodsName,
                                         Number = o.GoodsTotal,
                                         Price = o.GoodsPrice,
                                         Specification = o.GoodsSpecification
                                     }).ToArray(),
                    Invoice = _invoiceCdDictionary[item.InvoiceCd],
                    Company = item.InvoiceName,
                    FAQDesc=item.FAQDesc??"",
                    Shop=scmUsers.First(i=>i.Id==item.UserId).Company??""
                };
                list.Add(orderDetail);
            }
           

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<List<OrderDetailModel>>
                {
                    Result = list
                }.Transform()
            };
        }
    }
}

