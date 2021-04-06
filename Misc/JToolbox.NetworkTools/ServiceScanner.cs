using JToolbox.NetworkTools.Clients;
using JToolbox.NetworkTools.Inputs;
using JToolbox.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public class ServiceScanner : ProcessingQueue<ServiceInput, bool>
    {
        public delegate void ScanProgress(ProcessingQueueItem<ServiceInput, bool> item);

        public ScanProgress OnScanProgress = delegate { };

        private IPortClient portClient;

        public Task<List<ProcessingQueueItem<ServiceInput, bool>>> ServiceScan(ServiceScanInput serviceScanInput, IPortClient portClient, CancellationToken cancellationToken = default)
        {
            this.portClient = portClient;
            var input = serviceScanInput.Addresses.Select(s => new ServiceInput
            {
                Address = s,
                Port = serviceScanInput.Port,
                Retries = serviceScanInput.Retries,
                Timeout = serviceScanInput.Timeout
            }).ToList();
            return Run(input, cancellationToken);
        }

        public Task<bool> CheckService(ServiceInput item, IPortClient portClient)
        {
            this.portClient = portClient;
            return ProcessItem(item);
        }

        public override async Task<bool> ProcessItem(ServiceInput item)
        {
            var scanResult = await ScannersCommon.IsPortOpen(new PortInput
            {
                Address = item.Address,
                Ports = new List<int> { item.Port },
                Retries = item.Retries,
                Timeout = item.Timeout
            }, portClient);
            return scanResult[0].IsOpen;
        }

        public override Task ReportProgress(ProcessingQueueItem<ServiceInput, bool> item)
        {
            OnScanProgress(item);
            return Task.CompletedTask;
        }
    }
}