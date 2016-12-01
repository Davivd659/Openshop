
namespace YG.SC.WebAPIService.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using YG.SC.Service.IService;
    using YG.SC.WebAPIService.Filters;
    using YG.SC.WebAPIService.Models;

    /// <summary>
    /// 商品服务
    /// 命名空间：YG.SC.WebAPIService.Controllers
    /// 类功能：
    /// </summary>
    /// 创建者：边亮
    /// 创建日期：2014-09-24 19:15
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class GoodsController : WebApiBaseController
    {
        /// <summary>
        /// 是否推荐0 推荐 1 不推荐
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 16:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private const string IsElite = "0";

        /// <summary>
        /// 字段_skuGoodsService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 11:14
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ISkuGoodsService _skuGoodsService;

        /// <summary>
        /// 字段_sysyRefCdUnitCDictionary
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/15 15:35
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly SortedDictionary<string, string> _sysyRefCdUnitCDictionary;

        /// <summary>
        /// 字段_sysRefCdStorageSortedDictionary
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/16 16:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly SortedDictionary<string, string> _sysRefCdStorageSortedDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoodsController" /> class.
        /// </summary>
        /// <param name="skuGoodsService">The skuGoodsService</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 11:14
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public GoodsController(ISkuGoodsService skuGoodsService)
        {
            this._skuGoodsService = skuGoodsService;

            this._sysyRefCdUnitCDictionary = this._skuGoodsService.SysRefCdUnitCdSortedDictionary();
            this._sysRefCdStorageSortedDictionary = this._skuGoodsService.SysRefCdStorageSortedDictionary();
        }

        /// <summary>
        /// 释放对象使用的非托管资源，并有选择性地释放托管资源。
        /// </summary>
        /// <param name="disposing">若为 true，则同时释放托管资源和非托管资源；若为 false，则仅释放非托管资源。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 11:14
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            this._skuGoodsService.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 获取最新的六个商品
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014-09-24 20:16
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
        //[YgScCacheOutput(ClientTimeSpan = 1800, ServerTimeSpan = 1800)]
        public HttpResponseMessage GetNew([FromUri] int number = 6)
        {
            if (number != -1 && number <= 0)
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = string.Empty,
                        StatusMsg = "请求参数不能小于0",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };
            }

            var array = this._skuGoodsService.GetGoodsFromView(item => item.Recsts == "0").OrderByDescending(item => item.InsDt).ThenBy(item => item.GoodsPinYin).ToArray();
            if (number != -1)
            {
                array = array.Take(number).ToArray();
            }


            var userAddressArray = (from p in array
                                    select new MaterialModel
                                    {
                                        Id = p.Id,
                                        Name = p.GoodsName,
                                        Price = p.SourcePrice??p.Price,
                                        Unit = _sysyRefCdUnitCDictionary[p.UnitCd],
                                        Image = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgPath + p.ImageName,
                                        ImageSmall = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgSmallPath + p.ImageName,
                                        ClassifyName = p.ClassifyName,
                                        ClassifyId = p.ClassifyId,
                                        ClassifySecondId = p.ClassifySecondId,
                                        ClassifySecondName = p.ClassifySecondName,
                                        Brand = p.BrandName,
                                        Producer = p.Producer,
                                        Storage = _sysRefCdStorageSortedDictionary[p.StorageCd],
                                        Specification = p.Specification
                                    }).ToArray();
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<MaterialModel[]>
                {
                    Result = userAddressArray
                }.Transform()
            };
        }

        /// <summary>
        /// 店内推荐
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 11:21
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
        //[YgScCacheOutput(ClientTimeSpan = 1800, ServerTimeSpan = 1800)]
        public HttpResponseMessage GetElite([FromUri] int number = 9)
        {
            if (number != -1 && number <= 0)
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = string.Empty,
                        StatusMsg = "请求参数不能小于0",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };
            }

            var array = this._skuGoodsService.GetGoodsFromView(item => item.IsElite == IsElite && item.Recsts == "0").OrderByDescending(item => item.InsDt).ThenBy(item => item.GoodsPinYin).ToArray();
            if (number != -1)
            {
                array = array.Take(number).ToArray();
            }

            var userAddressArray = (from p in array
                                    orderby p.InsDt descending
                                    select new MaterialModel
                                    {
                                        Id = p.Id,
                                        Name = p.GoodsName,
                                        Price = p.SourcePrice ?? p.Price,
                                        Unit = _sysyRefCdUnitCDictionary[p.UnitCd],
                                        Image = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgPath + p.ImageName,
                                        ImageSmall = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgSmallPath + p.ImageName,
                                        ClassifyName = p.ClassifyName,
                                        ClassifyId = p.ClassifyId,
                                        ClassifySecondId = p.ClassifySecondId,
                                        ClassifySecondName = p.ClassifySecondName,
                                        Brand = p.BrandName,
                                        Producer = p.Producer,
                                        Storage = _sysRefCdStorageSortedDictionary[p.StorageCd],
                                        Specification = p.Specification
                                    }).ToArray();

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<MaterialModel[]>
                {
                    Result = userAddressArray,
                }.Transform()
            };
        }

        /// <summary>
        /// 按商品名称进行搜索
        /// </summary>
        /// <param name="materialSearchParameter">The MaterialSearchParameter</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014-09-25 11:49
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage SearchElite([FromBody] MaterialSearchParameter materialSearchParameter)
        {
            var array = this._skuGoodsService.SearchElite(SourceCd, materialSearchParameter.Keyword).OrderByDescending(item => item.InsDt);
            var userAddressArray = (from p in array
                                    select new MaterialModel
                                    {
                                        Id = p.Id,
                                        Name = p.GoodsName,
                                        Price = p.SourcePrice ?? p.Price,
                                        Unit = _sysyRefCdUnitCDictionary[p.UnitCd],
                                        Image = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgPath + p.ImageName,
                                        ImageSmall = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgSmallPath + p.ImageName,
                                        ClassifyName = p.ClassifyName,
                                        ClassifyId = p.ClassifyId,
                                        ClassifySecondId = p.ClassifySecondId,
                                        ClassifySecondName = p.ClassifySecondName,
                                        Brand = p.BrandName,
                                        Producer = p.Producer,
                                        Storage = _sysRefCdStorageSortedDictionary[p.StorageCd],
                                        Specification = p.Specification
                                    }).OrderBy(i => i.Name).ThenBy(i => i.Brand).ToArray();
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<MaterialModel[]>
                {
                    Result = userAddressArray
                }.Transform()
            };
        }


        /// <summary>
        /// 校验商品
        /// </summary>
        /// <param name="checkGoodsIdModel">The checkGoodsIdModel</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014/10/14 10:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage CheckGoods([FromBody] CheckGoodsIdModel checkGoodsIdModel)
        {
            if (checkGoodsIdModel == null || checkGoodsIdModel.GoodsIdList.Length <= 0)
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

            var goods = this._skuGoodsService.GetGoods(item => checkGoodsIdModel.GoodsIdList.Contains(item.Id));
            if (goods == null || goods.Length <= 0)
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
            var goodsModel = (from g in goods
                              select new CheckGoods
                              {
                                  GoodsId = g.Id,
                                  GoodsName = g.GoodsName,
                                  Price = g.SourcePrice??g.Price,
                                  Recsts = g.Recsts
                              }).ToArray();


            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<CheckGoods[]>
                {
                    Result = goodsModel
                }.Transform()
            };

        }
    }
}
