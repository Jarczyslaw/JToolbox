using JToolbox.WCF.Common;
using System;
using System.ServiceModel;

namespace JToolbox.WCF.ClientSide
{
    public class Client<TProxy> : IDisposable
    {
        private ChannelFactoryProvider<TProxy> channelFactoryProvider = new ChannelFactoryProvider<TProxy>();

        public Client(BindingConfigurationBase bindingConfiguration)
        {
            ChannelFactory = channelFactoryProvider.GetChannelFactory(bindingConfiguration);
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