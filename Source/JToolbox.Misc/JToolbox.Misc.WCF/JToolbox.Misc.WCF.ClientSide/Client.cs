using JToolbox.Misc.WCF.Common;
using System;
using System.ServiceModel;

namespace JToolbox.Misc.WCF.ClientSide
{
    public class Client<TProxy> : IDisposable
    {
        public Client(BindingConfigurationBase bindingConfiguration)
        {
            ChannelFactory = new ChannelFactory<TProxy>(bindingConfiguration.Binding, new EndpointAddress(bindingConfiguration.ServiceAddress));
        }

        public ChannelFactory<TProxy> ChannelFactory { get; }
        public bool IsConnected => ChannelFactory?.State == CommunicationState.Opened;
        public TProxy Proxy { get; private set; }

        public void Dispose()
        {
            Stop();
        }

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
    }
}