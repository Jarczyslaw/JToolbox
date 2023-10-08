using PhotoSauce.MagicScaler;

namespace JToolbox.Misc.ImagesResizer
{
    public class InputImage
    {
        public string InputFilePath { get; set; }

        public ProcessImageSettings Settings { get; set; } = new ProcessImageSettings();
    }
}