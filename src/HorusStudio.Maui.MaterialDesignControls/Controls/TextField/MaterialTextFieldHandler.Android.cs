﻿using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Android.OS;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class MaterialTextFieldHandler
{
    public static void MapBorder(IEntryHandler handler, IEntry entry)
    {
        handler.PlatformView.Background = null;
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
    }

    public static void MapCursorColor(IEntryHandler handler, IEntry entry)
    {
        if (entry is CustomEntry customEntry && customEntry.CursorColor != null && handler.PlatformView is Android.Widget.EditText editText)
        {
            BuildVersionCodes androidVersion = Build.VERSION.SdkInt;
            if (androidVersion >= BuildVersionCodes.Q)
            {
                editText.TextCursorDrawable.SetTint(customEntry.CursorColor.ToPlatform());
            }
        }
    }
}