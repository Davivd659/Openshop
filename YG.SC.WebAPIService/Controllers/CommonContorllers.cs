
namespace YG.SC.WebAPIService.Controllers
{
    using System.Configuration;
    using System;

    /// <summary>
    /// 类名称：CommonContorllers
    /// 命名空间：YG.SC.WebAPIService.Controllers
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/25 10:17
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class CommonContorllers
    {
        //=============================== webui
        /// <summary>
        /// banner图
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 18:44
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string BannerImagePath = "/FileUpload/BannerImage/";

        /// <summary>
        /// 一级分类图标
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/17 10:39
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string FileUploadCategoryImgPath = "/FileUpload/CategoryIcon/";

        /// <summary>
        /// 商品图片路径
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/20 16:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string FileUploadGoodsImgPath = "/FileUpload/GoodsImage/";

        /// <summary>
        /// 商品图片路径
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/20 16:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string FileUploadGoodsImgSmallPath = "/FileUpload/GoodsImageSmall/";

        /// <summary>
        /// 引用WEB UI站点url
        /// </summary>
        /// <value>
        /// The web UI host.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 14:25
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string WebUiHost
        {
            get { return ConfigurationManager.AppSettings["WebUIHost"]; }
        }
        //=============================== webui

        /// <summary>
        ///当前网站域名Url
        /// </summary>
        /// <value>
        /// The web API host.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 14:23
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string WebApiHost
        {
            get { return ConfigurationManager.AppSettings["WebApiHost"]; }
        }

        /// <summary>
        /// 是否记录请求信息
        /// </summary>
        /// <value>
        /// The CanLog
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/26 17:02
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static bool CanLogRequestInfo
        {
            get { return string.Equals("true", ConfigurationManager.AppSettings["CanLogRequestInfo"], StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// 发送用户验证码短信（注册时使用）
        /// </summary>
        /// <value>
        /// The SMS send user verification code.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 10:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SmsSendUserVerificationCode
        {
            get
            {
                return ConfigurationManager.AppSettings["SmsSendUserVerificationCode"];
            }
        }

        /// <summary>
        /// 数据默认缓存时间
        /// </summary>
        /// <value>
        /// The data cache default minutes.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/8 11:40
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static int DataCacheDefaultMinutes
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["DataCacheDefaultMinutes"]); }
        }

    }

}