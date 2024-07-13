using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JToolbox.Misc.QuartzScheduling
{
    public static class SchedulingHelper
    {
        public static ITrigger CreateCronTrigger(
            string cronExpression,
            string jobName,
            Action<TriggerBuilder> builderAction = null)
        {
            CronScheduleBuilder cronScheduleBuilder = CronScheduleBuilder.CronSchedule(cronExpression);
            return CreateCronTrigger(cronScheduleBuilder, jobName, builderAction);
        }

        public static ITrigger CreateCronTrigger(
            int hour,
            int minute,
            List<DayOfWeek> days,
            string jobName,
            Action<TriggerBuilder> builderAction = null)
        {
            CronScheduleBuilder schedulebuilder = CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(hour, minute, days.ToArray());
            return CreateCronTrigger(schedulebuilder, jobName, builderAction);
        }

        public static ITrigger CreateCronTrigger(
            CronScheduleBuilder cronScheduleBuilder,
            string jobName,
            Action<TriggerBuilder> builderAction = null)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .WithSchedule(cronScheduleBuilder)
                .ForJob(jobName);

            builderAction?.Invoke(builder);

            return builder.Build();
        }

        public static ITrigger CreateIntervalTrigger(
            TimeSpan interval,
            string jobName,
            bool delayed = false,
            Action<TriggerBuilder> builderAction = null)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .ForJob(jobName)
                .WithSimpleSchedule(x => x.WithInterval(interval).RepeatForever());

            if (delayed) { builder = builder.StartAt(DateTimeOffset.Now.Add(interval)); }

            builderAction?.Invoke(builder);

            return builder.Build();
        }

        public static IJobDetail CreateJobDetail<T>(JobKey jobKey, Action<JobBuilder> builderAction = null)
            => CreateJobDetail(typeof(T), jobKey, builderAction);

        public static IJobDetail CreateJobDetail(Type jobType, JobKey jobKey, Action<JobBuilder> builderAction = null)
        {
            JobBuilder jobBuilder = JobBuilder.Create()
                .OfType(jobType)
                .WithIdentity(jobKey);

            builderAction?.Invoke(jobBuilder);

            return jobBuilder.Build();
        }

        public static ITrigger CreateStartAtTrigger(
            DateTime startAt,
            string jobName,
            Action<TriggerBuilder> builderAction = null)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .ForJob(jobName)
                .StartAt(new DateTimeOffset(startAt));

            builderAction?.Invoke(builder);

            return builder.Build();
        }

        public static ITrigger CreateStartNowTrigger(string jobName, Action<TriggerBuilder> builderAction = null)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .ForJob(jobName)
                .StartNow();

            builderAction?.Invoke(builder);

            return builder.Build();
        }

        public static async Task Launch(IScheduler scheduler, IJobDetail jobDetail, ITrigger trigger)
        {
            if (trigger.JobKey != jobDetail.Key) { throw new ArgumentException("Trigger's jobKey is invalid"); }

            IJobDetail currentJob = await scheduler.GetJobDetail(jobDetail.Key);
            if (currentJob == null)
            {
                await scheduler.ScheduleJob(jobDetail, trigger);
                return;
            }

            await UnscheduleJob(scheduler, jobDetail.Key);
            await scheduler.ScheduleJob(trigger);
        }

        public static async Task UnscheduleJob(IScheduler scheduler, JobKey jobKey)
        {
            IReadOnlyCollection<ITrigger> triggers = await scheduler.GetTriggersOfJob(jobKey);

            if (triggers.Any())
            {
                await scheduler.UnscheduleJobs(triggers.Select(x => x.Key).ToList());
            }
        }
    }
}