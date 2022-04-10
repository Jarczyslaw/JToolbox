using System.ServiceModel;

namespace JToolbox.Misc.WCF.Common
{
    public class BasicHttpsConfiguration : BindingConfigurationBase
    {
        public BasicHttpsConfiguration() : this(new BasicHttpsBinding(BasicHttpsSecurityMode.Transport))
        {
        }

        public BasicHttpsConfiguration(BasicHttpsBinding binding)
        {
            Binding = binding;
            binding.MaxReceivedMessageSize =
                binding.MaxBufferSize = int.MaxValue;
        }

        public BasicHttpsBinding BasicHttpsBinding => Binding as BasicHttpsBinding;

        public override string BindingAddress => "https://";
        public string IpAddress { get; set; }
        public override string MachineAddress { get => $"{IpAddress}:{Port}"; }
        public int Port { get; set; }
    }
}