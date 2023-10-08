using JToolbox.Core.Extensions;
using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Misc.ImagesResizer
{
    public static class ResizerInputImagesFactory
    {
        public static IEnumerable<InputImage> ToProcess(ProcessImageSettings settings, IEnumerable<string> filePaths)
        {
            return filePaths.Select(x => new InputImage
            {
                InputFilePath = x,
                Settings = settings
            });
        }

        public static IEnumerable<InputImage> ToResizeWithFixedHeight(int height, string filePath)
            => ToResizeWithFixedHeight(height, filePath.AsList());

        public static IEnumerable<InputImage> ToResizeWithFixedHeight(int height, IEnumerable<string> filePaths)
        {
            var settings = new ProcessImageSettings
            {
                Height = height,
                ResizeMode = CropScaleMode.Contain
            };
            return ToProcess(settings, filePaths);
        }

        public static IEnumerable<InputImage> ToResizeWithFixedWidth(int width, string filePath)
            => ToResizeWithFixedWidth(width, filePath.AsList());

        public static IEnumerable<InputImage> ToResizeWithFixedWidth(int width, IEnumerable<string> filePaths)
        {
            var settings = new ProcessImageSettings
            {
                Width = width,
                ResizeMode = CropScaleMode.Contain
            };
            return ToProcess(settings, filePaths);
        }

        public static IEnumerable<InputImage> ToResizeWithFixedWidthAndHeight(int width, int height, string filePath)
            => ToResizeWithFixedWidthAndHeight(width, height, filePath.AsList());

        public static IEnumerable<InputImage> ToResizeWithFixedWidthAndHeight(int width, int height, IEnumerable<string> filePaths)
        {
            var settings = new ProcessImageSettings
            {
                Width = width,
                Height = height,
                ResizeMode = CropScaleMode.Stretch
            };
            return ToProcess(settings, filePaths);
        }

        public static IEnumerable<InputImage> ToResizeWithPercentageScale(float percentage, string filePath)
            => ToResizeWithPercentageScale(percentage, filePath.AsList());

        public static IEnumerable<InputImage> ToResizeWithPercentageScale(float percentage, IEnumerable<string> filePaths)
        {
            var result = new List<InputImage>();

            foreach (string filePath in filePaths)
            {
                IPixelSource pixelSource
                    = MagicImageProcessor.BuildPipeline(filePath, new ProcessImageSettings()).PixelSource;

                var settings = new ProcessImageSettings
                {
                    Width = GetDimension(percentage, pixelSource.Width),
                    Height = GetDimension(percentage, pixelSource.Height),
                    ResizeMode = CropScaleMode.Stretch
                };

                result.Add(new InputImage
                {
                    InputFilePath = filePath,
                    Settings = settings
                });
            }

            return result;
        }

        private static int GetDimension(float percentage, int value)
        {
            if (percentage <= 0f) { return value; }

            return (int)Math.Ceiling(value * percentage / 100f);
        }
    }
}