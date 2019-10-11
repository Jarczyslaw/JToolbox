using System.Runtime.CompilerServices;

namespace JToolbox.Desktop.Core.Services
{
    public interface IAppConfigService
    {
        string GetValue([CallerMemberName] string key = null, bool throwIfNotExists = true);
    }
}