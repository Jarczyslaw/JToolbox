using JToolbox.NetworkTools.Inputs;
using JToolbox.NetworkTools.Results;
using JToolbox.Threading;
using System.Collections.Generic;
using System.Linq;
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

        public override Task<PingResult> ProcessItem(PingInput item)
        {
            return ScannersCommon.Ping(item);
        }

        public override Task ReportProgress(ProcessingQueueItem<PingInput, PingResult> item)
        {
            OnScanProgress(item);
            return Task.CompletedTask;
        }
    }
}