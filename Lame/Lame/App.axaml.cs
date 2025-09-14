using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Lame.About;
using Lame.Services;
using Lame.ViewModels;
using Lame.Views;
using Serilog;
using Splat;
using Splat.Serilog;

namespace Lame;

public partial class App : Application, IEnableLogger
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ConfigureLogger();
        Locator.CurrentMutable.InitializeSplat();
        Locator.CurrentMutable.UseSerilogFullLogger();
        
        RegisterDependencies();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Locator.Current.GetService<MainWindowViewModel>()!
            };

            desktop.Exit += (_, _) =>
            {
                this.Log().Info("Exiting application");
            };
            
            desktop.Startup += (_, _) =>
            {
                this.Log().Info("Starting application");
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
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