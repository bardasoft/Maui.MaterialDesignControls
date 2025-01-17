﻿using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;

partial class CustomDatePickerHandler
{
    public static void MapBorder(IDatePickerHandler handler, IDatePicker picker)
    {
        handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
    }

    public static void MapHorizontalTextAlignment(IDatePickerHandler handler, IDatePicker picker) { }

    public static void MapIsFocused(IDatePickerHandler handler, IDatePicker datePicker){ }
}
