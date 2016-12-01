using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YG.SC.DataAccess;

namespace YG.SC.Service
{
    public class JobMinuteLogic
    {
        public void Add(YG.SC.DataAccess.JobMinute model)
        {
            using (var db = new YG.SC.DataAccess.Entities ())
            {
                db.JobMinute.Add(model);
                db.SaveChanges();
            }
        }

        public void Add(string jobName, string jobKey)
        {
            JobMinute model = new JobMinute();
            model.JobTime = DateTime.Now;
            model.JobName = jobName;
            model.JobKey = jobKey;
            this.Add(model);
        }
    }
}