using System;
using Quartz;
using Quartz.Impl;

namespace Pm25.WebCrawler
{
    public class MinuteJob : Quartz.IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //Pm25.DataAccess.JobMinuteLogic MinuteLogic = new DataAccess.JobMinuteLogic();
            //Pm25.DataAccess.JobMinute model = new DataAccess.JobMinute();
            //model.JobTime = DateTime.Now;
            //model.JobName = "MinuteJob";
            //model.JobKey = Guid.NewGuid();

           // MinuteLogic.Add(model);
        }
    }

    public class MinuteAutoJob
    {
        private static StdSchedulerFactory factory;
        public static void Start()
        {
            factory = new StdSchedulerFactory();
            // get a scheduler
            var scheduler = factory.GetScheduler();
            scheduler.Start();

            // define the job and tie it to our HelloJob class
            var job = JobBuilder.Create<MinuteJob>()
                .WithIdentity("Pm25_minute_Job", "minute")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            var trigger = TriggerBuilder.Create()
              .WithIdentity("Pm25_minute_Trigger", "minute")
              .StartNow()
             .WithDailyTimeIntervalSchedule(i => i.OnEveryDay().WithIntervalInMinutes(1))
                //StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0,60)))
                //.WithSimpleSchedule(x => x
                //.WithIntervalInSeconds(30)
                //.RepeatForever())
              .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}