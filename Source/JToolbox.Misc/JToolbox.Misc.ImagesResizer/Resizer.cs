using JToolbox.Core.Extensions;
using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.IO;

namespace JToolbox.Misc.ImagesResizer
{
    public static class Resizer
    {
        public static List<OutputImage> Process(
            IEnumerable<InputImage> inputImages,
            string outputPath,
            string outputFileNameMask = null,
            Func<Exception, bool> onExceptionHandler = null)
        {
            var outputImages = new List<OutputImage>();
            foreach (InputImage inputImage in inputImages)
            {
                string inputFileName = Path.GetFileName(inputImage.InputFilePath);
                string outputFileName = GetOutputFileName(outputImages.Count + 1, inputFileName, outputFileNameMask);
                string outputFilePath = Path.Combine(outputPath, outputFileName);

                var outputImage = new OutputImage(inputImage)
                {
                    OutputFilePath = outputFilePath,
                };

                CreateDirectories(outputPath);

                try
                {
                    outputImage.ProcessImageResult
                        = MagicImageProcessor.ProcessImage(inputImage.InputFilePath, outputFilePath, inputImage.Settings);

                    outputImages.Add(outputImage);
                }
                catch (Exception ex) when (onExceptionHandler != null)
                {
                    bool cancel = onExceptionHandler(ex);
                    if (cancel) { return outputImages; }
                }
            }

            return outputImages;
        }

        private static void CreateDirectories(string outputPath)
        {
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
        }

        private static string GetOutputFileName(int index, string inputFileName, string outputFileNameMask)
        {
            if (string.IsNullOrEmpty(outputFileNameMask)) { return inputFileName; }

            string inputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFileName);
            string inputFileExtension = Path.GetExtension(inputFileName);

            var toReplace = new Dictionary<string, string>()
            {
                ["{index}"] = index.ToString(),
                ["{fileName}"] = inputFileName,
                ["{fileNameWithoutExtension}"] = inputFileNameWithoutExtension,
                ["{extension}"] = inputFileExtension
            };

            return outputFileNameMask.ReplaceMany(toReplace);
        }
    }
}