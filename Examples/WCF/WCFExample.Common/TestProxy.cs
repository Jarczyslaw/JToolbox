using JToolbox.WCF;
using System.ServiceModel;

namespace WCFExample.Common
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TestProxy : ITestProxy, IProxy
    {
        public string Ping(string message)
        {
            return message;
        }
    }
}