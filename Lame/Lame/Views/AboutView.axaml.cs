using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Lame.About;

public partial class AboutView : ReactiveUserControl<AboutViewModel>
{
    public AboutView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            
        });
    }
}