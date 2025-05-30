﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class RadioButtonViewModel : BaseViewModel
{
    #region Attributes & Properties

    public override string Title => Models.Pages.RadioButton;
    protected override string ControlReferenceUrl => "components/radio-button/overview";
    
    [ObservableProperty]
    private ObservableCollection<CustomColor> _itemsSourceColors;

    [ObservableProperty]
    private CustomColor _checkedColor;

    [ObservableProperty]
    private bool _isRadioButtonEnabled;

    #endregion

    public RadioButtonViewModel()
    {
        Subtitle = "Radio buttons let people select one option from a set of options";

        ItemsSourceColors = new ObservableCollection<CustomColor>
            {
                new CustomColor()
                {
                    Color = "Red",
                    Id = 1
                },
                new CustomColor()
                {
                    Color = "Blue",
                    Id = 2
                },
                new CustomColor()
                {
                    Color = "Green",
                    Id = 3
                }
            };

        CheckedColor = ItemsSourceColors.FirstOrDefault();
    }

    [ICommand]
    private async Task CheckChanged()
    {
        await DisplayAlert(Title, CheckedColor.Color ?? "none", "OK");
    }

    [ICommand]
    private async Task CheckedChanged(object message)
    {
        await DisplayAlert(Title + " from Command", message.ToString(), "OK");
    }
}

public class CustomColor
{
    public int Id { get; set; }

    public string Color { get; set; }

    // We override this method only to show a Custom Object without set ItemDisplayPath in Full API example.
    public override string ToString()
    {
        return $"{Id} - {Color}";
    }

    public override bool Equals(object obj)
    {
        if(obj is CustomColor color)
        {
            return Color.Equals(color.Color);
        }
        return false;
    }
}
