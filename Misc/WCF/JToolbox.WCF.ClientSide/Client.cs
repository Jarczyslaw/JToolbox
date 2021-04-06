using JToolbox.WCF.Common;
using System;
using System.ServiceModel;

namespace JToolbox.WCF.ClientSide
{
    public class Client<TProxy> : IDisposable
    {
        public Client(BindingConfigurationBase bindingConfiguration)
        {
            ChannelFactory = new ChannelFactory<TProxy>(bindingConfiguration.Binding, new EndpointAddress(bindingConfiguration.ServiceAddress));
        }

        public TProxy Proxy { get; private set; }
        public ChannelFactory<TProxy> ChannelFactory { get; }
        public bool IsConnected => ChannelFactory?.State == CommunicationState.Opened;

        public void Start()
        {
            Proxy = ChannelFactory.CreateChannel();
        }

        public void Stop()
        {
            if (IsConnected)
            {
                try
                {
                    ChannelFactory.Close();
                }
                catch
                {
                    ChannelFactory.Abort();
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}