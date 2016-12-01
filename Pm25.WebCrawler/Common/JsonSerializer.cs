using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pm25.WebCrawler.Common
{
    public class JsonSerializer
    {
        /// <summary>
        /// obj 转string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 解析 json 字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object Deserialize(string input, Type targetType)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(input, targetType);
            return obj;
        }
    }
}