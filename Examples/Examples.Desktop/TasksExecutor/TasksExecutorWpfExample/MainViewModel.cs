using JToolbox.Misc.SysInformation;
using JToolbox.Misc.Threading;
using JToolbox.WPF.Core.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace TasksExecutorWpfExample
{
    public class MainViewModel : BaseViewModel
    {
        private readonly TasksExecutor tasksExecutor = new TasksExecutor();
        private TasksExecutorState state;
        private ObservableCollection<TaskViewModel> pendingTasks = new ObservableCollection<TaskViewModel>();
        private ObservableCollection<TaskViewModel> activeTasks = new ObservableCollection<TaskViewModel>();
        private ObservableCollection<TaskViewModel> completedTasks = new ObservableCollection<TaskViewModel>();
        private int taskDelay = 4;
        private int id;
        private int tasksCount = 1;
        private string title;

        public MainViewModel()
        {
            state = tasksExecutor.GetState();
            tasksExecutor.OnTasksExecutorStateChanged += s => State = s;
            UpdateTitle();
            StartUpdateTimer();
        }

        public TasksExecutorState State
        {
            get => state;
            set => Set(ref state, value);
        }

        public int MaxThreads
        {
            get => tasksExecutor.MaxThreads;
            set
            {
                tasksExecutor.MaxThreads = value;
                OnPropertyChanged(nameof(MaxThreads));
            }
        }

        public int MinThreads
        {
            get => tasksExecutor.MinThreads;
            set
            {
                tasksExecutor.MinThreads = value;
                OnPropertyChanged(nameof(MinThreads));
            }
        }

        public int ThreadsTimeout
        {
            get => tasksExecutor.ThreadsTimeout.HasValue ? (int)Math.Round(tasksExecutor.ThreadsTimeout.Value.TotalSeconds) : 0;
            set
            {
                tasksExecutor.ThreadsTimeout = TimeSpan.FromSeconds(value);
                OnPropertyChanged(nameof(ThreadsTimeout));
            }
        }

        public ObservableCollection<TaskViewModel> PendingTasks
        {
            get => pendingTasks;
            set => Set(ref pendingTasks, value);
        }

        public ObservableCollection<TaskViewModel> ActiveTasks
        {
            get => activeTasks;
            set => Set(ref activeTasks, value);
        }

        public ObservableCollection<TaskViewModel> CompletedTasks
        {
            get => completedTasks;
            set => Set(ref completedTasks, value);
        }

        public int TaskDelay
        {
            get => taskDelay;
            set => Set(ref taskDelay, value);
        }

        public int TasksCount
        {
            get => tasksCount;
            set => Set(ref tasksCount, value);
        }

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public RelayCommand AddTaskCommand => new RelayCommand(() => CreateNewTask(TasksCount));

        private void CreateNewTask(int tasksCount = 1)
        {
            for (int i = 0; i < tasksCount; i++)
            {
                id++;
                var task = new TaskViewModel(id, TaskDelay);
                task.OnTaskStart += t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PendingTasks.Remove(t);
                        ActiveTasks.Insert(0, t);
                    });
                };
                task.OnTaskFinish += t =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ActiveTasks.Remove(t);
                        CompletedTasks.Insert(0, t);
                    });
                };
                PendingTasks.Add(task);
                tasksExecutor.Add(task);
            }
        }

        private void StartUpdateTimer()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1d)
            };
            timer.Tick += (s, e) => UpdateTitle();
            timer.Start();
        }

        private void UpdateTitle()
        {
            Title = $"Tasks executor (CPU usage: {Math.Round(PerformanceMonitor.GetCpuUsage())}%, Free memory: {PerformanceMonitor.GetAvailableMemoryMB()}MB)";
        }
    }
}