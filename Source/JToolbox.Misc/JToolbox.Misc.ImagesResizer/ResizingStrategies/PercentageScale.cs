using PhotoSauce.MagicScaler;
using System;

namespace JToolbox.Misc.ImagesResizer.ResizingStrategies
{
    public class PercentageScale : IResizingStrategy
    {
        private readonly float _percentage;

        public PercentageScale(float percentage)
        {
            _percentage = percentage;
        }

        public ProcessImageSettings CreateProcessImageSettings(string filePath)
        {
            IPixelSource pixelSource
                = MagicImageProcessor.BuildPipeline(filePath, new ProcessImageSettings()).PixelSource;

            return new ProcessImageSettings
            {
                Width = GetDimension(pixelSource.Width),
                Height = GetDimension(pixelSource.Height),
                ResizeMode = CropScaleMode.Stretch
            };
        }

        private int GetDimension(int value)
        {
            if (_percentage <= 0f) { return value; }

            return (int)Math.Ceiling(value * _percentage / 100f);
        }
    }
}