using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JToolbox.XamarinForms.Perms
{
    public class PermsService : IPermsService
    {
        public Task<PermissionStatus> Check<T>()
            where T : Permissions.BasePermission, new()
        {
            return Permissions.CheckStatusAsync<T>();
        }

        public Task<PermissionStatus> Check(Type permissionType)
        {
            CheckPermissionType(permissionType);
            return (Task<PermissionStatus>)CreateGenericMethod(nameof(Permissions.CheckStatusAsync), permissionType)
                .Invoke(this, null);
        }

        public Task<PermissionStatus> CheckAndRequest<T>()
            where T : Permissions.BasePermission, new()
        {
            return CheckAndRequest(typeof(T));
        }

        public async Task<PermissionStatus> CheckAndRequest(Type permissionType)
        {
            var state = await Check(permissionType);
            if (state != PermissionStatus.Granted)
            {
                state = await Request(permissionType);
            }
            return state;
        }

        public async Task<bool> CheckAndRequest(List<Type> permissionTypes)
        {
            foreach (var permissionType in permissionTypes)
            {
                var state = await CheckAndRequest(permissionType);
                if (state != PermissionStatus.Granted)
                {
                    return false;
                }
            }
            return true;
        }

        public Task<PermissionStatus> Request<T>()
                                    where T : Permissions.BasePermission, new()
        {
            return Permissions.RequestAsync<T>();
        }

        public Task<PermissionStatus> Request(Type permissionType)
        {
            CheckPermissionType(permissionType);
            return (Task<PermissionStatus>)CreateGenericMethod(nameof(Permissions.RequestAsync), permissionType)
                .Invoke(this, null);
        }

        private void CheckPermissionType(Type permissionType)
        {
            if (!typeof(Permissions.BasePermission).IsAssignableFrom(permissionType))
            {
                throw new ArgumentException("Permission type has to derive from BasePermission");
            }
        }

        private MethodInfo CreateGenericMethod(string methodName, Type permissionType)
        {
            var method = typeof(Permissions).GetMethod(methodName);
            return method.MakeGenericMethod(permissionType);
        }
    }
}