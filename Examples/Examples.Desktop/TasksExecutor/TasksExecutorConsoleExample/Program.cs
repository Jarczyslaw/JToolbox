using JToolbox.Misc.Threading;
using System;
using System.Threading;

namespace TasksExecutorConsoleExample
{
    internal static class Program
    {
        private static void Executor_OnTasksExecutorStateChanged(TasksExecutorState state)
        {
            Console.WriteLine($"Threads: {state.Threads}, working: {state.WorkingThreads}, idle: {state.IdleThreads}, waiting: {state.WaitingThreads}, pending tasks: {state.PendingTasks}");
        }

        private static void Main(string[] args)
        {
            var executor = new TasksExecutor
            {
                ThreadsTimeout = TimeSpan.FromSeconds(3),
                MaxThreads = 10
            };
            executor.OnTasksExecutorStateChanged += Executor_OnTasksExecutorStateChanged;
            for (int i = 0; i < 100; i++)
            {
                executor.Add(new ConsoleTask());
                Thread.Sleep(200);
            }

            Console.ReadKey();
        }
    }
}