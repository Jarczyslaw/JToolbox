using System;
using System.Security.Principal;

namespace JToolbox.Misc.WindowsHelpers
{
    public class WindowsHelper
    {
        public static bool HasUserAdministratorRights()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}