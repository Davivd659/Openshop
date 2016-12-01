
namespace YG.SC.Common
{
    using System.Configuration;
    using YG.SC.Common.Cache;

    public class CacheUtility
    {
        /// <summary>
        /// The method will 
        /// </summary>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 11:34
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static void Register()
        {
            var prefix = ConfigurationManager.AppSettings["CachePrefix"];

            if (MemcacheInstance == null) MemcacheInstance = new Memcache(prefix, ConfigurationManager.AppSettings["Memcache.ServerList"].Split(','));
           // if (RedisInstance == null) RedisInstance = new Redis(prefix, ConfigurationManager.AppSettings["Redis.Server"]);
        }

        /// <summary>
        /// Gets the get memcache instance.
        /// </summary>
        /// <value>
        /// The get memcache instance.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 11:36
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static Memcache MemcacheInstance { get; private set; }

        /// <summary>
        /// Gets the redis instance.
        /// </summary>
        /// <value>
        /// The redis instance.
        /// </value>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 15:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static Redis RedisInstance { get; private set; }
    }
}
