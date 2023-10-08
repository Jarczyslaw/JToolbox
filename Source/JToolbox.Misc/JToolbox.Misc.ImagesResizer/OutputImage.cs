using PhotoSauce.MagicScaler;

namespace JToolbox.Misc.ImagesResizer
{
    public class OutputImage
    {
        public OutputImage(InputImage inputImage)
        {
            InputFilePath = inputImage.InputFilePath;
        }

        public string InputFilePath { get; internal set; }

        public string OutputFilePath { get; internal set; }

        public ProcessImageResult ProcessImageResult { get; internal set; }
    }
}