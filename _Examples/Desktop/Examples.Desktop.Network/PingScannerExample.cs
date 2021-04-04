using Examples.Desktop.Base;
using JToolbox.Core.Extensions;
using JToolbox.Core.Utilities;
using JToolbox.NetworkTools;
using JToolbox.NetworkTools.Inputs;
using JToolbox.NetworkTools.Results;
using JToolbox.Threading;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Examples.Desktop.Network
{
    public class PingScannerExample : IDesktopExample
    {
        private IOutputInput outputInput;

        public string Title => "Ping scanner";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public async Task Run(IOutputInput outputInput)
        {
            this.outputInput = outputInput;
            var addressString = Common.GetLocalAddress(outputInput);
            if (string.IsNullOrEmpty(addressString))
            {
                return;
            }

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
                return;
            }

            var address = IPAddress.Parse(addressString);
            var mask = IPAddress.Parse(inputString);
            var startAddress = NetworkUtils.GetNetworkAddress(address, mask).Add(1);
            var endAddress = NetworkUtils.GetBroadcastAddress(address, mask).Add(-1);
            var addresses = NetworkUtils.GetContinousAddressesInRange(startAddress, endAddress);

            if (addresses.Count == 0)
            {
                outputInput.WriteLine("No addresses to scan");
                return;
            }

            outputInput.WriteLine("Current IP address: " + address.ToString());
            outputInput.WriteLine("Mask: " + mask.ToString());
            outputInput.WriteLine("Start address: " + startAddress.ToString());
            outputInput.WriteLine("End address: " + endAddress.ToString());
            outputInput.WriteLine("Count of addresses to scan: " + addresses.Count);
            outputInput.WriteLine("Scan started!");

            var pingScanner = new PingScanner()
            {
                StopOnFirstException = false,
                TasksCount = 0
            };
            pingScanner.OnScanProgress += PingScanner_OnScanProgress;
            outputInput.StartTime();
            var result = await pingScanner.PingScan(new PingScanInput
            {
                Addresses = addresses,
                Retries = 1,
                Timeout = 1000
            });
            outputInput.StopTime();
            var processed = result.GetResults();
            var found = processed.Where(s => s.Reply?.Status == IPStatus.Success);
            outputInput.WriteLine($"Scan completed. Scanned addresses: {processed.Count}, found {found.Count()} devices");
        }

        private void PingScanner_OnScanProgress(ProcessingQueueItem<PingInput, PingResult> item)
        {
            if (item.Output?.Reply?.Status == IPStatus.Success)
            {
                outputInput.WriteLine("Device found on: " + item.Input.Address);
            }
        }
    }
}