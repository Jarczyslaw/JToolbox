﻿using JToolbox.Misc.Threading;
using JToolbox.WPF.Core.Base;
using System;
using System.Diagnostics;
using System.Threading;

namespace TasksExecutorWpfExample
{
    public class TaskViewModel : BaseViewModel, ITask
    {
        public Action<TaskViewModel> OnTaskFinish;
        public Action<TaskViewModel> OnTaskStart;
        private double remaining;

        public TaskViewModel(int id, int duration)
        {
            Id = id;
            Duration = duration;
            Remaining = Duration;
        }

        public int Duration { get; }

        public int Id { get; }

        public double Remaining
        {
            get => remaining;
            set => Set(ref remaining, value);
        }

        public void Finish(TasksExecutor tasksExecutor, Exception exception, TimeSpan elapsed)
        {
            OnTaskFinish?.Invoke(this);
        }

        public void Run(TasksExecutor tasksExecutor)
        {
            OnTaskStart?.Invoke(this);
            var stopwatch = Stopwatch.StartNew();
            while (true)
            {
                Remaining = Math.Max(0, Duration - stopwatch.Elapsed.TotalSeconds);
                if (Duration - stopwatch.Elapsed.TotalSeconds <= 0d)
                {
                    break;
                }
                Thread.Sleep(1);
            }
        }
    }
}