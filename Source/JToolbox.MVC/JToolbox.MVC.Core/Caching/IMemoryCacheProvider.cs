using System.Runtime.Caching;

namespace JToolbox.MVC.Core.Caching
{
    public interface IMemoryCacheProvider : ICacheProvider
    {
        MemoryCache Cache { get; }
    }
}