using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

namespace JToolbox.XamarinForms.Perms
{
    public class PermsService : IPermsService
    {
        public Task<PermissionStatus> Check(BasePermission permission)
        {
            return permission.CheckStatusAsync();
        }

        public async Task<PermissionStatus> CheckAndRequest(BasePermission permission)
        {
            var state = await permission.CheckStatusAsync();
            if (state != PermissionStatus.Granted)
            {
                state = await permission.RequestAsync();
            }
            return state;
        }

        public async Task<bool> CheckAndRequest(List<BasePermission> permissions)
        {
            foreach (var permission in permissions)
            {
                var state = await CheckAndRequest(permission);
                if (state != PermissionStatus.Granted)
                {
                    return false;
                }
            }

            return true;
        }
    }
}