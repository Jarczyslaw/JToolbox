using System.ServiceModel;

namespace Examples.Desktop.WCF
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string Ping(string message);
    }
}