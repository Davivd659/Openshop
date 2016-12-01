
namespace YG.SC.Common.Cache
{
    using Memcached.Client;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 类名称：Memcache
    /// 命名空间：YG.SC.Common.Cache
    /// 类功能：Memcache缓存
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/12 16:36
    /// 修改者：
    /// 修改时间：
    // ----------------------------------------------------------------------------------------
    public class Memcache : ICache
    { /// <summary>
        /// 字段KeyPrefix
        /// </summary>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 14:53
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly string _keyPrefix;


        /// <summary>
        /// 字段MemcachedClient
        /// </summary>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 10:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly MemcachedClient _memcachedClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Memcache" /> class.
        /// </summary>
        /// <param name="keyPrefix">The keyPrefix</param>
        /// <param name="serverList">The serverList</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 10:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public Memcache(string keyPrefix, string[] serverList)
        {
            _keyPrefix = keyPrefix;

            var pool = SockIOPool.GetInstance();
            pool.SetServers(serverList);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 50;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();

            _memcachedClient = new MemcachedClient();
        }

        /// <summary>
        /// 添加缓存，默认存储30分钟
        /// 注意：如果缓存KEY已经存在，不会再次存储
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 11:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Add(string key, object value)
        {
            return Add(key, value, DateTime.Now.AddMinutes(ExpiredMinutes));
        }

        /// <summary>
        /// 添加缓存
        /// 注意：如果缓存KEY已经存在，不会再次存储
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <param name="expiredTime">The expiredTime</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 11:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Add(string key, object value, DateTime expiredTime)
        {
            try
            {
                return _memcachedClient.Add(GetKey(key), value, expiredTime);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 替换缓存，默认30分钟
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 11:19
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Replace(string key, object value)
        {
            return Replace(key, value, DateTime.Now.AddMinutes(ExpiredMinutes));
        }

        /// <summary>
        /// 替换缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <param name="expiredTime">The expiredTime</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 11:20
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Replace(string key, object value, DateTime expiredTime)
        {
            return _memcachedClient.Replace(GetKey(key), value, expiredTime);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 11:23
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Delete(string key)
        {
            return _memcachedClient.Delete(GetKey(key));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key</param>
        /// <returns>
        /// The ``0
        /// </returns>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 11:24
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public object Get(string key)
        {
            return _memcachedClient.Get(this.GetKey(key));
        }

        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 11:25
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Exists(string key)
        {
            return _memcachedClient.KeyExists(this.GetKey(key));
        }

        /// <summary>
        /// memcache key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// String
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 10:24
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string GetKey(string key)
        {
            return _keyPrefix + "Memcache_" + key;
        }

        /// <summary>
        /// 过期时间分钟
        /// </summary>
        /// <value>
        /// The expired minutes.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 11:17
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public int ExpiredMinutes { get { return 30; } }

        /// <summary>
        /// 获取所有缓存中的KEY
        /// </summary>
        /// <value>
        /// All keys.
        /// </value>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/8 17:51
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public IEnumerable<string> AllKeys
        {
            get
            {
                var list = new List<string>();
                var ht= _memcachedClient.Stats();
                var items = ht["curr_items"];

              //  _memcachedClient.

                return list;
            }
        }



    }
}
