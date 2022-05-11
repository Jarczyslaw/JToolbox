using Fclp;
using JToolbox.Core.Models.Results;

namespace AppUploader
{
    public class CommandLineParser
    {
        public Result<UploadData> Parse(string[] args)
        {
            var parser = new FluentCommandLineParser<UploadData>();

            parser.Setup(x => x.Hostname)
               .As("hostName");

            parser.Setup(x => x.Username)
               .As("userName");

            parser.Setup(x => x.Password)
               .As("password");

            parser.Setup(x => x.Port)
               .As("port");

            parser.Setup(x => x.FilePath)
               .As("filePath");

            parser.Setup(x => x.TargetPath)
               .As("targetPath");

            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                return Result<UploadData>.AsError(result.ErrorText);
            }

            return new Result<UploadData>(parser.Object);
        }
    }
}