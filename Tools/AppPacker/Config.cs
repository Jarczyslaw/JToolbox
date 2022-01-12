namespace AppPacker
{
    public class Config
    {
        public string AssemblyFolderPath { get; set; }
        public bool IgnoreExeConfigFile { get; set; } = false;
        public bool IgnorePdbFiles { get; set; } = true;
        public bool IgnoreXmlFiles { get; set; } = true;
        public string OutputFileNamePattern { get; set; } = "{assemblyName}_{version}";
    }
}