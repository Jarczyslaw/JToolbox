using System.ServiceModel.Channels;

namespace JToolbox.Misc.WCF.Common
{
    public abstract class BindingConfigurationBase
    {
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

        public string ApplicationName { get; set; }
        public Binding Binding { get; protected set; }
        public abstract string BindingAddress { get; }
        public virtual string MachineAddress { get; protected set; }

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

        public string ServiceName { get; set; }
    }
}