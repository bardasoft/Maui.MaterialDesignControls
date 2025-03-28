﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class TextFieldViewModel : BaseViewModel
    {
        #region Attributes & Properties

        public override string Title => Models.Pages.TextField;
        protected override string ControlReferenceUrl => "components/text-fields/overview";

        [ObservableProperty]
        private string _supportingTextValue = "Enter the value.";

        [ObservableProperty]
        private string _text = "";

        [ObservableProperty]
        private bool _hasAnError;
        
        [ObservableProperty]
        private bool _fixedLabel;

        #endregion

        public TextFieldViewModel()
        {
            Subtitle = "Text fields let people enter text into a UI. They typically appear in forms and dialogs.";
        }

        [ICommand]
        private void CheckTextField()
        {
            SupportingTextValue = "Enter the value.";
            HasAnError = false;

            if (string.IsNullOrWhiteSpace(Text))
            {
                SupportingTextValue = "You should enter a valid value.";
                HasAnError = true;
            }
        }

        [ICommand]
        private void LeadingAction()
        {
            DisplayAlert("Leading icon", "Command for leading icon.", "OK");
        }
        
        [ICommand]
        private void TrailingAction()
        {
            DisplayAlert("Trailing icon", "Command for trailing icon.", "OK");
        }

        [ICommand]
        private void Return()
        {
            DisplayAlert("Return", "Command for return type.", "OK");
        }
    }
}

