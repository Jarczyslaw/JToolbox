using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.Core.Utilities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public delegate void OnDeviceScanned(PingResult result);

    public delegate void OnScanComplete(List<PingResult> results);

    public class PingScanner
    {
        public event OnDeviceScanned OnDeviceScanned = delegate { };

        public event OnScanComplete OnScanComplete = delegate { };

        public async Task PingScan(PingScanInput pingScanInput)
        {
            var result = new BlockingCollection<PingResult>();
            var addressesRange = NetworkUtils.GetAddressesInRange(pingScanInput.StartAddress, pingScanInput.EndAddress);
            var addressesPacks = addressesRange.ChunkInto(pingScanInput.Workers);

            var ping = new Ping();
            await AsyncHelper.ForEach(addressesPacks, async (addressPack, token) =>
            {
                foreach (var address in addressPack)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    var pingResult = await Ping(new PingInput
                    {
                        Address = address,
                        Retries = pingScanInput.Retries,
                        Timeout = pingScanInput.Timeout
                    });
                    result.Add(pingResult);
                    OnDeviceScanned(pingResult);
                }
            }, pingScanInput.CancellationToken);
            OnScanComplete(result.ToList());
        }

        public async Task<PingResult> Ping(PingInput pingInput)
        {
            using (var ping = new Ping())
            {
                PingReply pingReply = null;
                for (int i = 0; i < pingInput.Retries; i++)
                {
                    pingReply = await ping.SendPingAsync(pingInput.Address, pingInput.Timeout);
                    if (pingReply.Status == IPStatus.Success)
                    {
                        break;
                    }
                }
                return new PingResult
                {
                    Address = pingInput.Address,
                    Reply = pingReply
                };
            }
        }
    }
}