using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class DateTimeHelpers
    {
        /// <summary>
        /// 获取时间过去多次时间
        /// 例子：５分钟前、１０分钟前、５小时及两天前、１年前
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetSpacingTime(this DateTime time)
        {
            DateTime now = DateTime.Now;
            TimeSpan span= now - time;

            double spanDay = span.TotalDays;
            double spanHour = span.TotalHours;
            double spanMinutes = span.TotalMinutes;

            string timeString = string.Empty;
            if (spanDay < 1)
            {
                if (spanHour > 1)
                {
                    return string.Format("{0}小时前", Convert.ToInt32(spanHour));
                }
                else
                {
                    if (spanMinutes > 1)
                    {
                        return string.Format("{0}分钟前", Convert.ToInt32(spanMinutes));
                    }
                    else
                    {
                        return string.Format("刚刚",Convert.ToInt32(span.TotalSeconds));
                    }
                }
            }
            else if (spanDay >= 1 && spanDay < 31)
            {
                return string.Format("{0}天前", Convert.ToInt32(spanDay));
            }
            else
            {
                return time.ToString("yyyy-MM--dd HH:mm");
            }
        }
    }
}