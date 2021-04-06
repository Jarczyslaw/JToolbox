using JToolbox.WCF.Common;
using System;
using System.ServiceModel;

namespace JToolbox.WCF.ServerSide
{
    public class Server : IDisposable
    {
        public static Server CreateMultiple<TProxy>(BindingConfigurationBase bindingConfiguration, Type proxyImplType, ServerConfiguration serverConfiguration = null)
        {
            var server = new Server
            {
                Host = new ServiceHost(proxyImplType, new Uri(bindingConfiguration.ApplicationAddress))
            };
            server.Initialize(bindingConfiguration, typeof(TProxy), proxyImplType, serverConfiguration);
            return server;
        }

        public static Server CreateMultiple<TProxy, TProxyImpl>(BindingConfigurationBase bindingConfiguration)
        {
            return CreateMultiple<TProxy>(bindingConfiguration, typeof(TProxyImpl));
        }

        public static Server CreateSingle<TProxy>(BindingConfigurationBase bindingConfiguration, TProxy proxyInstance, ServerConfiguration serverConfiguration = null)
        {
            var server = new Server
            {
                Host = new ServiceHost(proxyInstance, new Uri(bindingConfiguration.ApplicationAddress))
            };
            server.Initialize(bindingConfiguration, typeof(TProxy), proxyInstance.GetType(), serverConfiguration);
            return server;
        }

        public ServiceHost Host { get; private set; }
        public Type ProxyType { get; private set; }
        public Type ProxyImplType { get; private set; }
        public bool IsListening => Host?.State == CommunicationState.Opened;

        public void Start()
        {
            Host.Open();
        }

        public void Stop()
        {
            if (IsListening)
            {
                try
                {
                    Host.Close();
                }
                catch
                {
                    Host.Abort();
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }

        private void Initialize(BindingConfigurationBase bindingConfiguration, Type proxyType, Type proxyImplType, ServerConfiguration serverConfiguration)
        {
            Host.AddServiceEndpoint(proxyType, bindingConfiguration.Binding, bindingConfiguration.ServiceName);
            ProxyType = proxyType;
            ProxyImplType = proxyImplType;

            if (serverConfiguration != null)
            {
                if (serverConfiguration.IncludeExceptionDetailInFaults)
                {
                    ((ServiceBehaviorAttribute)Host.Description.Behaviors[typeof(ServiceBehaviorAttribute)]).IncludeExceptionDetailInFaults = true;
                }
                foreach (var behavior in serverConfiguration.ServiceBehaviors)
                {
                    Host.Description.Behaviors.Add(behavior);
                }
            }
        }
    }
}