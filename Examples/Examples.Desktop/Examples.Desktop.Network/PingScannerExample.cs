using Examples.Desktop.Base;
using JToolbox.Core.Utilities;
using JToolbox.Misc.NetworkTools;
using JToolbox.Misc.NetworkTools.Inputs;
using JToolbox.Misc.NetworkTools.Results;
using JToolbox.Misc.Threading;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Examples.Desktop.Network
{
    public class PingScannerExample : IDesktopExample
    {
        private IOutputInput outputInput;

        public string CustomActionTitle => null;

        public string Title => "Ping scanner";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public void CustomAction()
        {
            throw new System.NotImplementedException();
        }

        public async Task Run(IOutputInput outputInput)
        {
            this.outputInput = outputInput;
            var address = Common.GetLocalAddress(outputInput);
            if (address == null)
            {
                return;
            }

            var mask = NetworkUtils.GetSubnetMask(address);

            var addresses = NetworkUtils.GetAddressesInNetwork(address, mask);
            if (addresses.Count == 0)
            {
                outputInput.WriteLine("No addresses to scan");
                return;
            }

            outputInput.WriteLine("Current IP address: " + address.ToString());
            outputInput.WriteLine("Mask: " + mask.ToString());
            outputInput.WriteLine("Addresses to scan: " + addresses.Count);
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