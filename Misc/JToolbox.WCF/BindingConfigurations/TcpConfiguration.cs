using System.ServiceModel;

namespace JToolbox.WCF.BindingConfigurations
{
    public class TcpConfiguration : BindingConfiguration
    {
        public TcpConfiguration()
        {
            Binding = new NetTcpBinding();
        }

        public string IpAddress { get; set; }
        public int Port { get; set; }

        public override string MachineAddress { get => $"{IpAddress}:{Port}"; }
        public override string BindingAddress => "net.tcp://";
    }
}