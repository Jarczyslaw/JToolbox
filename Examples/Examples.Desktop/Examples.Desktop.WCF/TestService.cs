using Examples.Desktop.Base;
using System.ServiceModel;

namespace Examples.Desktop.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TestService : ITestService
    {
        private readonly IOutputInput outputInput;

        public TestService(IOutputInput outputInput)
        {
            this.outputInput = outputInput;
        }

        public string Ping(string message)
        {
            outputInput.WriteLine("Message received: " + message);
            return $"Reply from server: {message.ToUpper()}";
        }
    }
}