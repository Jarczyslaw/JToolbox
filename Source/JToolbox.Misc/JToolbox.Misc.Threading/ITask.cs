using System;

namespace JToolbox.Misc.Threading
{
    public interface ITask
    {
        void Finish(TasksExecutor tasksExecutor, Exception exception, TimeSpan elapsed);

        void Run(TasksExecutor tasksExecutor);
    }
}