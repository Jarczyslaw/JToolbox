using PhotoSauce.MagicScaler;

namespace JToolbox.Misc.ImagesResizer.ResizingStrategies
{
    public class FixedHeight : IResizingStrategy
    {
        private readonly int _height;

        public FixedHeight(int height)
        {
            _height = height;
        }

        public ProcessImageSettings CreateProcessImageSettings(string filePath)
        {
            return new ProcessImageSettings
            {
                Height = _height,
                ResizeMode = CropScaleMode.Contain
            };
        }
    }
}