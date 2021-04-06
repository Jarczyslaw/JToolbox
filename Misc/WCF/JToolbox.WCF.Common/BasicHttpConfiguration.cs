using System.ServiceModel;

namespace JToolbox.WCF.Common
{
    public class BasicHttpConfiguration : BindingConfigurationBase
    {
        public BasicHttpConfiguration() : this(new BasicHttpBinding())
        {
        }

        public BasicHttpConfiguration(BasicHttpBinding binding)
        {
            Binding = binding;
            binding.MaxReceivedMessageSize =
                binding.MaxBufferSize = int.MaxValue;
        }

        public BasicHttpBinding BasicHttpBinding => Binding as BasicHttpBinding;

        public string IpAddress { get; set; }
        public int Port { get; set; }

        public override string MachineAddress { get => $"{IpAddress}:{Port}"; }
        public override string BindingAddress => "http://";
    }
}