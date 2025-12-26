using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JToolbox.Core.Utilities.Deferred
{
    public class DeferredTasks<T>
    {
        private static readonly object _lock = new object();
        private readonly List<T> _deferredTasks = new List<T>();
        private readonly IDeferredTasksHandler<T> _handler;

        public DeferredTasks(IDeferredTasksHandler<T> handler)
        {
            _handler = handler;
        }

        public bool IsRunning { get; private set; }

        public bool TryRun(T task)
        {
            lock (_lock)
            {
                _deferredTasks.Add(task);

                if (IsRunning) { return false; }

                IsRunning = true;

                List<T> tasksToExecute = FlushTasks();

                Task.Run(() => Execute(tasksToExecute));

                return true;
            }
        }

        private async Task Execute(List<T> tasksToExecute)
        {
            while (true)
            {
                await _handler.ExecuteDeferredTasksAsync(tasksToExecute);

                lock (_lock)
                {
                    if (_deferredTasks.Count == 0)
                    {
                        IsRunning = false;
                        return;
                    }

                    tasksToExecute = FlushTasks();
                }
            }
        }

        private List<T> FlushTasks()
        {
            List<T> tasks = _deferredTasks.ToList();
            _deferredTasks.Clear();

            return tasks;
        }
    }
}