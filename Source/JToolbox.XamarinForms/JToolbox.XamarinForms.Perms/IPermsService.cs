using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace JToolbox.XamarinForms.Perms
{
    public interface IPermsService
    {
        Task<PermissionStatus> Check(BasePermission permission);

        Task<bool> CheckAndRequest(List<BasePermission> permissions);

        Task<PermissionStatus> CheckAndRequest(BasePermission permission);
    }
}