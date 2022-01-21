using Fclp;
using System;

namespace AppPacker
{
    public class CommandLineParser
    {
        public Config Parse(string[] args)
        {
            var parser = new FluentCommandLineParser<Config>();

            parser.Setup(x => x.AssemblyFolderPath)
                .As("assemblyFolderPath")
                .Required();

            parser.Setup(x => x.IgnoreXmlFiles)
                .As("ignoreXmlFiles");

            parser.Setup(x => x.IgnorePdbFiles)
                .As("ignorePdbFiles");

            parser.Setup(x => x.IgnoreExeConfigFile)
                .As("ignoreExeConfigFile");

            parser.Setup(x => x.OutputFileNamePattern)
                .As("outputFileNamePattern");

            parser.Setup(x => x.RemovePreviousPackages)
                .As("removePreviousPackages");

            parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(text));

            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                Console.WriteLine(result.ErrorText);
                return null;
            }

            return parser.Object;
        }
    }
}