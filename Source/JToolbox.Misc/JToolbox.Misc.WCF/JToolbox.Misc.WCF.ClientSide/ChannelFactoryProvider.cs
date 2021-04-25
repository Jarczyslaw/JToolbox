using JToolbox.Misc.WCF.Common;
using System.Collections.Generic;
using System.ServiceModel;

namespace JToolbox.Misc.WCF.ClientSide
{
    public class ChannelFactoryProvider<TProxy>
    {
        private Dictionary<string, ChannelFactory<TProxy>> channelFactories = new Dictionary<string, ChannelFactory<TProxy>>();

        public ChannelFactory<TProxy> GetChannelFactory(BindingConfigurationBase bindingConfiguration)
        {
            if (channelFactories.TryGetValue(bindingConfiguration.ServiceAddress, out ChannelFactory<TProxy> channelFactory))
            {
                return channelFactory;
            }
            else
            {
                var newChannelFactory = new ChannelFactory<TProxy>(bindingConfiguration.Binding, new EndpointAddress(bindingConfiguration.ServiceAddress));
                channelFactories.Add(bindingConfiguration.ServiceAddress, newChannelFactory);
                return newChannelFactory;
            }
        }
    }
}