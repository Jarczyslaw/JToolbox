using JToolbox.WCF;
using JToolbox.WCF.BindingConfigurations;
using System;
using WCFExample.Common;

namespace WCFExample.TestClient
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var config = new TcpConfiguration
                {
                    IpAddress = "192.168.0.104",
                    Port = 9999,
                    ApplicationName = "TestApp",
                    ServiceName = "TestService"
                };

                using (var client = new Client<ITestProxy>(config))
                {
                    client.Start();
                    Console.WriteLine(client.Proxy.Ping("Test message"));
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            Console.ReadKey();
        }
    }
}