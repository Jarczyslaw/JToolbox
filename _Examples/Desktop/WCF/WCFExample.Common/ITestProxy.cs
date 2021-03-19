using JToolbox.WCF;
using System.ServiceModel;

namespace WCFExample.Common
{
    [ServiceContract]
    public interface ITestProxy : IProxy
    {
        [OperationContract]
        string Ping(string message);
    }
}