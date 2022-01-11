using Fclp;
using System;

namespace AppPacker
{
    public class CommandLineParser
    {
        public Config Parse(string[] args)
        {
            var parser = new FluentCommandLineParser<Config>();

            parser.Setup(x => x.BuildOutputPath)
                .As('o', "outputPath")
                .Required();

            parser.Setup(x => x.IgnoreXmlFiles)
                .As("ixf");

            parser.Setup(x => x.IgnorePdbFiles)
                .As("ipf");

            parser.Setup(x => x.OutputFileNamePattern)
                .As("pattern");

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