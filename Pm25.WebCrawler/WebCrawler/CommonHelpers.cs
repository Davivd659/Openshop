using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pm25.WebCrawler
{
    public class CommonHelpers
    {
        public static CommonHelpers Instance = new CommonHelpers();

        /// <summary>
        /// 转int 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int? ParseToInt(string s)
        {
            int result = 0;
            int.TryParse(s, out result);
            if (result > 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public decimal? ParseToDecimal(string s)
        {
            decimal result = 0;
            decimal.TryParse(s, out result);
            if (result > 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 污染级别
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        public string GetLevel(string quality)
        {
            // 优、良、轻度污染、中度污染、重度污染、严重污染
            string level = "";
            switch (quality)
            {
                case "优":
                    level = "一级";
                    break;
                case "良":
                    level = "二级";
                    break;
                case "轻度污染":
                    level = "三级";
                    break;
                case "中度污染":
                    level = "四级";
                    break;
                case "重度污染":
                    level = "五级";
                    break;
                case "严重污染":
                    level = "六级";
                    break;
                default:
                    return "";
            }
            return level;
        }

    }
}