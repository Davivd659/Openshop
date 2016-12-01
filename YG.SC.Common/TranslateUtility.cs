
namespace YG.SC.Common
{

    /// <summary>
    /// 类名称：TranslateUtility
    /// 命名空间：YG.SC.Common
    /// 类功能：实体类 类型转换
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TArget">The type of the arget.</typeparam>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/11 14:17
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class TranslateUtility<TSource, TArget>
        where TSource : class,new()
        where TArget : class,new()
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <returns>
        /// The `1
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 14:18
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static TArget Translate(TSource tSource)
        {
            var tArget = new TArget();
            if (tSource == null) return tArget;
            var sourceType = tSource.GetType();

            var targetType = tArget.GetType();
            var fileInfos = targetType.GetProperties();
            foreach (var item in fileInfos)
            {
                var val = sourceType.GetProperty(item.Name).GetValue(tSource);
                item.SetValue(tArget, val);
            }
            return tArget;
        }
    }
}
