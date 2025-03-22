using System.ServiceModel;

namespace Examples.Desktop.AppStart.WCF
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string Ping(string message);
    }
}