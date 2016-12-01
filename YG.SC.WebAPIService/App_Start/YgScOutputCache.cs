
namespace YG.SC.WebAPIService
{
    using System;
    using System.Collections.Generic;
    using WebApi.OutputCache.Core.Cache;
    using YG.SC.Common;
    using YG.SC.Common.Cache;

    /// <summary>
    /// 类名称：YgScOutputCache
    /// 命名空间：YG.SC.WebAPIService
    /// 类功能：https://github.com/filipw/AspNetWebApi-OutputCache redis缓存服务
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/10/8 17:47
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class YgScOutputCache : IApiOutputCache
    {
        private static readonly Memcache RedisCache = CacheUtility.MemcacheInstance;

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="o">The o</param>
        /// <param name="expiration">The expiration</param>
        /// <param name="dependsOnKey">The dependsOnKey</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/8 17:47
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
        {
            RedisCache.Add(key, o, expiration.DateTime);
        }

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <value>
        /// All keys.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 14:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public IEnumerable<string> AllKeys
        {
            get
            {
                var allkeys = RedisCache.AllKeys;
                return allkeys;
            }
        }

        /// <summary>
        /// Description:
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns></returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 14:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Contains(string key)
        {
            var flag = RedisCache.Exists(key);
            return flag;
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// The Object
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 14:32
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public object Get(string key)
        {
            var obj = RedisCache.Get(key);
            return obj;
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key</param>
        /// <returns>
        /// The ``0
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 14:33
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public T Get<T>(string key) where T : class
        {
            var t = RedisCache.Get(key);
            return t as T;
        }

        /// <summary>
        /// The method will 
        /// </summary>
        /// <param name="key">The key</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 14:33
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void Remove(string key)
        {
            RedisCache.Delete(key);
        }

        /// <summary>
        /// Removes the starts with.
        /// </summary>
        /// <param name="key">The key</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 14:33
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public void RemoveStartsWith(string key)
        {
            RedisCache.Delete(key);
        }
    }
}