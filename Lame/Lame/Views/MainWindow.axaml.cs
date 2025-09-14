using System;
using Avalonia.ReactiveUI;
using System.Reactive.Disposables;
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
        });
    }
}