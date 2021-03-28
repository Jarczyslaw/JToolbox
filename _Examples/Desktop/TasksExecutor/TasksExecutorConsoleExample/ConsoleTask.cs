using JToolbox.Threading;
using System;
using System.Threading;

namespace TasksExecutorConsoleExample
{
    public class ConsoleTask : ITask
    {
        private static int taskId;
        private static readonly Random random = new Random();

        public int TaskId { get; set; }

        public ConsoleTask()
        {
            taskId++;
            TaskId = taskId;
        }

        public void Run(TasksExecutor tasksExecutor)
        {
            Console.WriteLine($"Task {TaskId} started");
            Thread.Sleep(random.Next(100, 2000));
        }

        public void Finish(TasksExecutor tasksExecutor, Exception exception, TimeSpan elapsed)
        {
            Console.WriteLine($"Task {TaskId} stopped in {elapsed.TotalMilliseconds}ms");
        }
    }
}