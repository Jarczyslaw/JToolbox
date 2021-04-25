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

        public NetTcpBinding NetTcpBinding => Binding as NetTcpBinding;

        public string IpAddress { get; set; }
        public int Port { get; set; }

        public override string MachineAddress { get => $"{IpAddress}:{Port}"; }
        public override string BindingAddress => "net.tcp://";
    }
}