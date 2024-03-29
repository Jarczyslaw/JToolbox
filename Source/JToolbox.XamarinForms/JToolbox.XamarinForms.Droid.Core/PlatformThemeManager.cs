﻿using JToolbox.XamarinForms.Core.Extensions;
using JToolbox.XamarinForms.Droid.Core.Extensions;
using JToolbox.XamarinForms.Themes;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace JToolbox.XamarinForms.Droid.Core
{
    public class PlatformThemeManager : IPlatformThemeManager
    {
        private readonly FormsAppCompatActivity activity = CrossCurrentActivity.Current.Activity as FormsAppCompatActivity;

        public void SetTheme(IThemeResourceDictionary themeResourceDictionary)
        {
            SetStatusBar(themeResourceDictionary.ThemeColorExtractor.SecondaryColor);
        }

        private void SetStatusBar(Color color)
        {
            activity.SetStatusBarColor(color.ToAndroid());
            activity.SetLightStatusBar(color.IsLight());
        }
    }
}