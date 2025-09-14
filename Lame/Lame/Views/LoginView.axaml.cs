using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using Lame.ViewModels;
using ReactiveUI;

namespace Lame.Views;

public partial class LoginView : ReactiveUserControl<LoginViewModel>
{
    public LoginView()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.BindCommand(ViewModel, vm => vm.LoginCommand, v => v.LoginButton)
                .DisposeWith(disposables);
            
            this.Bind(ViewModel, vm => vm.Username, v => v.UserNameTextBox.Text)
                .DisposeWith(disposables);
            
            this.Bind(ViewModel, vm => vm.Password, v => v.PasswordTextBox.Text)
                .DisposeWith(disposables);
        });
    }
}