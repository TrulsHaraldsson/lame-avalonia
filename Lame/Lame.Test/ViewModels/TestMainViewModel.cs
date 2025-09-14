using Lame.About;
using Lame.Services;
using Lame.ViewModels;
using Moq;

namespace Lame.Test.ViewModels;

public class TestMainViewModel
{
    Mock<IAuthService> _authService = null!;
    private MainWindowViewModel _vm;

    [SetUp]
    public void Setup()
    {
        _authService = new Mock<IAuthService>();
        _vm = new MainWindowViewModel(
            _authService.Object, 
            new LoginViewModel(_authService.Object),
            new AboutViewModel());
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
        Assert.That(_vm.CurrentViewModel, Is.TypeOf<AboutViewModel>(), 
            "Expected the AboutViewModel to be set after login.");
        
    }
}