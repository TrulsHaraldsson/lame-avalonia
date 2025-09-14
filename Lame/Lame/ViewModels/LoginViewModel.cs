using System.Reactive;
using System.Reactive.Linq;
using Lame.Services;
using ReactiveUI;

namespace Lame.ViewModels;

public class LoginViewModel : ViewModelBase, IActivatableViewModel
{
    private readonly IAuthService _authService;
    
    private string _username = string.Empty;
    public string Username
    {
        get => _username; 
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }
    
    private string _password = string.Empty;
    public string Password
    {
        get => _password; 
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    private readonly ObservableAsPropertyHelper<bool> _loginIsAvailable;
    public bool LoginIsAvailable => _loginIsAvailable.Value;
    
    public ReactiveCommand<Unit, bool> LoginCommand { get; }
    public ReactiveCommand<Unit, bool> RegisterCommand { get; }

    public LoginViewModel(IAuthService authService)
    {
        Activator = new ViewModelActivator();
        _authService = authService;
        _loginIsAvailable = this.WhenAnyValue(vm => vm.Username, vm => vm.Password)
            .Select(s => !string.IsNullOrEmpty(s.Item1) && !string.IsNullOrEmpty(s.Item2))
            .ToProperty(this, vm => vm.LoginIsAvailable);
        
        LoginCommand = ReactiveCommand.Create(
            () => _authService.TryLogin(Username, Password),
            this.WhenAnyValue(x => x.LoginIsAvailable)
        );

        RegisterCommand = ReactiveCommand.CreateFromTask(
            () => _authService.RegisterAsync(Username, Password),
            this.WhenAnyValue(x => x.LoginIsAvailable));
    }

    public ViewModelActivator Activator { get; }
}