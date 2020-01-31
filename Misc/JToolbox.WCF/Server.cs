using JToolbox.WCF.BindingConfigurations;
using System;
using System.ServiceModel;

namespace JToolbox.WCF
{
    public class Server : IDisposable
    {
        public static Server CreateMultiple<TProxy>(BindingConfiguration bindingConfiguration, Type proxyImplType)
            where TProxy : IProxy
        {
            var server = new Server
            {
                Host = new ServiceHost(proxyImplType, new Uri(bindingConfiguration.ApplicationAddress))
            };
            server.Initialize(bindingConfiguration, typeof(TProxy), proxyImplType);
            return server;
        }

        public static Server CreateMultiple<TProxy, TProxyImpl>(BindingConfiguration bindingConfiguration)
            where TProxy : IProxy
        {
            return CreateMultiple<TProxy>(bindingConfiguration, typeof(TProxyImpl));
        }

        public static Server CreateSingle<TProxy>(BindingConfiguration bindingConfiguration, TProxy proxyInstance)
            where TProxy : IProxy
        {
            var server = new Server
            {
                Host = new ServiceHost(proxyInstance, new Uri(bindingConfiguration.ApplicationAddress))
            };
            server.Initialize(bindingConfiguration, typeof(TProxy), proxyInstance.GetType());
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

        private void Initialize(BindingConfiguration bindingConfiguration, Type proxyType, Type proxyImplType)
        {
            Host.AddServiceEndpoint(proxyType, bindingConfiguration.Binding, bindingConfiguration.ServiceName);
            ProxyType = proxyType;
            ProxyImplType = proxyImplType;
        }
    }
}