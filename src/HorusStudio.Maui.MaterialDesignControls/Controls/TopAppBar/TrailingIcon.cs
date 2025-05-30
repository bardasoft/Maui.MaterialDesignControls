﻿using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public class TrailingIcon : BindableObject
    {
        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="Icon" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(TrailingIcon), null);

        /// <summary>
        /// The backing store for the <see cref="Command" /> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TrailingIcon), null);

        /// <summary>
        /// The backing store for the <see cref="IsBusy" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(TrailingIcon), false);

        /// <summary>
        /// The backing store for the <see cref="IsVisible" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(TrailingIcon), true);

        /// <summary>
        /// The backing store for the <see cref="IsEnabled" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(TrailingIcon), true);

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Allows you to display an icon button on the right side of the top app bar. This is a bindable property.
        /// </summary>
        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// Gets or sets the command to invoke when the trailing icon button is clicked. This is a bindable property.
        /// </summary>
        /// <remarks>This property is used to associate a command with an instance of a top app bar. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.</remarks>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets if the trailing icon button is on busy state (executing Command).
        /// The default value is <see langword="false"/>.
        /// This is a bindable property.
        /// </summary>
        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        /// <summary>
        /// Gets or sets if the trailing icon button is visible or hidden.
        /// The default value is <see langword="true"/>.
        /// This is a bindable property.
        /// </summary>
        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets if the trailing icon button is in an enabled or disabled state.
        /// The default value is <see langword="true"/>.
        /// This is a bindable property.
        /// </summary>
        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

#nullable enable
        internal int? Index { get; set; }
#nullable disable

        #endregion Properties
    }
}