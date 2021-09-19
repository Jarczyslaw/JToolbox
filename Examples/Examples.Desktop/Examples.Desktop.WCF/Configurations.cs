using JToolbox.Misc.WCF.Common;

namespace Examples.Desktop.WCF
{
    public static class Configurations
    {
        private static string appName = "WCFExamples";
        private static string serviceName = "TestService";

        public static BasicHttpConfiguration GetBasicHttpConfiguration(string ipAddress, int port)
        {
            return new BasicHttpConfiguration
            {
                IpAddress = ipAddress,
                Port = port,
                ApplicationName = appName,
                ServiceName = serviceName
            };
        }

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