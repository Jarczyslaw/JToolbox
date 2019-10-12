using System.Web.SessionState;

namespace JToolbox.MVC.Core.Caching
{
    public interface ISessionProvider : ICacheProvider
    {
        HttpSessionState Session { get; }
    }
}