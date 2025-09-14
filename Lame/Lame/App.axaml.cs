using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Lame.About;
using Lame.Services;
using Lame.ViewModels;
using Lame.Views;
using Splat;

namespace Lame;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Locator.CurrentMutable.InitializeSplat();
        
        RegisterDependencies();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Locator.Current.GetService<MainWindowViewModel>()!
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private void RegisterDependencies()
    {
        // Services 
        Locator.CurrentMutable.RegisterLazySingleton(() => new AuthService(), typeof(IAuthService));
        
        // View Models
        Locator.CurrentMutable.RegisterLazySingleton(
            () => new MainWindowViewModel(
                Locator.Current.GetService<IAuthService>()!,
                Locator.Current.GetService<LoginViewModel>()!, 
                Locator.Current.GetService<AboutViewModel>()!),
            typeof(MainWindowViewModel));
        Locator.CurrentMutable.RegisterLazySingleton(() => new LoginViewModel(Locator.Current.GetService<IAuthService>()!), typeof(LoginViewModel));
        Locator.CurrentMutable.RegisterLazySingleton(() => new AboutViewModel(), typeof(AboutViewModel));
    }
}