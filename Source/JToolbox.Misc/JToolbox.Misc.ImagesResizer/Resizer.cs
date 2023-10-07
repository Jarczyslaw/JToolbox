using JToolbox.Core.Extensions;
using JToolbox.Misc.ImagesResizer.ResizingStrategies;
using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.IO;

namespace JToolbox.Misc.ImagesResizer
{
    public static class Resizer
    {
        public static List<ImagesPair> ResizeWith(
            string filePath,
            string outputPath,
            IResizingStrategy resizingStrategy,
            string outputFileNameMask = null,
            Func<Exception, bool> onExceptionHandler = null)
        {
            return ResizeWith(filePath.AsList(), outputPath, resizingStrategy, outputFileNameMask, onExceptionHandler);
        }

        public static List<ImagesPair> ResizeWith(
            IEnumerable<string> filePaths,
            string outputPath,
            IResizingStrategy resizingStrategy,
            string outputFileNameMask = null,
            Func<Exception, bool> onExceptionHandler = null)
        {
            var imagesPairs = new List<ImagesPair>();
            foreach (string inputFilePath in filePaths)
            {
                string inputFileName = Path.GetFileName(inputFilePath);
                string outputFileName = GetOutputFileName(imagesPairs.Count + 1, inputFileName, outputFileNameMask);
                string outputFilePath = Path.Combine(outputPath, outputFileName);

                var imagePair = new ImagesPair()
                {
                    InputFileName = inputFilePath,
                    OutputFileName = outputFilePath,
                };

                CreateDirectories(outputPath);

                try
                {
                    ProcessImageSettings settings = resizingStrategy.CreateProcessImageSettings(inputFilePath);

                    imagePair.ProcessImageResult
                        = MagicImageProcessor.ProcessImage(inputFilePath, outputFilePath, settings);

                    imagesPairs.Add(imagePair);
                }
                catch (Exception ex) when (onExceptionHandler != null)
                {
                    bool cancel = onExceptionHandler(ex);
                    if (cancel) { return imagesPairs; }
                }
            }

            return imagesPairs;
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