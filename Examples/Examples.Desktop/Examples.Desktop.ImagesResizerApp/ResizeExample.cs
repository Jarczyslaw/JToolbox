using Examples.Desktop.Base;
using JToolbox.Misc.ImagesResizer;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.Desktop.ImagesResizerApp
{
    public class ResizeExample : BaseExample
    {
        public override Task Run(IOutputInput outputInput)
        {
            string inputDirectory = outputInput.SelectDirectory("Input images");
            if (string.IsNullOrEmpty(inputDirectory)) { return Task.CompletedTask; }

            List<string> imagesFiles = Directory.EnumerateFiles(inputDirectory, "*.jpg", SearchOption.AllDirectories)
                .OrderBy(x => x)
                .ToList();

            if (imagesFiles.Count == 0)
            {
                outputInput.WriteLine("No images found");
                return Task.CompletedTask;
            }

            outputInput.WriteLine($"Found {imagesFiles.Count} images");

            string outputDirectory = outputInput.SelectDirectory("Output directory");
            if (string.IsNullOrEmpty(outputDirectory)) { return Task.CompletedTask; }

            string inputWidth = outputInput.Read("Insert width", "270");
            if (!int.TryParse(inputWidth, out int width) || width < 0) { return Task.CompletedTask; }

            string inputMask = outputInput.Read("Insert mask", "{fileNameWithoutExtension}_min.{extension}");
            if (string.IsNullOrEmpty(inputMask)) { return Task.CompletedTask; }

            outputInput.WriteLine("Converting");

            IEnumerable<InputImage> images = ResizerInputImagesFactory.ToResizeWithFixedWidth(width, imagesFiles);
            Resizer.Process(images, outputDirectory, inputMask, x =>
            {
                outputInput.WriteLine(x.Message);
                return true;
            });

            outputInput.WriteLine("Done");
            return Task.CompletedTask;
        }
    }
}