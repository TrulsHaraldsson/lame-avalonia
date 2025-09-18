using System.Reactive.Disposables;
using Lame.Services;
using ReactiveUI;

namespace Lame.ViewModels;

public class LiveDataViewModel : ViewModelBase, IActivatableViewModel
{
    private readonly ILiveDataService _liveDataService;
    
    private string _liveData = string.Empty;
    public string LiveData
    {
        get => _liveData; 
        set => this.RaiseAndSetIfChanged(ref _liveData, value);
    }
    
    public ViewModelActivator Activator { get; }
    
    public LiveDataViewModel(ILiveDataService liveDataService)
    {
        _liveDataService = liveDataService;
        Activator = new ViewModelActivator();
        
        this.WhenActivated(disposables =>
        {
            _liveDataService.LiveDataReceived += UpdateData;
            Disposable.Create(() =>
                {
                    // To avoid memory leaks
                    _liveDataService.LiveDataReceived -= UpdateData;
                })
                .DisposeWith(disposables);
        });
    }

    private void UpdateData(string data)
    {
        LiveData = data;
    }
}