using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using Lame.ViewModels;
using ReactiveUI;

namespace Lame.Views;

public partial class LiveDataView : ReactiveUserControl<LiveDataViewModel>
{
    public LiveDataView()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.LiveData, v => v.TextBlockLiveData.Text)
                .DisposeWith(disposables);
        });
    }
}