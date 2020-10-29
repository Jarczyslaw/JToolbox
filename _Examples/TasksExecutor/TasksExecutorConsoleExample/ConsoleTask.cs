using JToolbox.Threading.TasksExecution;
using System;
using System.Threading;

namespace TasksExecutorConsoleExample
{
    public class ConsoleTask : BaseTask
    {
        private static int taskId;
        private static readonly Random random = new Random();

        public int TaskId { get; set; }

        public ConsoleTask()
        {
            taskId++;
            TaskId = taskId;
        }

        public override void Run()
        {
            Console.WriteLine($"Task {TaskId} started");
            Thread.Sleep(random.Next(100, 2000));
            Console.WriteLine($"Task {TaskId} stopped");
        }
    }
}