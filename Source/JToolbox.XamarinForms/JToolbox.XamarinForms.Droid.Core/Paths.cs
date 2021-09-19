using Android.App;
using JToolbox.XamarinForms.Core.Abstraction;

namespace JToolbox.XamarinForms.Droid.Core
{
    public class Paths : IPaths
    {
        public string InternalFolder => Application.Context.FilesDir.AbsolutePath;

        public string PrivateExternalFolder => Application.Context.GetExternalFilesDir(null).AbsolutePath;
        public string PublicExternalFolder => Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
    }
}