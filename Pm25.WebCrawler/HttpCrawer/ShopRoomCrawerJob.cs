using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Quartz;
using Quartz.Impl;

namespace Pm25.WebCrawler
{
    using YG.SC.DataAccess;
    using Pm25.WebCrawler;
    using System.Threading.Tasks;
    using System.Threading;

    public class Pm25CrawerJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Guid jobkey = Guid.NewGuid();
         
        }

        /// <summary>
        /// 采集湿度
        /// </summary>
        public void Excute_Weather_RH()
        {
            //var weatherAreadb = new PmWeatherAreaLogic();
            //var weatherArealist = weatherAreadb.GetList();

            //var RHList = new List<WeatherRH>();
            //foreach (var item in weatherArealist)
            //{
            //    string url = string.Format("http://www.weather.com.cn/data/sk/" + item.CityNumber + ".html");
            //    try
            //    {
            //        if (item.CityNumber.HasValue == false)
            //        {
            //            continue;
            //        }
            //        WeatherRHCrawler wrhc = new WeatherRHCrawler(new Uri(url));
            //        var weatherRh = wrhc.Extract(item.CityNumber.Value);
            //        if (weatherRh != null)
            //        {
            //            RHList.Add(weatherRh);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        // string msg = ex.InnerException.Message;
            //    }
            //    Thread.Sleep(500);
            //}
            //var PmWeatherRHDb = new PmWeatherRHLogic();
            //PmWeatherRHDb.AddRange(RHList);
        }

    }


}