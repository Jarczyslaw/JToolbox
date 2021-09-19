using System.ServiceModel;

namespace JToolbox.Misc.WCF.Common
{
    public class NamedPipeConfiguration : BindingConfigurationBase
    {
        public NamedPipeConfiguration() : this(new NetNamedPipeBinding())
        {
        }

        public NamedPipeConfiguration(NetNamedPipeBinding binding)
        {
            MachineAddress = "localhost";
            Binding = binding;
            binding.MaxReceivedMessageSize =
                binding.MaxBufferSize = int.MaxValue;
        }

        public override string BindingAddress => "net.pipe://";
        public NetNamedPipeBinding NetNamedPipeBinding => Binding as NetNamedPipeBinding;
    }
}