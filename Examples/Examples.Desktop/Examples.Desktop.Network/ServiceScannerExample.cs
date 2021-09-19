using Examples.Desktop.Base;
using JToolbox.Core.Utilities;
using JToolbox.Misc.NetworkTools;
using JToolbox.Misc.NetworkTools.Clients;
using JToolbox.Misc.NetworkTools.Inputs;
using JToolbox.Misc.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.Desktop.Network
{
    public class ServiceScannerExample : IDesktopExample
    {
        private IOutputInput outputInput;
        public string Title => "Service scanner - find machines with port";

        public Task CleanUp()
        {
            return Task.CompletedTask;
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

            var port = Common.GetPort(outputInput, 9989);
            if (port == 0)
            {
                return;
            }

            var addresses = NetworkUtils.GetAddressesInNetwork(address, mask);
            if (addresses.Count == 0)
            {
                outputInput.WriteLine("No addresses to scan");
                return;
            }

            outputInput.WriteLine("Current IP address: " + address.ToString());
            outputInput.WriteLine("Mask: " + mask.ToString());
            outputInput.WriteLine("Addresses to scan: " + addresses.Count);
            outputInput.WriteLine("Port to scan: " + port);
            outputInput.WriteLine("Scan started!");

            var scanner = new ServiceScanner()
            {
                StopOnFirstException = false,
                TasksCount = 0
            };
            scanner.OnScanProgress += ServiceScanner_OnScanProgress;
            outputInput.StartTime();
            var result = await scanner.ServiceScan(new ServiceScanInput
            {
                Addresses = addresses,
                Port = port,
                Retries = 1,
                Timeout = 1000
            }, new TCPPortClient());
            outputInput.StopTime();
            var processed = result.GetResults();
            outputInput.WriteLine($"Scan completed. Scanned addresses: {processed.Count}, found services: {result.Count(r => r.Output)}");

            return;
        }

        private void ServiceScanner_OnScanProgress(ProcessingQueueItem<ServiceInput, bool> item)
        {
            if (item.Output)
            {
                outputInput.WriteLine("Found service on: " + item.Input.Endpoint.ToString());
            }
        }
    }
}