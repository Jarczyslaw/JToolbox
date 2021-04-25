using System.Web.Caching;

namespace JToolbox.MVC.Core.Caching
{
    public interface IHttpContextCacheProvider : ICacheProvider
    {
        Cache Cache { get; }
    }
}