﻿using Android.Media;
using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Droid.Core;
using Prism;
using Prism.Ioc;

namespace XamarinPrismApp.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPaths, Paths>();
            containerRegistry.RegisterSingleton<IAppCore, AppCore>();
        }
    }
}