using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using JToolbox.XamarinForms.Droid.Core;
using Plugin.XF.AppInstallHelper;

namespace XamarinPrismApp.Droid
{
    [Activity(Label = "XamarinPrismApp",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/Splash",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : MainActivityBase
    {
        private readonly AppGlobalExceptionHandler globalExceptionHandler = new AppGlobalExceptionHandler();

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            SetTheme(Resource.Style.MainTheme);
            Initialize(bundle);
            base.OnCreate(bundle);

            globalExceptionHandler.Attach();
            Initialize(bundle);
            InstallationHelper.Init("com.jtsolutions.xamarinprismapp.fileprovider");

            LoadApplication(new App(new AndroidInitializer()));
        }
    }
}