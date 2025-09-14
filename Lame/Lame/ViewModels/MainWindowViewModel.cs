using Lame.Services;
using ReactiveUI;

namespace Lame.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentViewModel = null!;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set => this.RaiseAndSetIfChanged(ref _currentViewModel, value);
    }

    public MainWindowViewModel()
    {
        CurrentViewModel = new LoginViewModel(new AuthService());
    }
}