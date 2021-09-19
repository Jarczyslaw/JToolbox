using System.ServiceModel;

namespace JToolbox.Misc.WCF.Common
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

        public override string BindingAddress => "http://";
        public string IpAddress { get; set; }
        public override string MachineAddress { get => $"{IpAddress}:{Port}"; }
        public int Port { get; set; }
    }
}