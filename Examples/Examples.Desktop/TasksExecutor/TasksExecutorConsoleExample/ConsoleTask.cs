using JToolbox.Misc.Threading;
using System;
using System.Threading;

namespace TasksExecutorConsoleExample
{
    public class ConsoleTask : ITask
    {
        private static readonly Random random = new Random();
        private static int taskId;

        public ConsoleTask()
        {
            taskId++;
            TaskId = taskId;
        }

        public int TaskId { get; set; }

        public void Finish(TasksExecutor tasksExecutor, Exception exception, TimeSpan elapsed)
        {
            Console.WriteLine($"Task {TaskId} stopped in {elapsed.TotalMilliseconds}ms");
        }

        public void Run(TasksExecutor tasksExecutor)
        {
            Console.WriteLine($"Task {TaskId} started");
            Thread.Sleep(random.Next(100, 2000));
        }
    }
}