using System.Runtime.CompilerServices;

namespace JToolbox.Misc.AppConfig
{
    public interface IAppConfigService
    {
        string GetValue([CallerMemberName] string key = null, bool throwIfNotExists = true);
    }
}