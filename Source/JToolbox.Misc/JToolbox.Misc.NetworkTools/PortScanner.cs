using JToolbox.Misc.NetworkTools.Clients;
using JToolbox.Misc.NetworkTools.Inputs;
using JToolbox.Misc.NetworkTools.Results;
using JToolbox.Misc.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Misc.NetworkTools
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

        public Task<List<PortResult>> CheckPort(PortInput item, IPortClient portClient)
        {
            this.portClient = portClient;
            return ProcessItem(item);
        }

        public override Task<List<PortResult>> ProcessItem(PortInput item)
        {
            return ScannersCommon.IsPortOpen(item, portClient);
        }

        public override Task ReportProgress(ProcessingQueueItem<PortInput, List<PortResult>> item)
        {
            OnScanProgress(item);
            return Task.CompletedTask;
        }
    }
}