using System.Runtime.CompilerServices;

namespace JToolbox.AppSettings
{
    public interface IAppConfigService
    {
        string GetValue([CallerMemberName] string key = null, bool throwIfNotExists = true);
    }
}