using JToolbox.Core.Utilities;
using System.Net;

namespace Examples.Desktop.Base
{
    public static class Common
    {
        public static IPAddress GetLocalAddress(IOutputInput outputInput)
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
            return address;
        }

        public static IPAddress GetMask(IOutputInput outputInput)
        {
            var inputString = outputInput.Read("Insert mask:", "255.255.255.0", s =>
            {
                if (!IPAddress.TryParse(s, out IPAddress _))
                {
                    return "Invalid mask format";
                }
                return null;
            });

            if (string.IsNullOrEmpty(inputString))
            {
                outputInput.WriteLine("No mask provided");
                return null;
            }
            return IPAddress.Parse(inputString);
        }

        public static int GetPort(IOutputInput outputInput, int port)
        {
            var portString = outputInput.Read("Insert port:", port.ToString(), s =>
            {
                if (!int.TryParse(s, out int _))
                {
                    return "Invalid port value";
                }
                return null;
            });

            if (string.IsNullOrEmpty(portString))
            {
                outputInput.WriteLine("Invalid port value");
                return 0;
            }
            return int.Parse(portString);
        }
    }
}