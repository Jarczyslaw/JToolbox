using JToolbox.Misc.Configuration;

namespace Examples.Desktop.Configuration
{
    public class Config : BaseConfiguration
    {
        public bool GetBoolValue => GetBool("boolValue");
        public Dictionary<string, Entry> GetDictValue => GetDictionary<string, Entry>("dictValue");
        public int GetIntValue => GetInt("intValue");

        public List<Entry> GetListValue => GetList<Entry>("listValue");
        protected override string GetConfigurationFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
    }
}