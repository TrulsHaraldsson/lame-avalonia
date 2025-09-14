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

    public readonly ReactiveCommand<Unit, Unit> CommandExitApplication;
    public readonly ReactiveCommand<Unit, Unit> CommandLogout;

    public MainWindowViewModel(
        IAuthService authService,
        LoginViewModel loginViewModel,
        AboutViewModel aboutViewModel)
    {
        _loginViewModel = loginViewModel;
        _aboutViewModel = aboutViewModel;
        CurrentViewModel = _loginViewModel;

        authService.IsLoggedInChanged += (_, b) =>
        {
            CurrentViewModel = b
                ? _aboutViewModel
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
    }
}