using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.Core.Utilities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public delegate void OnDeviceScanned(PingScanResult result);

    public delegate void OnScanComplete(List<PingScanResult> results);

    public class PingScanner
    {
        public event OnDeviceScanned OnDeviceScanned = delegate { };

        public event OnScanComplete OnScanComplete = delegate { };

        public async Task StartScan(IPAddress startAddress, IPAddress endAddress, int workers, int timeout, CancellationToken cancellationToken)
        {
            var result = new BlockingCollection<PingScanResult>();
            var addressesRange = NetworkUtils.GetAddressesInRange(startAddress, endAddress);
            var addressesPacks = addressesRange.ChunkInto(workers);
            await AsyncHelper.ForEach(addressesPacks, async (addressPack, token) =>
            {
                var ping = new Ping();
                foreach (var address in addressPack)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    var reply = await ping.SendPingAsync(address, timeout);
                    var pingResult = new PingScanResult
                    {
                        Address = address,
                        Reply = reply
                    };
                    result.Add(pingResult);
                    OnDeviceScanned(pingResult);
                }
            }, cancellationToken);
            OnScanComplete(result.ToList());
        }
    }
}