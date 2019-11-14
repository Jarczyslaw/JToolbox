using JToolbox.Desktop.Core;
using System.Drawing;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Extensions
{
    public static class ScreenCaptureExtensions
    {
        public static Bitmap CapturePrimaryScreen(this ScreenCapture screenCapture)
        {
            return screenCapture.Capture(Screen.PrimaryScreen.Bounds);
        }

        public static Bitmap CaptureAllScreens(this ScreenCapture screenCapture)
        {
            return screenCapture.Capture(SystemInformation.VirtualScreen);
        }
    }
}