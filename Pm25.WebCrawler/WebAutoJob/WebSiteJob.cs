using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using System.Threading;

using Quartz;
using Quartz.Impl;
namespace Pm25.WebCrawler
{
    public class WebSiteJob
    {
        private static StdSchedulerFactory factory;
        public static void Start()
        {
            factory = new StdSchedulerFactory();
            // get a scheduler
            var scheduler = factory.GetScheduler();
            scheduler.Start();

            // define the job and tie it to our HelloJob class
            var job = JobBuilder.Create<Pm25CrawerJob>()
                .WithIdentity("Pm25_in_Job", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            var trigger = TriggerBuilder.Create()
              .WithIdentity("Pm25_in_Trigger", "group1")
              .StartNow()
             .WithDailyTimeIntervalSchedule(i => i.OnEveryDay().WithIntervalInHours(1))

            // StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0,60)))
                //.WithSimpleSchedule(x => x
                //   .WithIntervalInSeconds(30)
                //  .RepeatForever())
              .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}