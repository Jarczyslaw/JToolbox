using Examples.Desktop.Base;
using JToolbox.Core.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.Desktop.ImagesResizerApp
{
    public class JpegToJpgExample : BaseExample
    {
        public override Task Run(IOutputInput outputInput)
        {
            string inputDirectory = outputInput.SelectDirectory("Input images");
            if (string.IsNullOrEmpty(inputDirectory)) { return Task.CompletedTask; }

            List<string> imagesFiles = Directory.EnumerateFiles(inputDirectory, "*.jpeg", SearchOption.AllDirectories)
                .OrderBy(x => x)
                .ToList();

            int index = 1;
            foreach (string imageFile in imagesFiles)
            {
                FileSystemHelper.RenameFile(imageFile, $"{index}.jpg");
                index++;
            }

            outputInput.WriteLine($"Changed {imagesFiles.Count} files");
            return Task.CompletedTask;
        }
    }
}