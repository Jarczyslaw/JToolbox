using JToolbox.WCF.Common;

namespace Examples.Desktop.WCF
{
    public static class Configurations
    {
        public static NamedPipeConfiguration GetNamedPipeConfiguration()
        {
            var appName = "WCFExample";
            var serviceName = "TestService";
            return new NamedPipeConfiguration
            {
                ApplicationName = appName,
                ServiceName = serviceName
            };
        }
    }
}