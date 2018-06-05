using System;
using Quartz;
using Quartz.Impl;
using TMF_ftp.Util;

namespace TMF_ftp.Services
{
    public class TaskScheduler
    {
        public TaskScheduler()
        {
        }

        public void Perform()
        {
            try
            {
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                IScheduler sched = schedFact.GetScheduler();
                sched.Start();

                IJobDetail job = JobBuilder.Create<AutoJob>()
                    .WithIdentity("myJob", "group1")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s
                            //.WithIntervalInHours(1)
                            .WithIntervalInMinutes(58)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 00))
                            .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 00))
                    )
                    .Build();
                sched.ScheduleJob(job, trigger);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}