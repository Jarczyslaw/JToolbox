using System.Collections.Generic;
using System.ServiceModel.Description;

namespace JToolbox.WCF.ServerSide
{
    public class ServerConfiguration
    {
        public bool IncludeExceptionDetailInFaults { get; set; } = true;
        public List<IServiceBehavior> ServiceBehaviors { get; set; } = new List<IServiceBehavior>();
    }
}