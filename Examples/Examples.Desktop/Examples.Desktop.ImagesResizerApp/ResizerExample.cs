using Examples.Desktop.Base;
using JToolbox.Misc.ImagesResizer;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.Desktop.ImagesResizerApp
{
    public class ResizerExample : BaseExample
    {
        public override Task Run(IOutputInput outputInput)
        {
            string inputDirectory = outputInput.SelectDirectory("Input images");
            if (string.IsNullOrEmpty(inputDirectory)) { return Task.CompletedTask; }

            List<string> imagesFiles = Directory.EnumerateFiles(inputDirectory, "*.*", SearchOption.AllDirectories)
                .Where(x => x.EndsWith(".jpeg") || x.EndsWith(".jpg"))
                .ToList();

            if (imagesFiles.Count == 0)
            {
                outputInput.WriteLine("No images found");
                return Task.CompletedTask;
            }

            outputInput.WriteLine($"Found {imagesFiles.Count} images");

            string outputDirectory = outputInput.SelectDirectory("Output directory");
            if (string.IsNullOrEmpty(outputDirectory)) { return Task.CompletedTask; }

            outputInput.WriteLine("Converting");

            IEnumerable<InputImage> images = ResizerInputImagesFactory.ToResizeWithFixedWidth(270, imagesFiles);
            Resizer.Process(images, outputDirectory, "{fileNameWithoutExtension}_min.jpg", x =>
            {
                outputInput.WriteLine(x.Message);
                return true;
            });

            outputInput.WriteLine("Done");
            return Task.CompletedTask;
        }
    }
}