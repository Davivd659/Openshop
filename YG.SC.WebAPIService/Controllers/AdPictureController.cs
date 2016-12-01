
namespace YG.SC.WebAPIService.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using YG.SC.Service.IService;
    using YG.SC.WebAPIService.Filters;
    using YG.SC.WebAPIService.Models;
    using YG.SC.DataAccess;

    /// <summary>
    /// 类名称：AdPictureController
    /// 命名空间：YG.SC.WebAPIService.Controllers
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/30 10:30
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class AdPictureController : WebApiBaseController
    {
        /// <summary>
        /// 字段_sctAdPictureService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/30 14:39
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ISctAdPictureService _sctAdPictureService;

        /// <summary>
        /// 字段_skuGoodsService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/18 1:52
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
        /// Initializes a new instance of the <see cref="AdPictureController" /> class.
        /// </summary>
        /// <param name="sctAdPictureService">The sctAdPictureService</param>
        /// <param name="skuGoodsService">The skuGoodsService</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/30 14:39
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public AdPictureController(ISctAdPictureService sctAdPictureService, ISkuGoodsService skuGoodsService)
        {
            this._sctAdPictureService = sctAdPictureService;
            this._skuGoodsService = skuGoodsService;

            this._sysyRefCdUnitCDictionary = this._skuGoodsService.SysRefCdUnitCdSortedDictionary();
            this._sysRefCdStorageSortedDictionary = this._skuGoodsService.SysRefCdStorageSortedDictionary();
        }

        /// <summary>
        /// 释放对象使用的非托管资源，并有选择性地释放托管资源。
        /// </summary>
        /// <param name="disposing">若为 true，则同时释放托管资源和非托管资源；若为 false，则仅释放非托管资源。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/30 14:40
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            this._sctAdPictureService.Dispose();
            this._skuGoodsService.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// 首页Banner
        /// </summary>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/30 15:07
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
        // [YgScCacheOutputAttribute(ClientTimeSpan = 1800, ServerTimeSpan = 1800)]
        public HttpResponseMessage Banner()
        {
            var array = this._sctAdPictureService.GetAll();
            var goodsIds = array.Select(item => item.GoodsId).Distinct().ToArray();
            var goodsDic = this._skuGoodsService.GetGoodsFromView(item => goodsIds.Contains(item.Id)).ToDictionary(item => item.Id, item => item);

            var result = (from p in array
                          let goods = p.GoodsId == -1 ? new Scp_GetAllGoods_View()
                          {
                              Id = -1,
                              GoodsName="",
                              Price = 0,
                              UnitCd="",
                              ClassifyName = "",
                              ImageName="",
                              ClassifyId =0,
                              ClassifySecondId =0,
                              ClassifySecondName ="",
                              BrandName="",
                              Producer ="",
                              StorageCd="",
                              Specification =""
                          } : goodsDic[p.GoodsId]
                          select new AdPictureModel
                          {
                              Id = p.Id,
                              ImageUrl = CommonContorllers.WebUiHost + CommonContorllers.BannerImagePath + p.ImageName,
                              ImageLink = p.Url,
                              GoodsMode = new MaterialModel
                              {
                                  Id = goods.Id,
                                  Name = goods.GoodsName,
                                  Price = goods.Price,
                                  Unit =string.IsNullOrEmpty(goods.UnitCd)?"": _sysyRefCdUnitCDictionary[goods.UnitCd],
                                  Image = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgPath + goods.ImageName,
                                  ImageSmall = CommonContorllers.WebUiHost + CommonContorllers.FileUploadGoodsImgSmallPath + goods.ImageName,
                                  ClassifyName = goods.ClassifyName,
                                  ClassifyId = goods.ClassifyId,
                                  ClassifySecondId = goods.ClassifySecondId,
                                  ClassifySecondName = goods.ClassifySecondName,
                                  Brand = goods.BrandName,
                                  Producer = string.IsNullOrEmpty(goods.Producer) ? "" : goods.Producer,
                                  Storage = string.IsNullOrEmpty(goods.StorageCd) ? "" : _sysRefCdStorageSortedDictionary[goods.StorageCd],
                                  Specification = goods.Specification
                              }
                          }).ToArray();

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<AdPictureModel[]>
                {
                    Result = result
                }.Transform()
            };
        }

    }
}