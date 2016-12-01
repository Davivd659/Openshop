
namespace YG.SC.WebAPIService.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using YG.SC.Service;
    using YG.SC.Service.IService;
    using YG.SC.WebAPIService.Filters;
    using YG.SC.WebAPIService.Models;

    /// <summary>
    /// 分类服务
    /// 命名空间：YG.SC.WebAPIService.Controllers
    /// 类功能：
    /// </summary>
    /// 创建者：边亮
    /// 创建日期：2014-09-24 19:05
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class CategoryController : WebApiBaseController
    {
        /// <summary>
        /// 字段_skuCategoryFirstService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 14:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ISkuCategoryFirstService _skuCategoryFirstService;

        /// <summary>
        /// 字段_skuGoodsService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 14:33
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
        /// 创建日期：2014/10/17 11:24
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly SortedDictionary<string, string> _sysRefCdStorageSortedDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController" /> class.
        /// </summary>
        /// <param name="skuCategoryFirstService">The skuCategoryFirstService</param>
        /// <param name="skuGoodsService">The skuGoodsService</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 14:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public CategoryController(ISkuCategoryFirstService skuCategoryFirstService, ISkuGoodsService skuGoodsService)
        {
            this._skuCategoryFirstService = skuCategoryFirstService;
            this._skuGoodsService = skuGoodsService;

            this._sysyRefCdUnitCDictionary = this._skuGoodsService.SysRefCdUnitCdSortedDictionary();
            this._sysRefCdStorageSortedDictionary = this._skuGoodsService.SysRefCdStorageSortedDictionary();
        }


        /// <summary>
        /// 释放对象使用的非托管资源，并有选择性地释放托管资源。
        /// </summary>
        /// <param name="disposing">若为 true，则同时释放托管资源和非托管资源；若为 false，则仅释放非托管资源。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 14:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            this._skuCategoryFirstService.Dispose();
            this._skuGoodsService.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        ///分类信息，获取分类服务
        /// </summary>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014-09-24 20:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
       // [YgScCacheOutputAttribute(ClientTimeSpan = 1800, ServerTimeSpan = 1800)]
        public HttpResponseMessage GetClassifty()
        {
            var array = this._skuCategoryFirstService.GetClassifty();
            var classifty = from p in array
                            group p by p.FirstId
                                into g
                                let entity = g.First(item => item.FirstId == g.Key)
                                select new ClassifyModel
                                {
                                    Id = g.Key,
                                    Name = entity.FirstName,
                                    Picture = CommonContorllers.WebUiHost + CommonContorllers.FileUploadCategoryImgPath + entity.FirstImg,
                                    SubClassify =
                                    g.
                                       Select(item => new SubClassifyModel { Id = item.SecondId, Name = item.SecondCategoryName })
                                       .ToArray()
                                };

            var resultClassifty = classifty.ToArray();

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<ClassifyModel[]>
                {
                    Result = resultClassifty
                }.Transform()
            };

        }


        /// <summary>
        /// 根据二级分类获取商品
        /// </summary>
        /// <param name="firstId">The firstId</param>
        /// <param name="secondId">The secondId</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 14:38
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
       // [YgScCacheOutput(ClientTimeSpan = 1800, ServerTimeSpan = 1800)]
       // [CacheOutput(ClientTimeSpan = 1800, ServerTimeSpan = 1800)]
        public HttpResponseMessage GetGoodsBySecond([FromUri]int firstId = -1, [FromUri]int secondId = -1)
        {
            if (firstId == -1 && secondId == -1)
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = string.Empty,
                        StatusMsg = "请求参数错误",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };
            }

            var array = this._skuGoodsService.GetGoodsFromViewBySecond(firstId, secondId);
            var materials = (from p in array
                             select new MaterialModel
             {
                 Id = p.GoodsId,
                 Name = p.GoodsName,
                 Price = p.Price,
                 Unit = this._sysyRefCdUnitCDictionary[p.UnitCd],
                 Image = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgPath + p.ImageName,
                 ImageSmall = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgSmallPath + p.ImageName,
                 ClassifyName = p.FirstCategoryName,
                 ClassifyId = p.FirstId,
                 ClassifySecondId = p.SecondId,
                 ClassifySecondName = p.SecondCategoryName,
                 Brand = p.BrandName,
                 Specification = p.Specification,
                 Producer = p.Producer,
                 Storage = this._sysRefCdStorageSortedDictionary[p.StorageCd]
             }).OrderBy(item => item.Name).ToArray();

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<MaterialModel[]>
                {
                    Result = materials
                }.Transform()
            };
        }


    }
}
