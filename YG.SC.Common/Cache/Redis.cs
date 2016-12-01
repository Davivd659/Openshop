

namespace YG.SC.Common.Cache
{
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// 类名称：Redis
    /// 命名空间：YG.SC.Common.Cache
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/12 16:38
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class Redis : ICache
    {

        /// <summary>
        /// 字段KeyPrefix
        /// </summary>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 14:53
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly string _keyPrefix;

        /// <summary>
        /// 字段redDb
        /// </summary>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 13:24
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ConnectionMultiplexer _connection;

        /// <summary>
        /// 字段_servers
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 10:29
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly string _servers;

        /// <summary>
        /// 字段Serializer
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/9 16:53
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly BinaryFormatterItemSerializer _serializer;
        /// <summary>
        /// Initializes a new instance of the <see cref="Redis" /> class.
        /// </summary>
        /// <param name="keyPrefix">The keyPrefix</param>
        /// <param name="servers">The servers</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 14:28
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public Redis(string keyPrefix, string servers)
        {
            _keyPrefix = keyPrefix;
            _servers = servers;
            _serializer = new BinaryFormatterItemSerializer();
            _connection = ConnectionMultiplexer.Connect(servers);
        }

        /// <summary>
        /// 添加缓存，默认存储30分钟
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 9:36
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Add(string key, object value)
        {
            return Add(key, value, DateTime.Now.AddMinutes(ExpiredMinutes));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <param name="expiredTime">The expiredTime</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 9:39
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Add(string key, object value, DateTime expiredTime)
        {
            var db = _connection.GetDatabase();
            var expiration = expiredTime - DateTime.UtcNow;
            var entryBytes = _serializer.Serialize(value);

            return db.StringSet(this.GetKey(key), entryBytes, expiration, When.NotExists);
        }

        /// <summary>
        /// 替换缓存，默认30分钟
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 9:41
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Replace(string key, object value)
        {
            return Add(this.GetKey(key), value);
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
        /// 创建日期：2014/8/15 14:36
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Replace(string key, object value, DateTime expiredTime)
        {
            return Add(this.GetKey(key), value, expiredTime);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 9:42
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Delete(string key)
        {
            var db = _connection.GetDatabase();
            return db.KeyDelete(GetKey(key));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// The ``0
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 14:39
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public object Get(string key)
        {
            var db = _connection.GetDatabase();
            var result = db.StringGet(GetKey(key));
            var value = _serializer.Deserialize(result);
            //   var r = (value as byte[]) != null ? System.Text.Encoding.UTF8.GetString((byte[]) value):value;
            return value;
        }

        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 14:40
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public bool Exists(string key)
        {
            var db = _connection.GetDatabase();
            return db.KeyExists(GetKey(key));
        }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// String
        /// </returns>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 14:40
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string GetKey(string key)
        {
            return _keyPrefix + "Redis_" + key;
        }

        /// <summary>
        /// 过期时间分钟
        /// </summary>
        /// <value>
        /// The expired minutes.
        /// </value>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 14:40
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
                var server = _connection.GetServer(_servers);
                return server.Keys().Select(item => item.ToString());
            }
        }

        public class BinaryFormatterItemSerializer
        {
            public byte[] Serialize(object item)
            {
                var formatter = new BinaryFormatter();

                byte[] itemBytes;

                using (var ms = new MemoryStream())
                {
                    formatter.Serialize(ms, item);
                    itemBytes = ms.GetBuffer();
                }

                return itemBytes;
            }

            public object Deserialize(byte[] bytes)
            {
                var formatter = new BinaryFormatter();

                object item;
                using (var ms = new MemoryStream(bytes))
                {
                    item = formatter.Deserialize(ms);
                }

                return item;
            }
        }
    }
}
