
namespace YG.SC.Common.Cache
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 接口名称：ICache
    /// 命名空间：YG.SC.Common.Cache
    /// 接口功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/12 16:36
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public interface ICache
    {

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
        IEnumerable<string> AllKeys { get; }

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
        bool Add(string key, object value);

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
        bool Add(string key, object value, DateTime expiredTime);

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
        bool Replace(string key, object value);

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
        /// 创建日期：2014/8/15 9:42
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        bool Replace(string key, object value, DateTime expiredTime);

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
        bool Delete(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// The ``0
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/8/15 9:43
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        object Get(string key);

        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙 
        /// 创建日期：2014/8/15 9:44
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        bool Exists(string key);

        /// <summary>
        /// Gets the cache.
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
        string GetKey(string key);

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
        int ExpiredMinutes { get; }
    }
}
