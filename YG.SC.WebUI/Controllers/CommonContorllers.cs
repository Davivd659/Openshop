
using System.Configuration;

namespace YG.SC.WebUI.Controllers
{
    /// <summary>
    /// 类名称：CommonContorllers
    /// 命名空间：YG.SC.WebUI.Controllers
    /// 类功能：用于Controller层的配置
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/11 13:34
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class CommonContorllers
    {

        /// <summary>
        /// 用户权限ids 缓存key
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/22 11:26
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UserActionIdsCacheName = "UserActionIds";

        /// <summary>
        /// 用户角色缓存key
        /// </summary>
        /// 创建者：边亮
        /// 创建日期：2014/9/22 16:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UserRoleIdsCacheName = "UserRoleIds";



        #region//项目图
        /// <summary>
        /// 项目图
        /// </summary>
        public static string FileUploadProjectPhotoPath = "/FileUpload/ProjectPhoto/";
        /// <summary>
        /// 项目缩略图
        /// </summary>
        public static string FileUploadProjectPhotoSmallPath = "/FileUpload/ProjectPhotoSmall/";
        /// <summary>
        /// 项目封面图
        /// </summary>
        public static string FileUploadProjectCoverPhotoPath = "/FileUpload/ProjectCoverPhoto/";
        public static string FileUploadProjectCoverPhotoSmallPath = "/FileUpload/ProjectCoverPhotoSmall/";
        #endregion
        #region //品牌图片
        /// <summary>
        /// 品牌logo
        /// </summary>
        public static string FileUploadLogoImgPath = "/FileUpload/BrandLogoImage/";
        /// <summary>
        /// 品牌二维码
        /// </summary>
        public static string FileUploadQRCodeImgPath = "/FileUpload/BrandQRCodeImage/";


        public static string FileUploadAdPicSmallImgPath = "/FileUpload/AdPicSmallImage/";
        public static string FileUploadAdPicImgPath = "/FileUpload/AdPicImage/";
        /// <summary>
        /// 品牌图
        /// </summary>
        public static string FileUploadBrandImgPath = "/FileUpload/BrandImage/";
        public static string FileUploadBrandSmallImgPath = "/FileUpload/BrandSmallImage/";
        public static string FileUploadBrandSquareImgPath = "/FileUpload/BrandSquareImage/";
        public static string FileUploadBrandRectangleImgPath = "/FileUpload/BrandRectangleImage/";
        /// <summary>
        /// 任务合同图片
        /// </summary>
        public static string FileUploadMissionContractImgPath = "/FileUpload/MissionContractImage/";
        #endregion
        #region //开店帮图片
        /// <summary>
        /// 品牌logo
        /// </summary>
        public static string FileUploadOpenShopLogoImgPath = "/FileUpload/OpenShopLogoImage/";


        /// <summary>
        /// 品牌图
        /// </summary>
        public static string FileUploadOpenShopImgPath = "/FileUpload/OpenShopImage/";
        public static string FileUploadOpenShopSmallImgPath = "/FileUpload/OpenShopSmallImage/";
        public static string FileUploadOpenShopSquareImgPath = "/FileUpload/OpenShopSquareImage/";
        public static string FileUploadOpenShopRectangleImgPath = "/FileUpload/OpenShopRectangleImage/";
        #endregion
        #region 商品信息
        public static string FileUploadProductImgPath = "/FileUpload/ProductImgPath/";
        #endregion
        /// <summary>
        /// 用户菜单
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 16:49
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UserSecurityPath = "/config/security/";

        /// <summary>
        /// 用户菜单缓存名称
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/18 17:40
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UserMenuCacheName = "UserMenuCache";

        /// <summary>
        /// 用户ID 缓存名称
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/19 14:42
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UserIdCacheName = "UserIdCache";

        /// <summary>
        /// 用户名 缓存名称
        /// </summary>
        /// 创建者：边亮
        /// 创建日期：2014/9/22 13:52
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UserNameCacheName = "UserNameCahe";

        /// <summary>
        /// 用户类型 缓存名称
        /// </summary>
        /// 创建者：边亮
        /// 创建日期：2014/9/22 13:56
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UserCdCacheName = "UserCdCahe";

        /// <summary>
        /// 编辑用户权限时缓存用户权限的 key
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/19 14:39
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string EditUserSecurityActionCacheName = "EditUserSecurityActionCache";
        /// <summary>
        /// 订单接受发送短信内容
        /// </summary>
        /// <value>
        /// The SMS send user verification code.
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/30 15:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string OrderReceiveSmsSend
        {
            get
            {
                return ConfigurationManager.AppSettings["OrderReceiveSmsSend"];
            }
        }
        /// <summary>
        /// 订单失败发送短信息
        /// </summary>
        /// <value>
        /// The order fail SMS send.
        /// </value>
        /// 创建者：边亮
        /// 创建日期：2014/10/30 15:33
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string OrderFailSmsSend
        {
            get
            {
                return ConfigurationManager.AppSettings["OrderFailSmsSend"];
            }
        }
    }
}