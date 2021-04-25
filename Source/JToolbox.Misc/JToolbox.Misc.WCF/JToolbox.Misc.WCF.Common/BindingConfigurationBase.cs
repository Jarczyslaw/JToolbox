using System.ServiceModel.Channels;

namespace JToolbox.Misc.WCF.Common
{
    public abstract class BindingConfigurationBase
    {
        public Binding Binding { get; protected set; }
        public string ApplicationName { get; set; }
        public string ServiceName { get; set; }
        public virtual string MachineAddress { get; protected set; }

        public abstract string BindingAddress { get; }

        public string ApplicationAddress
        {
            get
            {
                var result = BindingAddress + MachineAddress;
                if (!string.IsNullOrEmpty(ApplicationName))
                {
                    result += "/" + ApplicationName;
                }
                return result;
            }
        }

        public string ServiceAddress
        {
            get
            {
                var result = ApplicationAddress;
                if (!string.IsNullOrEmpty(ServiceName))
                {
                    result += "/" + ServiceName;
                }
                return result;
            }
        }
    }
}