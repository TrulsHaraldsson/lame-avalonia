using System;
using Avalonia.ReactiveUI;
using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Lame.ViewModels;
using ReactiveUI;

namespace Lame.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(v => v.ViewModel!.CurrentViewModel)
                .Subscribe(vm =>
                {
                    var view = new ViewLocator().Build(vm);
                    if (view is null) return;
                    view.DataContext = vm;
                    MainContent.Content = view;
                })
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel, vm => vm.CommandExitApplication, v => v.MenuItemExit)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel, vm => vm.CommandLogout, v => v.MenuItemLogout)
                .DisposeWith(disposables);
        });
    }
    
    private void OnHamburgerClick(object? sender, RoutedEventArgs e)
    {
        Hamburger.Content = new TextBlock
        {
            Text = "File"
        };
    }

    private void OnHamburgerFlyoutClosed(object? sender, EventArgs e)
    {
        var icon = new IconPacks.Avalonia.Material.PackIconMaterial
        {
            Kind = IconPacks.Avalonia.Material.PackIconMaterialKind.Menu
        };
        
        Hamburger.Content = icon;
    }
}