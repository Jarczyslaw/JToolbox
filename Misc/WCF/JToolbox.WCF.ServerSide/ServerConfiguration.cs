using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Description;

namespace JToolbox.WCF.ServerSide
{
    public class ServerConfiguration
    {
        public bool IncludeExceptionDetailInFaults { get; set; } = true;
        public List<IServiceBehavior> ServiceBehaviors { get; set; } = new List<IServiceBehavior>();

        public void AddMetadataBehaviour(string serviceAddress)
        {
            ServiceBehaviors.Add(new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                HttpGetUrl = new Uri(Path.Combine(serviceAddress, "wsdl"))
            });
        }
    }
}