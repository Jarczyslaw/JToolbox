using System;

namespace JToolbox.Threading.TasksExecution
{
    public abstract class BaseTask
    {
        public TimeSpan Elapsed { get; set; }
        public Exception Exception { get; set; }

        public abstract void Run();
    }
}