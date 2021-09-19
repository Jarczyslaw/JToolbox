using JToolbox.Misc.NetworkTools.Clients;
using JToolbox.Misc.NetworkTools.Inputs;
using JToolbox.Misc.NetworkTools.Results;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace JToolbox.Misc.NetworkTools
{
    public static class ScannersCommon
    {
        public static async Task<List<PortResult>> IsPortOpen(PortInput input, IPortClient portClient)
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

        public static async Task<PingResult> Ping(PingInput pingInput)
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
    }
}