using JToolbox.Core.Utilities;
using JToolbox.Misc.ImagesResizer;
using JToolbox.Misc.ImagesResizer.ResizingStrategies;

namespace Examples.Desktop.ImagesResizer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            TestAllStrategies("lenna.png");
            TestAllStrategies("test.jpg");

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static bool OnException(Exception exc)
        {
            Console.WriteLine(exc.ToString());
            return true;
        }

        private static void TestAllStrategies(string fileName)
        {
            string appPath = ApplicationInfo.ApplicationPath;
            string outputPath = Path.Combine(appPath, "output");
            string inputFilePath = Path.Combine(appPath, fileName);

            Resizer.ResizeWith(inputFilePath, outputPath, new FixedHeight(200), "fixedHeight_{fileName}", OnException);
            Resizer.ResizeWith(inputFilePath, outputPath, new FixedHeight(800), "fixedHeight_upscale_{fileName}", OnException);
            Resizer.ResizeWith(inputFilePath, outputPath, new FixedWidth(200), "fixedWidth_{fileName}", OnException);
            Resizer.ResizeWith(inputFilePath, outputPath, new FixedWidthAndHeight(200, 300), "fixedWidthAndHeight_{fileName}", OnException);
            Resizer.ResizeWith(inputFilePath, outputPath, new PercentageScale(50), "percentageScale_{fileName}", OnException);
        }
    }
}