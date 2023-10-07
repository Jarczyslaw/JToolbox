using PhotoSauce.MagicScaler;

namespace JToolbox.Misc.ImagesResizer.ResizingStrategies
{
    public class FixedWidth : IResizingStrategy
    {
        private readonly int _width;

        public FixedWidth(int width)
        {
            _width = width;
        }

        public ProcessImageSettings CreateProcessImageSettings(string filePath)
        {
            return new ProcessImageSettings
            {
                Width = _width,
                ResizeMode = CropScaleMode.Contain
            };
        }
    }
}