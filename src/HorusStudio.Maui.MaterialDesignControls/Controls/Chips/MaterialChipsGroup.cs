﻿
using HorusStudio.Maui.MaterialDesignControls.Converters;
using Microsoft.Maui.Layouts;
using System.Collections;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Chips group facilitate the selection of one or more options within a group, optimizing space usage effectively <see href="https://m3.material.io/components/chips/overview">see here.</see>
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignPlugin/develop/screenshots/MaterialChips.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesign;assembly=HorusStudio.Maui.MaterialDesign"
/// 
/// &lt;material:MaterialChipsGroup
///        IsMultipleSelection="True"
///        ItemsSource="{Binding Colors}"
///        LabelText="Colors *"
///        SelectedItems="{Binding SelectedColors}"
///        SupportingText="Please select at least 4 colors"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var chips = new MaterialChipsGroup
/// {
///     IsMultipleSelection = true,
///     ItemsSource = Colors,
///     LabelText = "Colors *",
///     SelectedItems = SelectedColors,
///     SupportingText="Please select at least 4 colors"
/// };
///</code>
///
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesign.Sample/Pages/ChipsPage.xaml)
/// 
/// </example>
/// <todoList>
/// * For the SelectedItems to be updated correctly they must be initialized. Finding a way to make it work even when the list starts out null
/// </todoList>
public class MaterialChipsGroup : ContentView
{
    #region Attributes

