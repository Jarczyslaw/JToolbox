using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JToolbox.XamarinForms.Perms
{
    public interface IPermsService
    {
        Task<PermissionStatus> Check(Type permissionType);

        Task<PermissionStatus> Check<T>() where T : Permissions.BasePermission, new();

        Task<bool> CheckAndRequest(List<Type> permissionTypes);

        Task<PermissionStatus> CheckAndRequest(Type permissionType);

        Task<PermissionStatus> CheckAndRequest<T>() where T : Permissions.BasePermission, new();

        Task<PermissionStatus> Request(Type permissionType);

        Task<PermissionStatus> Request<T>() where T : Permissions.BasePermission, new();
    }
}