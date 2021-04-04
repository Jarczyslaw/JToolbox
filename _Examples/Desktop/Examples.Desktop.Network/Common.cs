using Examples.Desktop.Base;
using JToolbox.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Desktop.Network
{
    public static class Common
    {
        public static string GetLocalAddress(IOutputInput outputInput)
        {
            IPAddress address = null;
            var localAddresses = NetworkUtils.GetLocalIPAddresses();
            if (localAddresses.Count > 1)
            {
                address = outputInput.SelectValue("Select local address:", localAddresses);
            }
            else if (localAddresses.Count == 1)
            {
                address = localAddresses[0];
            }

            if (address == null)
            {
                outputInput.WriteLine("No IP address selected");
                return null;
            }
            return address.ToString();
        }
    }
}
