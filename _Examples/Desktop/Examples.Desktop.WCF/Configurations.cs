using JToolbox.WCF.Common;

namespace Examples.Desktop.WCF
{
    public static class Configurations
    {
        private static string appName = "WCFExamples";
        private static string serviceName = "TestService";

        public static NamedPipeConfiguration GetNamedPipeConfiguration()
        {
            
            return new NamedPipeConfiguration
            {
                ApplicationName = appName,
                ServiceName = serviceName
            };
        }

        public static NetTcpConfiguration GetNetTcpConfiguration(string ipAddress, int port)
        {
            return new NetTcpConfiguration
            {
                IpAddress = ipAddress,
                Port = port,
                ApplicationName = appName,
                ServiceName = serviceName
            };
        }
    }
}