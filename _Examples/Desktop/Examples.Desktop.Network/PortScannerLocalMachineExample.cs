using Examples.Desktop.Base;
using JToolbox.NetworkTools;
using JToolbox.NetworkTools.Clients;
using JToolbox.NetworkTools.Inputs;
using JToolbox.NetworkTools.Results;
using JToolbox.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Examples.Desktop.Network
{
    public class PortScannerLocalMachineExample : IDesktopExample
    {
        private IOutputInput outputInput;

        public string Title => "Port scanner - local machine";

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

            var address = IPAddress.Parse(addressString);
            var portsString = outputInput.Read("Insert ports (comma separated):", "80, 135");
            var ports = new List<int>();
            if (!string.IsNullOrEmpty(portsString))
            {
                foreach (var port in portsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList())
                {
                    if (int.TryParse(port, out int p))
                    {
                        ports.Add(p);
                    }
                }
            }

            if (ports.Count == 0)
            {
                outputInput.WriteLine("No ports provided");
                return;
            }

            outputInput.WriteLine("Current IP address: " + address.ToString());
            outputInput.WriteLine("Ports to scan: " + string.Join(", ", ports.Select(s => s.ToString())));
            outputInput.WriteLine("TCP Scan started!");
            var scanner = new PortScanner()
            {
                StopOnFirstException = false,
                TasksCount = 0
            };
            scanner.OnScanProgress += PortScanner_OnScanProgress;
            outputInput.StartTime();
            var result = await scanner.PortScan(new PortScanInput
            {
                Addresses = new List<IPAddress> { address },
                Ports = ports,
                Retries = 1,
                Timeout = 1000
            }, new TCPPortClient());
            outputInput.StopTime();
            var processed = result.GetResults();
            outputInput.WriteLine($"Scan completed. Scanned addresses: {processed.Count}, total scanned ports: {processed.Sum(s => s.Count)}");
        }

        private void PortScanner_OnScanProgress(ProcessingQueueItem<PortInput, List<PortResult>> item)
        {
            outputInput.WriteLine("Ports for " + item.Input.Address.ToString());
            foreach (var result in item.Output)
            {
                outputInput.WriteLine($"\t{result.Port} - {(result.IsOpen ? "Opened" : "Closed")}");
            }
        }
    }
}