using System.Collections.Generic;
using System.Threading.Tasks;

namespace JToolbox.Core.Utilities.Deferred
{
    public interface IDeferredTasksHandler<T>
    {
        Task ExecuteDeferredTasksAsync(List<T> tasks);
    }
}