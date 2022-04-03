using Fclp;
using System;

namespace AppZipper
{
    public class CommandLineParser
    {
        public Config Parse(string[] args)
        {
            var parser = new FluentCommandLineParser<Config>();

            parser.Setup(x => x.AssemblyFolderPath)
                .As("assemblyFolderPath")
                .Required();

            parser.Setup(x => x.FilesWhiteList)
                .As("filesWhiteList");

            parser.Setup(x => x.FilesBlackList)
                .As("filesBlackList");

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