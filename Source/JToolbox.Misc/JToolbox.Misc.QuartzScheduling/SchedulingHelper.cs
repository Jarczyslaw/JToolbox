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
            JobKey jobKey,
            TriggerKey triggerKey,
            string cronExpression,
            Action<TriggerBuilder> builderAction = null)
        {
            CronScheduleBuilder cronScheduleBuilder = CronScheduleBuilder.CronSchedule(cronExpression);
            return CreateCronTrigger(jobKey, triggerKey, cronScheduleBuilder, builderAction);
        }

        public static ITrigger CreateCronTrigger(
            JobKey jobKey,
            TriggerKey triggerKey,
            int hour,
            int minute,
            List<DayOfWeek> days,
            Action<TriggerBuilder> builderAction = null)
        {
            CronScheduleBuilder schedulebuilder = CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(hour, minute, days.ToArray());
            return CreateCronTrigger(jobKey, triggerKey, schedulebuilder, builderAction);
        }

        public static ITrigger CreateCronTrigger(
            JobKey jobKey,
            TriggerKey triggerKey,
            CronScheduleBuilder cronScheduleBuilder,
            Action<TriggerBuilder> builderAction = null)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .ForJob(jobKey)
                .WithSchedule(cronScheduleBuilder);

            builderAction?.Invoke(builder);

            return builder.Build();
        }

        public static ITrigger CreateIntervalTrigger(
            JobKey jobKey,
            TriggerKey triggerKey,
            TimeSpan interval,
            bool delayed = false,
            Action<TriggerBuilder> builderAction = null)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .ForJob(jobKey)
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
            JobKey jobKey,
            TriggerKey triggerKey,
            DateTime startAt,
            Action<TriggerBuilder> builderAction = null)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .ForJob(jobKey)
                .StartAt(new DateTimeOffset(startAt));

            builderAction?.Invoke(builder);

            return builder.Build();
        }

        public static ITrigger CreateStartNowTrigger(
            JobKey jobKey,
            TriggerKey triggerKey,
            Action<TriggerBuilder> builderAction = null)
        {
            TriggerBuilder builder = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .ForJob(jobKey)
                .StartNow();

            builderAction?.Invoke(builder);

            return builder.Build();
        }

        public static async Task Schedule(IScheduler scheduler, ITrigger trigger, IJobDetail jobDetail = null)
        {
            if (jobDetail == null)
            {
                jobDetail = await scheduler.GetJobDetail(trigger.JobKey);

                if (jobDetail == null) { throw new ArgumentException($"Can not find job with key {trigger.JobKey}"); }

                await scheduler.ScheduleJob(trigger);
            }
            else
            {
                if (!trigger.JobKey.IsTheSame(jobDetail.Key)) { throw new ArgumentException("Trigger's jobKey is invalid"); }

                await scheduler.ScheduleJob(jobDetail, trigger);
            }
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