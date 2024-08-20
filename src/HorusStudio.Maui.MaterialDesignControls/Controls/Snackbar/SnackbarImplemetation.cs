#if IOS
using UIKit;
using Microsoft.Maui.Platform;
#endif
#if ANDROID
using Platform = Microsoft.Maui.ApplicationModel.Platform;
#endif

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class SnackbarImplemetation
{
    public virtual partial IDisposable ShowSnackbar(SnackbarConfig config)
    {
#if IOS
        Snackbar bar = null;
        var app = UIApplication.SharedApplication;

        app.SafeInvokeOnMainThread(() =>
        {
            bar = new Snackbar(config);
            bar.Show();
        });

        return new DisposableAction(() => app.SafeInvokeOnMainThread(() =>
        {
            bar.Dismiss();
            config.DimissAction?.Invoke();
        }));
#endif
#if ANDROID
        Google.Android.Material.Snackbar.Snackbar snackBar = null;
        var activity = Platform.CurrentActivity;
        activity.SafeRunOnUi(() =>
        {
            snackBar = new SnackbarBuilder(activity, config).Build();

            snackBar.Show();
        });
        return new DisposableAction(() =>
        {
            if (snackBar.IsShown)
                activity.SafeRunOnUi(() =>
                {
                    snackBar.Dismiss();
                    config.DimissAction?.Invoke();
                });
        });
#endif
        return null;
    }
}

public partial class SnackbarImplemetation : ISnackbarUser
{
    const string _noAction = "Action should not be set as async will not use it";
    public virtual partial IDisposable ShowSnackbar(SnackbarConfig config);

    public virtual IDisposable ShowSnackbar(string message, string leadingIcon, string trailingIcon,
        TimeSpan? dismissTimer, string actionText, Action action, Action actionLeading, Action actionTrailing)
        => ShowSnackbar(new SnackbarConfig()
        {
            Message = message,
            LeadingIcon = leadingIcon,
            TrailingIcon = trailingIcon,
            Duration = dismissTimer ?? SnackbarConfig.DefaultDuration,
            Action = action,
            ActionLeading = actionLeading,
            ActionTrailing = actionTrailing,
            ActionText = actionText
        });

    public virtual Task ShowSnackbarAsync(string message, string leadingIcon, string trailingIcon,
        TimeSpan? dismissTimer, string actionText, CancellationToken? cancelToken)
        => ShowSnackbarAsync(new SnackbarConfig()
        {
            Message = message,
            LeadingIcon = leadingIcon,
            TrailingIcon = trailingIcon,
            Duration = dismissTimer ?? SnackbarConfig.DefaultDuration,
            ActionText = actionText
        }, cancelToken);

    public virtual async Task ShowSnackbarAsync(SnackbarConfig config, CancellationToken? cancelToken)
    {
        if (config.Action is not null)
            throw new ArgumentException(_noAction);
        if (config.ActionLeading is not null)
            throw new ArgumentException(_noAction);
        if (config.ActionTrailing is not null)
            throw new ArgumentException(_noAction);
    }
}

public interface ISnackbarUser
{
    IDisposable ShowSnackbar(string message, string iconLeading = null, string iconTrailing = null, TimeSpan? dismissTimer = null, string actionText = null, Action action = null, Action actionLeading = null, Action actionTrailing = null);
    IDisposable ShowSnackbar(SnackbarConfig config);
    
    Task ShowSnackbarAsync(string message, string iconLeading = null, string iconTrailing = null, TimeSpan? dismissTimer = null, string actionText = null, CancellationToken? cancelToken = null);
    Task ShowSnackbarAsync(SnackbarConfig config, CancellationToken? cancelToken = null);
}