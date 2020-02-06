using JToolbox.WCF;
using JToolbox.WCF.BindingConfigurations;
using System;
using WCFExample.Common;

namespace WCFExample.TestServer
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

                using (var server = Server.CreateSingle<ITestProxy>(config, new TestProxy()))
                {
                    server.Start();
                    Console.WriteLine("Server started");
                    Console.ReadKey();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                Console.ReadKey();
            }
        }
    }
}