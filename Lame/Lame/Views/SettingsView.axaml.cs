using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using Lame.ViewModels;
using ReactiveUI;

namespace Lame.Views;

public partial class SettingsView : ReactiveWindow<SettingsViewModel>
{
    public SettingsView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.Options, v => v.Options.ItemsSource)
                .DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.SelectedNode, v => v.TreeViewSettings.SelectedItem)
                .DisposeWith(disposables);
        });
    }
}