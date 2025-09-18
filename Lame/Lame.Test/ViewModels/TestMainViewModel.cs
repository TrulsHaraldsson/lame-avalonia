using Lame.About;
using Lame.Services;
using Lame.ViewModels;
using Moq;

namespace Lame.Test.ViewModels;

public class TestMainViewModel
{
    Mock<IAuthService> _authService = null!;
    Mock<ILiveDataService> _liveDataService = null!;
    private MainWindowViewModel _vm;

    [SetUp]
    public void Setup()
    {
        _authService = new Mock<IAuthService>();
        _liveDataService = new Mock<ILiveDataService>();
        _vm = new MainWindowViewModel(
            _authService.Object, 
            new LoginViewModel(_authService.Object),
            new AboutViewModel(),
            new LiveDataViewModel(_liveDataService.Object));
    }
    
    [Test]
    public void After_Initialization_LoginViewModel_Is_Set_To_Current_View()
    {
        // Assert
        Assert.That(_vm.CurrentViewModel, Is.TypeOf<LoginViewModel>(), 
            "Expected the LoginViewModel to be set after initialization.");
    }

    [Test]
    public void Change_To_AboutView_When_Login_Succeeds()
    {
        // Act
        _authService.Raise(m => m.IsLoggedInChanged += null, _authService.Object, true);
        
        // Assert
        Assert.That(_vm.CurrentViewModel, Is.TypeOf<LiveDataViewModel>(), 
            "Expected the AboutViewModel to be set after login.");
    }
}