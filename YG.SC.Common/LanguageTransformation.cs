
namespace YG.SC.Common
{
    using Microsoft.International.Converters.PinYinConverter;
    using System;
    /// <summary>
    /// 类名称：语言转换
    /// 命名空间：YG.SC.Common
    /// 类功能：
    /// </summary>
    /// 创建者：边亮
    /// 创建日期：2014/10/13 13:36
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
   public  class LanguageTransformation
    {
       /// <summary>
       /// 汉字转拼音
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static string GetPinYin(string str)
       {
           string r = string.Empty;
           foreach (var item in str)
           {
               try
               {
                   ChineseChar chineseChar = new ChineseChar(item);
                   string t = chineseChar.Pinyins[0];
                   r += t.Substring(0, t.Length - 1);
               }
               catch (Exception)
               {
                    r += item.ToString();
               }
           }
           return r;
       }
    }
}