    private static readonly Thickness DefaultPadding = new Thickness(12, 0);
    private static readonly Thickness DefaultChipsPadding = new Thickness(16, 0);
    private static readonly double DefaultChipsHeightRequest = 32.0;
    private static readonly double DefaultChipsFlexLayoutPercentageBasis = 0.0;
    private static readonly bool DefaultIsEnabled = true;
    private static readonly string DefaultLabelText = null;
    private static readonly IEnumerable DefaultItemsSource = null;
    private static readonly object DefaultSelectedItem = null;
    private static readonly IList DefaultSelectedItems = null;
    private static readonly string DefaultSupportingText = null;
    private static readonly Color DefaultLabelTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialLightTheme.Text }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultSupportingTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Error, Dark = MaterialLightTheme.Error }.GetValueForCurrentTheme<Color>();
    private static readonly double DefaultLabelSize = MaterialFontSize.BodySmall;
    private static readonly double DefaultSupportingSize = MaterialFontSize.BodySmall;
    private static readonly Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow }.GetValueForCurrentTheme<Color>();
    private static readonly Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
    private static readonly double DefaultFontSize = MaterialFontSize.LabelLarge;
    private static readonly string DefaultFontFamily = MaterialFontFamily.Default;
    private static readonly double DefaultCornerRadius = 8.0;
    private static readonly bool DefaultAnimateError = MaterialAnimation.AnimateOnError;
    private static readonly bool DefaultIsMultipleSelection = false;
    private static readonly AnimationTypes DefaultAnimation = MaterialAnimation.Type;
    private static readonly double? DefaultAnimationParameter = MaterialAnimation.Parameter;
    private static readonly Align DefaultAlign = Align.Start;
    private static readonly int DefaultVerticalSpacing = 4;
    private static readonly int DefaultHorizontalSpacing = 4;
    private static readonly string DefaultPropertyPath = null;

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Padding" />
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialChipsGroup), defaultValue: DefaultPadding);

    /// <summary>
    /// The backing store for the <see cref="ChipsPadding" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ChipsPaddingProperty = BindableProperty.Create(nameof(ChipsPadding), typeof(Thickness), typeof(MaterialChipsGroup), defaultValue: DefaultChipsPadding);

    /// <summary>
    /// The backing store for the <see cref="ChipsHeightRequest" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ChipsHeightRequestProperty = BindableProperty.Create(nameof(ChipsHeightRequest), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultChipsHeightRequest);

    /// <summary>
    /// The backing store for the <see cref="ChipsFlexLayoutPercentageBasis" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ChipsFlexLayoutBasisPercentageProperty = BindableProperty.Create(nameof(ChipsFlexLayoutPercentageBasis), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultChipsFlexLayoutPercentageBasis);

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" />
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultIsEnabled, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.SetIsEnabled(newValue);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="LabelText" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultLabelText);

    /// <summary>
    /// The backing store for the <see cref="ItemsSource" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(MaterialChipsGroup), defaultValue: DefaultItemsSource, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.SetItemsSource(newValue);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectedItem" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MaterialChipsGroup), defaultValue: DefaultSelectedItem, defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The backing store for the <see cref="SelectedItems" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(IList), typeof(MaterialChipsGroup), defaultValue: DefaultSelectedItems, defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The backing store for the <see cref="SupportingText" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingTextProperty = BindableProperty.Create(nameof(SupportingText), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultSupportingText, propertyChanged: async (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            await self.ValidateText(newValue);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="LabelTextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTextColorProperty = BindableProperty.Create(nameof(LabelTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultLabelTextColor);

    /// <summary>
    /// The backing store for the <see cref="SupportingTextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty SupportingTextColorProperty = BindableProperty.Create(nameof(SupportingTextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultSupportingTextColor);

    /// <summary>
    /// The backing store for the <see cref="LabelSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(nameof(LabelSize), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultLabelSize);

    /// <summary>
    /// The backing store for the <see cref="SupportingSize" />
    /// bindable property.
    /// </summary>   
    public static readonly BindableProperty SupportingSizeProperty = BindableProperty.Create(nameof(SupportingSize), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultSupportingSize);

    /// <summary>
    /// The backing store for the <see cref="TextColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" />
    /// bindable property.
    /// </summary>
    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="BorderColor" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialChipsGroup), defaultValue: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="FontSize" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultFontFamily);

    /// <summary>
    /// The backing store for the <see cref="CornerRadius" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MaterialChipsGroup), defaultValue: DefaultCornerRadius);

    /// <summary>
    /// The backing store for the <see cref="AnimateError" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimateErrorProperty = BindableProperty.Create(nameof(AnimateError), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultAnimateError);

    /// <summary>
    /// The backing store for the <see cref="IsMultipleSelection" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty IsMultipleSelectionProperty = BindableProperty.Create(nameof(IsMultipleSelection), typeof(bool), typeof(MaterialChipsGroup), defaultValue: DefaultIsMultipleSelection);

    /// <summary>
    /// The backing store for the <see cref="Animation" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialChipsGroup), defaultValue: DefaultAnimation);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialChipsGroup), defaultValue: DefaultAnimationParameter);

    /// <summary>
    /// The backing store for the <see cref="Align" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty AlignProperty = BindableProperty.Create(nameof(Align), typeof(Align), typeof(MaterialChipsGroup), defaultValue: DefaultAlign);

    /// <summary>
    /// The backing store for the <see cref="VerticalSpacing" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty VerticalSpacingProperty = BindableProperty.Create(nameof(VerticalSpacing), typeof(int), typeof(MaterialChipsGroup), defaultValue: DefaultVerticalSpacing, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.SetMargins();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="HorizontalSpacing" />
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty HorizontalSpacingProperty = BindableProperty.Create(nameof(HorizontalSpacing), typeof(int), typeof(MaterialChipsGroup), defaultValue: DefaultHorizontalSpacing, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialChipsGroup self)
        {
            self.SetMargins();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="PropertyPath" /> 
    /// bindable property.
    /// </summary>
    public static readonly BindableProperty PropertyPathProperty = BindableProperty.Create(nameof(PropertyPath), typeof(string), typeof(MaterialChipsGroup), defaultValue: DefaultPropertyPath);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the padding for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(12,0)
    /// </default>
    public new Thickness Padding
    {
        get { return (Thickness)GetValue(PaddingProperty); }
        set { SetValue(PaddingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the padding for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Thickness(16,0)
    /// </default>
    public Thickness ChipsPadding
    {
        get { return (Thickness)GetValue(ChipsPaddingProperty); }
        set { SetValue(ChipsPaddingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the height for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 32.0
    /// </default>
    public double ChipsHeightRequest
    {
        get { return (double)GetValue(ChipsHeightRequestProperty); }
        set { SetValue(ChipsHeightRequestProperty, value); }
    }

    /// <summary>
    /// Gets or sets the basis for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 0.0
    /// </default>
    public double ChipsFlexLayoutPercentageBasis
    {
        get { return (double)GetValue(ChipsFlexLayoutBasisPercentageProperty); }
        set { SetValue(ChipsFlexLayoutBasisPercentageProperty, value); }
    }

    /// <summary>
    /// Gets or sets the state when the Chips is enabled.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="True"/>
    /// </default>
    public new bool IsEnabled
    {
        get { return (bool)GetValue(IsEnabledProperty); }
        set { SetValue(IsEnabledProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public string LabelText
    {
        get { return (string)GetValue(LabelTextProperty); }
        set { SetValue(LabelTextProperty, value); }
    }

    /// <summary>
    /// Gets or sets the source of the items for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public IEnumerable ItemsSource
    {
        get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    /// <summary>
    /// Gets or sets the selected item for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public object SelectedItem
    {
        get { return (object)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    /// <summary>
    /// Gets or sets the selected items for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// This property needs to be initialized from the implementation. It cannot be null.
    /// </remarks>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public IList SelectedItems
    {
        get { return (IList)GetValue(SelectedItemsProperty); }
        set { SetValue(SelectedItemsProperty, value); }
    }

    /// <summary>
    /// Gets or sets the supporting text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public string SupportingText
    {
        get { return (string)GetValue(SupportingTextProperty); }
        set { SetValue(SupportingTextProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font color of text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Text">MaterialLightTheme.Text</see> - Dark = <see cref="MaterialDarkTheme.Text">MaterialDarkTheme.Text</see>
    /// </default>
    public Color LabelTextColor
    {
        get { return (Color)GetValue(LabelTextColorProperty); }
        set { SetValue(LabelTextColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font color of supporting text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Error">MaterialLightTheme.Error</see> - Dark = <see cref="MaterialDarkTheme.Error">MaterialDarkTheme.Error</see>
    /// </default>
    public Color SupportingTextColor
    {
        get { return (Color)GetValue(SupportingTextColorProperty); }
        set { SetValue(SupportingTextColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font size of text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodySmall">MaterialFontSize.BodySmall</see> / Tablet = 15 / Phone = 12
    /// </default>
    public double LabelSize
    {
        get { return (double)GetValue(LabelSizeProperty); }
        set { SetValue(LabelSizeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font size of supporting text for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.BodySmall">MaterialFontSize.BodySmall</see> / Tablet = 15 / Phone = 12
    /// </default>
    public double SupportingSize
    {
        get { return (double)GetValue(SupportingSizeProperty); }
        set { SetValue(SupportingSizeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font color of text for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark = <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the background color for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.SurfaceContainerLow">MaterialLightTheme.SurfaceContainerLow</see> - Dark = <see cref="MaterialDarkTheme.SurfaceContainerLow">MaterialDarkTheme.SurfaceContainerLow</see>
    /// </default>
    public new Color BackgroundColor
    {
        get { return (Color)GetValue(BackgroundColorProperty); }
        set { SetValue(BackgroundColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the border color for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Outline">MaterialLightTheme.Outline</see> - Dark = <see cref="MaterialDarkTheme.Outline">MaterialDarkTheme.Outline</see>
    /// </default>
    public Color BorderColor
    {
        get { return (Color)GetValue(BorderColorProperty); }
        set { SetValue(BorderColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font size of text for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet = 17 / Phone = 14
    /// </default>
    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family of text for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
    /// </default>
    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the corner radius for the Chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 8.0
    /// </default>
    public double CornerRadius
    {
        get { return (double)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    /// <summary>
    /// Gets or sets if the error animation is enabled for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="True"/>
    /// </default>
    public bool AnimateError
    {
        get { return (bool)GetValue(AnimateErrorProperty); }
        set { SetValue(AnimateErrorProperty, value); }
    }

    /// <summary>
    /// Gets or sets if the multi selection is enabled for the ChipsGroup.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="False"/>
    /// </default>
    public bool IsMultipleSelection
    {
        get { return (bool)GetValue(IsMultipleSelectionProperty); }
        set { SetValue(IsMultipleSelectionProperty, value); }
    }

    /// <summary>
    /// Gets or sets an animation to be executed when is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="AnimationTypes.Fade"> AnimationTypes.Fade </see>
    /// </default>
    public AnimationTypes Animation
    {
        get { return (AnimationTypes)GetValue(AnimationProperty); }
        set { SetValue(AnimationProperty, value); }
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
        get { return (double?)GetValue(AnimationParameterProperty); }
        set { SetValue(AnimationParameterProperty, value); }
    }

    /// <summary>
    /// Gets or sets the horizontal alignment for the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="Align.Start"> Align.Start </see>
    /// </default>
    public Align Align
    {
        get { return (Align)GetValue(AlignProperty); }
        set { SetValue(AlignProperty, value); }
    }

    /// <summary>
    /// Gets or sets the vertical spacing between the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 4
    /// </default>
    public int VerticalSpacing
    {
        get { return (int)GetValue(VerticalSpacingProperty); }
        set { SetValue(VerticalSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the horizontal spacing between the chips.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 4
    /// </default>
    public int HorizontalSpacing
    {
        get { return (int)GetValue(HorizontalSpacingProperty); }
        set { SetValue(HorizontalSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the property path.
    /// This property is used to map an object and display a property of it.
    /// </summary>
    /// <remarks>
    /// If it´s no defined, the control will use toString() method.
    /// </remarks>
    /// <default>
    /// <see langword="null"/>
    /// </default>
    public string PropertyPath
    {
        get => (string)GetValue(PropertyPathProperty);
        set => SetValue(PropertyPathProperty, value);
    }

    #endregion Properties

    #region Layout

    private FlexLayout _flexContainer;
    private MaterialLabel _textLabel;
    private MaterialLabel _lblSupporting;
    private ContentView _contentView;

    #endregion Layout

    #region Constructor

    public MaterialChipsGroup()
    {
        _flexContainer = new FlexLayout
        {
            Wrap = FlexWrap.Wrap,
            Direction = FlexDirection.Row,
        };

        _textLabel = new MaterialLabel
        {
            LineBreakMode = LineBreakMode.NoWrap,
            Margin = new Thickness(14, 0, 14, 2),
            HorizontalTextAlignment = TextAlignment.Start,
        };

        _lblSupporting = new MaterialLabel
        {
            LineBreakMode = LineBreakMode.NoWrap,
            Margin = new Thickness(14, 2, 14, 0),
            HorizontalTextAlignment = TextAlignment.Start,
        };

        _contentView = new ContentView
        {
            Content = _flexContainer,
        };

        _flexContainer.SetBinding(FlexLayout.JustifyContentProperty, new Binding(nameof(Align), source: this, converter: new AlignToFlexJustifyConverter()));

        _textLabel.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(LabelText), source: this));
        _textLabel.SetBinding(MaterialLabel.IsVisibleProperty, new Binding(nameof(LabelText), source: this, converter: new IsNotNullOrEmptyConverter()));
        _textLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(LabelTextColor), source: this));
        _textLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(LabelSize), source: this));
        _textLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));

        _lblSupporting.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(SupportingText), source: this));
        _lblSupporting.SetBinding(MaterialLabel.IsVisibleProperty, new Binding(nameof(SupportingText), source: this, converter: new IsNotNullOrEmptyConverter()));
        _lblSupporting.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(SupportingTextColor), source: this));
        _lblSupporting.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(SupportingSize), source: this));
        _lblSupporting.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));

        _contentView.SetBinding(ContentView.PaddingProperty, new Binding(nameof(Padding), source: this));

        var container = new VerticalStackLayout
        {
            _textLabel,
            _contentView,
            _lblSupporting
        };

        container.Spacing = 2;

        Content = container;
    }

    #endregion Constructor

    #region Setters

    private void SetItemsSource(object newValue)
    {
        _flexContainer.Children.Clear();

        if (newValue != null && newValue is IEnumerable)
        {
            var selectedPathValues = GetSelectedPathValues();
            var selectedPathValue = GetSelectedPathValue();

            foreach (var item in (IEnumerable)newValue)
            {
                var newItem = string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath);

                var materialChips = new MaterialChips(true)
                {
                    Text = newItem,
                    FontSize = FontSize,
                    FontFamily = FontFamily,
                    CornerRadius = CornerRadius,
                    Padding = ChipsPadding,
                    Type = MaterialChipsType.Filter,
                    IsEnabled = IsEnabled,
                    Animation = Animation,
                    AnimationParameter = AnimationParameter,
                    HeightRequest = ChipsHeightRequest,
                    Margin = GetMargin(),
                };

                if (DefaultBackgroundColor != BackgroundColor)
                {
                    materialChips.BackgroundColor = BackgroundColor;
                }

                if (DefaultTextColor != TextColor)
                {
                    materialChips.TextColor = TextColor;
                }

                if (DefaultBorderColor != BorderColor)
                {
                    materialChips.BorderColor = BorderColor;
                }

                if (IsMultipleSelection && SelectedItems != null && selectedPathValues.Any())
                {
                    materialChips.IsSelected = selectedPathValues.Contains(materialChips.Text);
                }
                else if (!IsMultipleSelection && SelectedItem != null)
                {
                    materialChips.IsSelected = materialChips.Text == selectedPathValue;
                }

                materialChips.Command = new Command(() => SelectionCommand(materialChips));

                if (ChipsFlexLayoutPercentageBasis > 0 && ChipsFlexLayoutPercentageBasis <= 1)
                {
                    FlexLayout.SetBasis(materialChips, new FlexBasis((float)ChipsFlexLayoutPercentageBasis, true));
                }

                _flexContainer.Children.Add(materialChips);
            }
        }
    }

    private void SetType(MaterialChipsType type)
    {
        foreach (var view in _flexContainer.Children)
        {
            if (view is MaterialChips materialChips)
            {
                materialChips.Type = type;
            }
        }
    }

    private void SetIsEnabled(object newValue)
    {
        foreach (var view in _flexContainer.Children)
        {
            if (view is MaterialChips materialChips)
            {
                materialChips.IsEnabled = IsEnabled;
            }
        }
    }

    private void SetMargins()
    {
        foreach (var view in _flexContainer.Children)
        {
            if (view is MaterialChips materialChips)
            {
                materialChips.Margin = GetMargin();
            }
        }
    }

    #endregion Setters

    #region Validations

    private async Task<bool> ValidateText(object value)
    {
        if (AnimateError && !string.IsNullOrEmpty(SupportingText) && SupportingText == (string)value)
        {
            await ShakeAnimation.AnimateAsync(Content);
        }

        return true;
    }

    #endregion Validations

    #region Methods

    private void SelectionCommand(MaterialChips materialChips)
    {
        if (!IsEnabled) return;

        if (materialChips is MaterialChips)
        {
            if (IsMultipleSelection)
            {
                var selectedItems = SelectedItems != null ? SelectedItems : new List<object>();

                foreach (var item in ItemsSource)
                {
                    var propertyPathValue = string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath);

                    if (propertyPathValue == materialChips.Text)
                    {
                        if (selectedItems.Contains(item))
                        {
                            if (materialChips.IsSelected)
                            {
                                selectedItems.Add(item);
                            }
                            else
                            {
                                selectedItems.Remove(item);
                            }
                        }
                        else if (materialChips.IsSelected)
                        {
                            selectedItems.Add(item);
                        }

                        break;
                    }
                }

                SelectedItems = selectedItems;
            }
            else
            {
                foreach (var item in ItemsSource)
                {
                    var propertyPathValue = string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath);

                    if (propertyPathValue == materialChips.Text)
                    {
                        SelectedItem = materialChips.IsSelected ? item : null;
                        break;
                    }
                }

                var selectedPathValue = GetSelectedPathValue();

                foreach (var item in _flexContainer.Children)
                {
                    if (item is MaterialChips itemMC && itemMC.Text != selectedPathValue)
                    {
                        itemMC.IsSelected = false;
                    }
                }
            }
        }
    }

    private List<string> GetSelectedPathValues()
    {
        var selectedPathValues = new List<string>();

        if (SelectedItems != null)
        {
            foreach (var item in SelectedItems)
            {
                selectedPathValues.Add(string.IsNullOrWhiteSpace(PropertyPath) ? item.ToString() : GetPropertyValue(item, PropertyPath));
            }
        }

        return selectedPathValues;
    }

    private string GetSelectedPathValue()
    {
        var selectedPathValue = string.Empty;

        if (SelectedItem != null)
        {
            selectedPathValue = string.IsNullOrWhiteSpace(PropertyPath) ? SelectedItem.ToString() : GetPropertyValue(SelectedItem, PropertyPath);
        }

        return selectedPathValue;
    }

    private Thickness GetMargin()
    {
        switch (Align)
        {
            default:
            case Align.Start:
                return new Thickness(0, VerticalSpacing / 2, HorizontalSpacing, VerticalSpacing / 2);
            case Align.Center:
                return new Thickness(HorizontalSpacing / 2, VerticalSpacing / 2, HorizontalSpacing / 2, VerticalSpacing / 2);
            case Align.End:
                return new Thickness(HorizontalSpacing, VerticalSpacing / 2, 0, VerticalSpacing / 2);
        }
    }

    private string GetPropertyValue(object item, string propertyToSearch)
    {
        var properties = item.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property.Name.Equals(propertyToSearch, StringComparison.InvariantCultureIgnoreCase))
            {
                return property.GetValue(item, null).ToString();
            }
        }
        return item.ToString();
    }

    #endregion Methods
}