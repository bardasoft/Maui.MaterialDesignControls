﻿using HorusStudio.Maui.MaterialDesignControls.Converters;
using Microsoft.Maui.Controls.Shapes;
using System.Runtime.CompilerServices;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Ratings provide insight regarding others' opinions and experiences, and can allow the user to submit a rating of their own.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialRating.jpg</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialRating
///         Label="How do you rate....?"
///         Value="1"
///         Animation="Scale"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var MaterialRating = new MaterialRating()
/// {
///     Label = "How do you rate....?",
///     Value = 1,
///     Animation = AnimationTypes.Scale
/// };
/// </code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/RatingPage.xaml)
/// 
/// </example>
public class MaterialRating : ContentView
{
    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultStrokeColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private const double DefaultStrokeThickness = 2.0;
    private const int DefaultItemsSize = 5;
    private const int DefaultItemsPerRow = 5;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultCharacterSpacing = _ => MaterialFontTracking.BodyLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontSize = _ => MaterialFontSize.BodyLarge;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationType = _ => MaterialAnimation.Type;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationParameter = _ => MaterialAnimation.Parameter;

    #endregion Attributes

    #region Layout

    private readonly Grid _containerLayout;

    #endregion Layout

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Label" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="LabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialRating), defaultValueCreator: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialRating), defaultValueCreator: DefaultFontFamily);
    
    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialRating), defaultValueCreator: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialRating), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialRating), defaultValueCreator: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="LabelTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTransformProperty = BindableProperty.Create(nameof(LabelTransform), typeof(TextTransform), typeof(MaterialRating), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialRating), defaultValue: true, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is MaterialRating self && newValue is bool)
        {
            self.ChangeVisualState();
            if(self._containerLayout is not null)
            {
                self.SetGridContent();
            }
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialRating), defaultValueCreator: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialRating), defaultValueCreator: DefaultAnimationParameter);

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialRating));

    /// <summary>
    /// The backing store for the <see cref="StrokeColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(MaterialRating), defaultValueCreator: DefaultStrokeColor);

    /// <summary>
    /// The backing store for the <see cref="StrokeThickness" /> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(MaterialRating), defaultValue: DefaultStrokeThickness);

    /// <summary>
    /// The backing store for the <see cref="SelectedIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIconSourceProperty = BindableProperty.Create(nameof(SelectedIconSource), typeof(ImageSource), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="UnselectedIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty UnselectedIconSourceProperty = BindableProperty.Create(nameof(UnselectedIconSource), typeof(ImageSource), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="SelectedIconsSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIconsSourceProperty = BindableProperty.Create(nameof(SelectedIconsSource), typeof(IEnumerable<ImageSource>), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="UnselectedIconsSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty UnselectedIconsSourceProperty = BindableProperty.Create(nameof(UnselectedIconsSource), typeof(IEnumerable<ImageSource>), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="ItemsSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSizeProperty = BindableProperty.Create(nameof(ItemsSize), typeof(int), typeof(MaterialRating), defaultValue: DefaultItemsSize);

    /// <summary>
    /// The backing store for the <see cref="ItemsPerRow" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsPerRowProperty = BindableProperty.Create(nameof(ItemsPerRow), typeof(int), typeof(MaterialRating), defaultValue: DefaultItemsPerRow);

    /// <summary>
    /// The backing store for the <see cref="Value"/> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(MaterialRating), defaultBindingMode: BindingMode.TwoWay, defaultValue: 0, propertyChanged: (bindableObject, _, newValue) =>
    {
        if (bindableObject is MaterialRating self)
        {
            self.OnValuePropertyChanged(self, newValue);
        }
    });

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the <see cref="Label" /> for the label.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Null"/>
    /// </default>
    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelColor" /> for the text of the label.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Text">MaterialLightTheme.Text</see> - Dark: <see cref="MaterialDarkTheme.Text">MaterialDarkTheme.Text</see>
    /// </default>
    public Color LabelColor
    {
        get => (Color)GetValue(LabelColorProperty);
        set => SetValue(LabelColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font family for the label.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the label.
    /// This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the text style of the label.
    /// This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system.
    /// The default value of this property is true
    /// </summary>
    /// <default>
    /// <see langword="True"/>
    /// </default>
    public bool FontAutoScalingEnabled
    {
        get => (bool)GetValue(FontAutoScalingEnabledProperty);
        set => SetValue(FontAutoScalingEnabledProperty, value);
    }

    /// <summary>
    /// Defines the font size of the label. This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodyLarge">MaterialFontSize.BodyLarge</see> - Tablet = 19 / Phone = 16
    /// </default>
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Defines the casing of the label.
    /// This is a bindable property.
    /// </summary>
    public TextTransform LabelTransform
    {
        get => (TextTransform)GetValue(LabelTransformProperty);
        set => SetValue(LabelTransformProperty, value);
    }

    /// <summary>
    /// Gets or sets <see cref="IsEnabled" /> for the rating control.
    /// This is a bindable property.
    /// </summary>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when an icon is clicked
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="AnimationTypes.Fade">AnimationTypes.Fade</see>
    /// </default>
    public AnimationTypes Animation
    {
        get => (AnimationTypes)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public double? AnimationParameter
    {
        get => (double?)GetValue(AnimationParameterProperty);
        set => SetValue(AnimationParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom animation to be executed when a icon is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the stroke of default start.
    /// This is a bindable property.
    /// </summary>
    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="StrokeThickness" /> for the default start.
    /// This is a bindable property.
    /// </summary>
    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the rating when is selected.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// For more options have a look at <see cref="ImageButton">ImageButton</see>.
    /// </remarks>
    public ImageSource SelectedIconSource
    {
        get => (ImageSource)GetValue(SelectedIconSourceProperty);
        set => SetValue(SelectedIconSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the rating when is unselected.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// For more options have a look at <see cref="ImageButton">ImageButton</see>.
    /// </remarks>
    public ImageSource UnselectedIconSource
    {
        get => (ImageSource)GetValue(UnselectedIconSourceProperty);
        set => SetValue(UnselectedIconSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image diferent on each rating when is selected.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// For more options have a look at <see cref="ImageButton">ImageButton</see>.
    /// </remarks>
    public IEnumerable<ImageSource> SelectedIconsSource
    {
        get => (IEnumerable<ImageSource>)GetValue(SelectedIconsSourceProperty);
        set => SetValue(SelectedIconsSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image diferent on each rating when is unselected.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// For more options have a look at <see cref="ImageButton">ImageButton</see>.
    /// </remarks>
    public IEnumerable<ImageSource> UnselectedIconsSource
    {
        get => (IEnumerable<ImageSource>)GetValue(UnselectedIconsSourceProperty);
        set => SetValue(UnselectedIconsSourceProperty, value);
    }

    /// <summary>
    /// Defines the quantity of items on the rating.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 5
    /// </default>
    public int ItemsSize
    {
        get => (int)GetValue(ItemsSizeProperty);
        set => SetValue(ItemsSizeProperty, value);
    }

    /// <summary>
    /// Defines the quantity of items per row on the rating.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 5
    /// </default>
    public int ItemsPerRow
    {
        get => (int)GetValue(ItemsPerRowProperty);
        set => SetValue(ItemsPerRowProperty, value);
    }

    /// <summary>
    /// Defines the value of the Rating.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// -1
    /// </default>
    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    #endregion Properties

    #region Constructors

    public MaterialRating()
    {
        Grid mainLayout = new()
        {
            Margin = new Thickness(0),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Fill,
            RowSpacing = 0,
            RowDefinitions = new()
            {
                new()
                {
                    Height =  GridLength.Auto
                },
                new()
                {
                    Height = GridLength.Auto
                }
            },
            ColumnDefinitions = new()
            {
                new()
                {
                    Width = GridLength.Star
                }
            }
        };

        MaterialLabel label = new()
        {
            TextColor = LabelColor,
            Text = Label,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
        };
        label.SetValue(Grid.RowProperty, 0);

        label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Label), source: this));
        label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(LabelColor), source: this));
        label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(LabelTransform), source: this));
        label.SetBinding(MaterialLabel.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        label.SetBinding(MaterialLabel.IsVisibleProperty, new Binding(nameof(Label), source: this, converter: new IsNotNullOrEmptyConverter()));

        mainLayout.Children.Add(label);

        _containerLayout = new()
        {
            Margin = new Thickness(0),
            Padding = new Thickness(0),
            ColumnSpacing = 0,
            RowSpacing = 0,
            HorizontalOptions = LayoutOptions.Fill
        };

        _containerLayout.SetBinding(Grid.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        _containerLayout.SetValue(Grid.RowProperty, 1);

        mainLayout.Children.Add(_containerLayout);

        SetGridContent();

        Content = mainLayout;
    }

    #endregion Constructors

    #region Methods

    public void OnValuePropertyChanged(MaterialRating control, object newValue)
    {
        if (newValue is not null && int.TryParse(newValue.ToString(), out int value))
        {
            for(int position = 0; position < control._containerLayout.Children.Count; position++)
            {
                var item = control._containerLayout.Children[position];
                SetIconsRatingControl(item, value, position);
            }
        }
    }

    // Set the content of the container of the ratings
    private void SetGridContent()
    {
        var useDefaultIcon = GetIfUseDefaultIcon();

        var itemsSize = ItemsSize;
        var useCustomIconsForEachRating = SelectedIconsSource is not null && UnselectedIconsSource is not null;
        if (useCustomIconsForEachRating) 
        {
            itemsSize = Math.Min(SelectedIconsSource!.Count(), UnselectedIconsSource!.Count());
        }

        int rows = (int)Math.Ceiling(itemsSize * 1.0 / ItemsPerRow * 1.0);
        var itemsPerRow = ItemsPerRow;
        if (rows == 1 && useCustomIconsForEachRating)
        {
            itemsPerRow = Math.Min(SelectedIconsSource!.Count(), UnselectedIconsSource!.Count());
        }

        _containerLayout.RowDefinitions = new RowDefinitionCollection();
        _containerLayout.ColumnDefinitions = new ColumnDefinitionCollection();
        _containerLayout.Children.Clear();

        AddRowsDefinitions(rows);
        AddColumnsDefinitions(itemsPerRow);
        PopulateGrid(rows, itemsPerRow, itemsSize, useDefaultIcon);
    }

    // Add all rating icons
    private void PopulateGrid(int rows, int columns, int itemsSize, bool useDefaultIcon)
    {
        int populatedObjects = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (populatedObjects == itemsSize)
                    return;

                int value = populatedObjects;

                ++populatedObjects;

                if (!useDefaultIcon)
                {
                    AddMaterialIconAsRating(i, j, value, populatedObjects);
                }
                else
                {
                    AddCustomGridAsRating(i, j, value);
                }
            }
        }
    }

    // Add ContentViewButton as rating icon
    private void AddCustomGridAsRating(int row, int column, int value)
    {
        // Add element at row,column position on grid
        var customGrid = new MaterialViewButton()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Command = new Command((e) => OnTapped((int)(e))),
            CommandParameter = value + 1,
            Animation = Animation,
            Padding = 4,
            WidthRequest = 50,
            HeightRequest = 50,
            Margin = new Thickness(3),
        };

        if (AnimationParameter.HasValue)
            customGrid.AnimationParameter = AnimationParameter;

        customGrid.SetValue(Grid.RowProperty, row);
        customGrid.SetValue(Grid.ColumnProperty, column);

        customGrid.SetBinding(Grid.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        SetIconsRatingControl(customGrid, this.Value);

        _containerLayout.Children.Add(customGrid);
    }

    // Add MaterialIconButton as rating icon, populatedObjects is used to know the position of the icon.
    private void AddMaterialIconAsRating(int row, int column, int value, int populatedObjects)
    {
        // Add element at row,column position on grid
        var customImageButton = new MaterialIconButton()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Type = MaterialIconButtonType.Standard,
            Command = new Command((e) => OnTapped((int)(e))),
            CommandParameter = value + 1,
            Animation = Animation,
            Padding = 4,
            Margin = new Thickness(3),
            Style = GetStyleForMaterialRating(),
            UseIconTintColor = false
        };

        if (AnimationParameter.HasValue)
            customImageButton.AnimationParameter = AnimationParameter;

        customImageButton.SetValue(Grid.RowProperty, row);
        customImageButton.SetValue(Grid.ColumnProperty, column);

        customImageButton.SetBinding(MaterialIconButton.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        SetIconsRatingControl(customImageButton, this.Value, populatedObjects - 1);

        _containerLayout.Children.Add(customImageButton);
    }

    // Create a style for MaterialIconButton used in MaterialRating
    private Style GetStyleForMaterialRating()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var pressedState = new VisualState { Name = ButtonCommonStates.Pressed };

        commonStatesGroup.States.Add(new VisualState { Name = ButtonCommonStates.Normal });
        commonStatesGroup.States.Add(pressedState);

        var style = new Style(typeof(MaterialIconButton));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return style;
    }

    // Add row definitions for rating container
    private void AddRowsDefinitions(int rows)
    {
        // Set row definitions of grid
        for (int i = 0; i < rows; i++)
            _containerLayout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
    }

    // Add columns definitions for rating container
    private void AddColumnsDefinitions(int columns)
    {
        // Set columns definitions of grid
        for (int i = 0; i < columns; i++)
            _containerLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(ItemsPerRow / 100.0, GridUnitType.Star) });
    }

    // Get if use the default icon, a star. Returns true if it should draw star; otherwise, false.
    private bool GetIfUseDefaultIcon()
    {
        return SelectedIconSource is null &&
               UnselectedIconSource is null &&
               SelectedIconsSource is null &&
               UnselectedIconsSource is null;
    }

    // Event to set the Value
    private void OnTapped(int value)
    {
        if (IsEnabled)
        {
            if (Value == 1 && value == 1)
                Value = 0;
            else
                Value = value;
        }
    }

    // Set Image of the rating depeding of the value and item
    public void SetIconsRatingControl(object item, int value, int? position = null)
    {
        if (item is MaterialIconButton iconButton)
        {
            UpdateIconButtonAppearance(iconButton, value, position!.Value);
        }
        else if (item is MaterialViewButton gridContainer)
        {
            UpdateGridContainerAppearance(gridContainer, value);
        }
    }

    // Set image of the icon container
    private void UpdateIconButtonAppearance(MaterialIconButton iconButton, int value, int position)
    {
        bool isSelected = iconButton.CommandParameter != null && (int)iconButton.CommandParameter <= value;

        if (isSelected)
        {
            if (SelectedIconSource != null)
            {
                iconButton.ImageSource = SelectedIconSource;
            }
            else
            {
                var selectedIcon = SelectedIconsSource?.ElementAtOrDefault(position);
                if (selectedIcon != null)
                {
                    iconButton.ImageSource = selectedIcon;
                }
            }
        }
        else
        {
            if (UnselectedIconSource != null)
            {
                iconButton.ImageSource = UnselectedIconSource;
            }
            else
            {
                var unselectedIcon = UnselectedIconsSource?.ElementAtOrDefault(position);
                if (unselectedIcon != null)
                {
                    iconButton.ImageSource = unselectedIcon;
                }
            }
        }
    }

    // Add a star to container with a value. If is selected, draws a filled start; otherwise, outline star
    private void UpdateGridContainerAppearance(MaterialViewButton gridContainer, int value)
    {
        double size = Math.Min(gridContainer.Width, gridContainer.Height) - 10;
        if (size < 1)
            size = 40;

        bool isSelected = gridContainer.CommandParameter != null && (int)gridContainer.CommandParameter <= value;
        var starPath = new Path
        {
            Data = CreateStarPathGeometry(size, size),
            Fill = isSelected ? new SolidColorBrush(StrokeColor) : null,
            Stroke = new SolidColorBrush(StrokeColor),
            StrokeThickness = StrokeThickness,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        gridContainer.Content = starPath;
    }

    // Used to draw a star, returns the path geometry of the star
    private PathGeometry CreateStarPathGeometry(double width, double height)
    {
        // Define the points of the star based on the width and height
        double centerX = width / 2;
        double centerY = height / 2;
        double radius = Math.Min(centerX, centerY);

        var pathFigure = new PathFigure { StartPoint = new Point(centerX, centerY - radius) };

        for (int i = 1; i < 10; i++)
        {
            double angle = Math.PI / 5 * i;
            double x = centerX + radius * Math.Sin(angle) * (i % 2 == 0 ? 1 : 0.5);
            double y = centerY - radius * Math.Cos(angle) * (i % 2 == 0 ? 1 : 0.5);
            pathFigure.Segments.Add(new LineSegment { Point = new Point(x, y) });
        }

        // Close the figure
        pathFigure.IsClosed = true;

        var pathGeometry = new PathGeometry();
        pathGeometry.Figures.Add(pathFigure);
        return pathGeometry;
    }

    // We override this method to execute SetGridContent when some property of rating changed that is used to populate the rating icons
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        switch (propertyName)
        {
            case nameof(ItemsSize):
            case nameof(ItemsPerRow):
            case nameof(UnselectedIconSource):
            case nameof(SelectedIconSource):
            case nameof(AnimationParameter):
            case nameof(Animation):
            case nameof(SelectedIconsSource):
            case nameof(UnselectedIconsSource):
                SetGridContent();
                break;
            default:
                base.OnPropertyChanged(propertyName);
                break;
        }
    }

    protected override void ChangeVisualState()
    {
        if (!IsEnabled)
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Disabled);
        }
        else
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Normal);
        }
    }

    #endregion Methods

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = VisualStateManager.CommonStates.Disabled };

        disabledState.Setters.Add(
            MaterialRating.StrokeColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        commonStatesGroup.States.Add(disabledState);

        var style = new Style(typeof(MaterialRating));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }
    #endregion Styles
}