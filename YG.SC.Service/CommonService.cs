
namespace YG.SC.Service
{
    using System;
    using System.Configuration;

    /// <summary>
    /// 类名称：CommonService
    /// 命名空间：YG.SC.Service
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/10/10 10:24
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class CommonService
    {
        /// <summary>
        /// 应用程序集缓存 缓存时间分钟
        /// </summary>
        /// <value>
        /// The system data cache default minutes.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/10 10:41
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static int SystemDataCacheDefaultMinutes { get { return Int32.Parse(ConfigurationManager.AppSettings["SystemDataCacheDefaultMinutes"]); } }

        /// <summary>
        /// 来源 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cd source cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 19:31
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdSourceCacheName { get { return "SysRefCdSourceCacheName"; } }

        /// <summary>
        /// 支付方式 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cd payment group CDS cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 19:41
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdPaymentCacheName { get { return "SysRefCdPaymentCacheName"; } }

        /// <summary>
        /// 支付状态 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cd payment status group cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 19:44
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdPaymentStatusCacheName { get { return "SysRefCdPaymentStatusCacheName"; } }

        /// <summary>
        /// 订单状态组 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cd order status group cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 19:56
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdOrderStatusCacheName { get { return "SysRefCdOrderStatusCacheName"; } }

        /// <summary>
        /// 发票类型组 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cd invoice type group cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 19:58
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdInvoiceTypeCacheName { get { return "SysRefCdInvoiceTypeCacheName"; } }

        /// <summary>
        /// 订单短信发送状态组 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cdorder state for send SMS CDS cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 20:01
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdorderStatusForSendSmsCdsCacheName { get { return "SysRefCdorderStatusForSendSmsCdsCacheName"; } }

        /// <summary>
        /// 商品单位分组 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cd commodity unit group cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 20:03
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdCommodityUnitCacheName { get { return "SysRefCdCommodityUnitCacheName"; } }

        /// <summary>
        /// 用户类型组 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cd user type group cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 20:05
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdUserTypeCacheName { get { return "SysRefCdUserTypeCacheName"; } }

        /// <summary>
        /// 储存方式分组 SortedDictionary 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system reference cdstorage group cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 20:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysRefCdstorageCacheName { get { return "SysRefCdstorageCacheName"; } }

        /// <summary>
        /// 省份缓存 ChinaProvince[] 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system china province cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/10 10:35
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysChinaProvinceCacheName { get { return "SysChinaProvinceCacheName"; } }

        /// <summary>
        /// 城市缓存 ChianCity[] 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system china city cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/10 11:04
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysChinaCityCacheName { get { return "SysChinaCityCacheName"; } }

        /// <summary>
        /// 区域缓存 ChianCounty[] 应用程序级
        /// </summary>
        /// <value>
        /// The name of the system china county cache.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/10 11:07
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string SysChinaCountyCacheName { get { return "SysChinaCountyCacheName"; } }
    }
}
