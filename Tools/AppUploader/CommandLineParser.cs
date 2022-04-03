using Fclp;
using JToolbox.Core.Results;

namespace AppUploader
{
    public class CommandLineParser
    {
        public Result<UploadSettings> Parse(string[] args)
        {
            var parser = new FluentCommandLineParser<UploadSettings>();

            var result = parser.Parse(args);

            if (result.HasErrors)
            {
                return Result<UploadSettings>.AsError(result.ErrorText);
            }

            return new Result<UploadSettings>(parser.Object);
        }
    }
}