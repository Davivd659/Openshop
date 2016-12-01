
using System;
using System.Collections.Generic;

namespace YG.SC.Common
{
    /// <summary>
    /// 类名称：RandomUtility
    /// 命名空间：YG.SC.Common
    /// 类功能：随机数
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/24 19:52
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class RandomUtility
    {
        /// <summary>
        /// 生成制定长度随机数
        /// </summary>
        /// <param name="length">The length</param>
        /// <returns>
        /// The Int32
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:52
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public string Generate(int length = 6)
        {
            var result = new List<int>();
            var random = new Random();
            for (var i = 0; i < length; i++)
            {
                result.Add(random.Next(0, 10));
            }
            return string.Join("", result);
        }
    }
}
