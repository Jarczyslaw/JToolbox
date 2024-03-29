﻿using JToolbox.Misc.NetworkTools.Clients;
using JToolbox.Misc.NetworkTools.Inputs;
using JToolbox.Misc.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.Misc.NetworkTools
{
    public class ServiceScanner : ProcessingQueue<ServiceInput, bool>
    {
        public ScanProgress OnScanProgress = delegate { };

        private IPortClient portClient;

        public delegate void ScanProgress(ProcessingQueueItem<ServiceInput, bool> item);

        public Task<bool> CheckService(ServiceInput item, IPortClient portClient)
        {
            this.portClient = portClient;
            return ProcessItem(item);
        }

        public override async Task<bool> ProcessItem(ServiceInput item)
        {
            var pingResult = await ScannersCommon.Ping(new PingInput
            {
                Address = item.Endpoint.Address,
                Retries = item.Retries,
                Timeout = item.Timeout
            });

            if (pingResult.Reply == null || pingResult.Reply.Status != IPStatus.Success)
            {
                return false;
            }

            var scanResult = await ScannersCommon.IsPortOpen(new PortInput
            {
                Address = item.Endpoint.Address,
                Ports = new List<int> { item.Endpoint.Port },
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

        public Task<List<ProcessingQueueItem<ServiceInput, bool>>> ServiceScan(ServiceScanInput serviceScanInput, IPortClient portClient, CancellationToken cancellationToken = default)
        {
            this.portClient = portClient;
            var input = serviceScanInput.Addresses.Select(s => new ServiceInput
            {
                Endpoint = new IPEndPoint(s, serviceScanInput.Port),
                Retries = serviceScanInput.Retries,
                Timeout = serviceScanInput.Timeout
            }).ToList();
            return Run(input, cancellationToken);
        }
    }
}