using JToolbox.NetworkTools.Clients;
using JToolbox.NetworkTools.Inputs;
using JToolbox.NetworkTools.Results;
using JToolbox.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public class PortScanner : ProcessingQueue<PortInput, List<PortResult>>
    {
        public delegate void ScanProgress(ProcessingQueueItem<PortInput, List<PortResult>> item);

        public ScanProgress OnScanProgress = delegate { };

        private IPortClient portClient;

        public Task<List<ProcessingQueueItem<PortInput, List<PortResult>>>> PortScan(PortScanInput portScanInput, IPortClient portClient, CancellationToken cancellationToken = default)
        {
            this.portClient = portClient;
            var input = portScanInput.Addresses.Select(s => new PortInput
            {
                Address = s,
                Ports = portScanInput.Ports,
                Retries = portScanInput.Retries,
                Timeout = portScanInput.Timeout
            }).ToList();
            return Run(input, cancellationToken);
        }

        public async Task<List<PortResult>> IsPortOpen(PortInput input, IPortClient portClient)
        {
            var result = new List<PortResult>();
            foreach (var port in input.Ports)
            {
                var isOpen = false;
                Exception exception = null;
                for (int i = 0; i < input.Retries; i++)
                {
                    try
                    {
                        isOpen = await portClient.Check(input.Address, port, input.Timeout);
                        if (isOpen)
                        {
                            break;
                        }
                    }
                    catch (Exception exc)
                    {
                        exception = exc;
                    }
                }

                result.Add(new PortResult
                {
                    IsOpen = isOpen,
                    Port = port,
                    LastException = exception
                });
            }
            return result;
        }

        public override Task<List<PortResult>> ProcessItem(PortInput item)
        {
            return IsPortOpen(item, portClient);
        }

        public override Task ReportProgress(ProcessingQueueItem<PortInput, List<PortResult>> item)
        {
            OnScanProgress(item);
            return Task.CompletedTask;
        }
    }
}