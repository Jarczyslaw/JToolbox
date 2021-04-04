using JToolbox.NetworkTools.Inputs;
using JToolbox.NetworkTools.Results;
using JToolbox.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public class PingScanner : ProcessingQueue<PingInput, PingResult>
    {
        public delegate void ScanProgress(ProcessingQueueItem<PingInput, PingResult> item);

        public event ScanProgress OnScanProgress = delegate { };

        public Task<List<ProcessingQueueItem<PingInput, PingResult>>> PingScan(PingScanInput pingScanInput, CancellationToken token = default)
        {
            var input = pingScanInput.Addresses.Select(s => new PingInput
            {
                Address = s,
                Retries = pingScanInput.Retries,
                Timeout = pingScanInput.Timeout
            }).ToList();
            return Run(input, token);
        }

        public async Task<PingResult> Ping(PingInput pingInput)
        {
            using (var ping = new Ping())
            {
                PingReply pingReply = null;
                Exception exception = null;
                for (int i = 0; i < pingInput.Retries; i++)
                {
                    try
                    {
                        pingReply = await ping.SendPingAsync(pingInput.Address, pingInput.Timeout);
                        if (pingReply.Status == IPStatus.Success)
                        {
                            break;
                        }
                    }
                    catch (Exception exc)
                    {
                        exception = exc;
                    }
                }
                return new PingResult
                {
                    Reply = pingReply,
                    LastException = exception
                };
            }
        }

        public override Task<PingResult> ProcessItem(PingInput item)
        {
            return Ping(item);
        }

        public override Task ReportProgress(ProcessingQueueItem<PingInput, PingResult> item)
        {
            OnScanProgress(item);
            return Task.CompletedTask;
        }
    }
}