using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.Core.Utilities;
using JToolbox.Misc.ImagesResizer;

namespace Examples.Desktop.ImagesResizer
{
    internal static class Program
    {
        private static string AppPath => ApplicationInfo.ApplicationPath;

        private static string OutputPath => Path.Combine(AppPath, "output");

        private static void Main(string[] args)
        {
            if (Directory.Exists(OutputPath))
            {
                FileSystemHelper.DeleteDirectoryWithContent(OutputPath);
            }

            TestAllResizeModes("lenna.png");
            TestAllResizeModes("test.jpg");
            TestConvert("lenna.png");

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static bool OnException(Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return true;
        }

        private static void TestAllResizeModes(string fileName)
        {
            string inputFilePath = Path.Combine(AppPath, fileName);

            IEnumerable<InputImage> images = ResizerInputImagesFactory.ToResizeWithFixedHeight(200, inputFilePath);
            Resizer.Process(images, OutputPath, "fixedHeight_{fileName}", OnException);

            images = ResizerInputImagesFactory.ToResizeWithFixedHeight(800, inputFilePath);
            Resizer.Process(images, OutputPath, "fixedHeight_upscale_{fileName}", OnException);

            images = ResizerInputImagesFactory.ToResizeWithFixedWidth(200, inputFilePath);
            Resizer.Process(images, OutputPath, "fixedWidth_{fileName}", OnException);

            images = ResizerInputImagesFactory.ToResizeWithFixedWidthAndHeight(200, 300, inputFilePath);
            Resizer.Process(images, OutputPath, "fixedWidthAndHeight_{fileName}", OnException);

            images = ResizerInputImagesFactory.ToResizeWithPercentageScale(50, inputFilePath);
            Resizer.Process(images, OutputPath, "percentageScale_{fileName}", OnException);
        }

        private static void TestConvert(string fileName)
        {
            var inputFile = new InputImage
            {
                InputFilePath = Path.Combine(AppPath, fileName)
            };

            Resizer.Process(inputFile.AsList(), OutputPath, "convertedToJpg_{fileNameWithoutExtension}.jpg", OnException);
        }
    }
}