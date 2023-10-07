using PhotoSauce.MagicScaler;

namespace JToolbox.Misc.ImagesResizer.ResizingStrategies
{
    public class FixedWidthAndHeight : IResizingStrategy
    {
        private readonly int _height;
        private readonly int _width;

        public FixedWidthAndHeight(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public ProcessImageSettings CreateProcessImageSettings(string filePath)
        {
            return new ProcessImageSettings
            {
                Width = _width,
                Height = _height,
                ResizeMode = CropScaleMode.Stretch
            };
        }
    }
}