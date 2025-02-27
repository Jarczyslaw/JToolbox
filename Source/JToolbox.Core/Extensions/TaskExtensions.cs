using System.Threading.Tasks;

namespace JToolbox.Core.Extensions
{
    public static class TaskExtensions
    {
        public static void RunSync<T>(this Task<T> task)
            => task.GetAwaiter().GetResult();

        public static void RunSync(this Task task)
            => task.GetAwaiter().GetResult();
    }
}