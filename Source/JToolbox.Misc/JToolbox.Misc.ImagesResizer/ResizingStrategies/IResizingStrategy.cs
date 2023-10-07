using PhotoSauce.MagicScaler;

namespace JToolbox.Misc.ImagesResizer.ResizingStrategies
{
    public interface IResizingStrategy
    {
        ProcessImageSettings CreateProcessImageSettings(string filePath);
    }
}