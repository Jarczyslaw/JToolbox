using System.ServiceModel;

namespace JToolbox.Misc.WCF.Common
{
    public class NetTcpConfiguration : BindingConfigurationBase
    {
        public NetTcpConfiguration() : this(new NetTcpBinding())
        {
        }

        public NetTcpConfiguration(NetTcpBinding binding)
        {
            Binding = binding;
            binding.MaxReceivedMessageSize =
                binding.MaxBufferSize = int.MaxValue;
        }

        public override string BindingAddress => "net.tcp://";
        public string IpAddress { get; set; }
        public override string MachineAddress { get => $"{IpAddress}:{Port}"; }
        public NetTcpBinding NetTcpBinding => Binding as NetTcpBinding;
        public int Port { get; set; }
    }
}