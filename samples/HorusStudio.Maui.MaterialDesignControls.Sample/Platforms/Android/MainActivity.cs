﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace HorusStudio.Maui.MaterialDesignControls.Sample
{
    [Activity(Theme = "@style/MyAppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}