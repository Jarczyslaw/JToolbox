using JToolbox.Threading.TasksExecution;
using System;

namespace TasksExecutorConsoleExample
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var executor = new TasksExecutor
            {
                ThreadsTimeout = TimeSpan.FromSeconds(2)
            };
            executor.OnTasksExecutorStateChanged += Executor_OnTasksExecutorStateChanged;
            for (int i = 0; i < 5; i++)
            {
                executor.Add(new ConsoleTask());
            }

            Console.ReadKey();
        }

        private static void Executor_OnTasksExecutorStateChanged(TasksExecutorState state)
        {
            Console.WriteLine($"Threads: {state.Threads}, working: {state.WorkingThreads}, idle: {state.IdleThreads}, waiting: {state.WaitingThreads}, pending tasks: {state.PendingTasks}");
        }
    }
}