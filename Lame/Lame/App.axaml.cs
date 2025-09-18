using System.Threading;
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
                StopServices();
            };
            
            desktop.Startup += (_, _) =>
            {
                this.Log().Info("Starting application");
                StartLiveService();
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }

    private void RegisterDependencies()
    {
        // Services 
        Locator.CurrentMutable.RegisterLazySingleton(() => new AuthService(), typeof(IAuthService));
        Locator.CurrentMutable.RegisterLazySingleton<ILiveDataService>(() => new LiveDataService());
        
        // View Models
        Locator.CurrentMutable.RegisterLazySingleton(
            () => new MainWindowViewModel(
                Locator.Current.GetService<IAuthService>()!,
                Locator.Current.GetService<LoginViewModel>()!, 
                Locator.Current.GetService<AboutViewModel>()!),
            typeof(MainWindowViewModel));
        Locator.CurrentMutable.RegisterLazySingleton(
            () => new LoginViewModel(Locator.Current.GetService<IAuthService>()!), typeof(LoginViewModel));
        Locator.CurrentMutable.RegisterLazySingleton(() => new AboutViewModel(), typeof(AboutViewModel));
        Locator.CurrentMutable.RegisterLazySingleton(
            () => new LiveDataViewModel(Locator.Current.GetService<ILiveDataService>()!), typeof(LiveDataViewModel));
    }

    private void StartLiveService()
    {
        if (Locator.Current.GetService<ILiveDataService>() is LiveDataService liveService)
        {
            liveService.StartAsync(CancellationToken.None).ConfigureAwait(false);
        }
    }

    private void StopServices()
    {
        var liveService = Locator.Current.GetService<ILiveDataService>() as LiveDataService;
        liveService?.Stop();
    }
}