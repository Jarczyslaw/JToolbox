using System;

namespace JToolbox.Misc.Threading
{
    public interface ITask
    {
        void Run(TasksExecutor tasksExecutor);
        void Finish(TasksExecutor tasksExecutor, Exception exception, TimeSpan elapsed);
    }
}