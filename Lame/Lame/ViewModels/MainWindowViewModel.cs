using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Lame.About;
using Lame.Services;
using ReactiveUI;
using Splat;

namespace Lame.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly LoginViewModel _loginViewModel;
    private readonly AboutViewModel _aboutViewModel;
    private readonly LiveDataViewModel _liveDataViewModel;
    private ViewModelBase _currentViewModel = null!;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            this.Log().Debug($"CurrentVeiwModel set to {value.GetType().Name}");
            this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }
    }

    public readonly ReactiveCommand<Unit, Unit> CommandOpenSettings;
    public readonly ReactiveCommand<Unit, Unit> CommandExitApplication;
    public readonly ReactiveCommand<Unit, Unit> CommandLogout;
    public readonly ReactiveCommand<Unit, Unit> CommandShowAboutPage;
    public readonly ReactiveCommand<Unit, Unit> CommandShowLiveData;

    public MainWindowViewModel(
        IAuthService authService,
        LoginViewModel loginViewModel,
        AboutViewModel aboutViewModel,
        LiveDataViewModel liveDataViewModel)
    {
        _loginViewModel = loginViewModel;
        _aboutViewModel = aboutViewModel;
        _liveDataViewModel = liveDataViewModel;
        CurrentViewModel = _loginViewModel;

        authService.IsLoggedInChanged += (_, b) =>
        {
            CurrentViewModel = b
                ? liveDataViewModel
                : _loginViewModel;
        };

        CommandExitApplication = ReactiveCommand.Create(() =>
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            {
                lifetime.Shutdown();
            }
        });
        
        CommandLogout = ReactiveCommand.Create(() =>
        {
            this.Log().Debug("CommandLogout executed");
            authService.Logout();
        });
        
        CommandOpenSettings = ReactiveCommand.Create(() =>
        {
            this.Log().Debug("CommandOpenSettings executed");
            var settingsWindow = new Views.SettingsView();
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                settingsWindow.DataContext = new ViewModels.SettingsViewModel();
                settingsWindow.Show(desktop.MainWindow!);
            }
        });

        CommandShowAboutPage = ReactiveCommand.Create(() =>
        {
            CurrentViewModel = _aboutViewModel;
        });
        
        CommandShowLiveData = ReactiveCommand.Create(() =>
        {
            CurrentViewModel = _liveDataViewModel;
        });
    }
}