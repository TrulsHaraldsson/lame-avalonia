using Lame.Services;
using Lame.ViewModels;
using Moq;

namespace Lame.Test.ViewModels;

public class TestLoginViewModel
{
    [TestCase("", "", false)]
    [TestCase("truls", "", false)]
    [TestCase("", "password", false)]
    [TestCase("test", "password", true)]
    public void Login_Is_Only_Available_When_UserName_And_Password_Are_Set(string username, string password,
        bool expected)
    {
        // Arrange
        var moq = new Mock<IAuthService>();
        var vm = new LoginViewModel(moq.Object);

        // Act
        vm.Username = username;
        vm.Password = password;
        
        // Assert
        Assert.That(vm.LoginIsAvailable, Is.EqualTo(expected));
    }

    [Test]
    public void Pressing_Login_Calls_AuthService()
    {
        // Arrange
        var moq = new Mock<IAuthService>();
        var vm = new LoginViewModel(moq.Object);
        
        // Act
        vm.LoginCommand.Execute().Subscribe();
        
        // Assert
        moq.Verify(m => m.TryLogin(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
    
    [Test]
    public void Login_Command_Calls_TryLogin_With_Correct_Parameters()
    {
        // Arrange
        var moq = new Mock<IAuthService>();
        var vm = new LoginViewModel(moq.Object);
        
        // Act
        vm.Username = "truls";
        vm.Password = "password";
        vm.LoginCommand.Execute().Subscribe();
        
        // Assert
        moq.Verify(m => m.TryLogin("truls", "password"), Times.Once);
    }
    
    [Test]
    public void Login_Command_Is_Enabled_When_Username_And_Password_Are_Set()
    {
        // Arrange
        var moq = new Mock<IAuthService>();
        var vm = new LoginViewModel(moq.Object);
        bool captured = false;
        vm.LoginCommand.CanExecute.Subscribe(v =>
        {
            captured = v;
        });
        
        // Act
        vm.Username = "truls";
        vm.Password = "password";
        
        // Assert
        Assert.That(captured, Is.True);
    }

    [Test]
    public void Register_Command_Calls_Register_With_Correct_Parameters()
    {
        // Arrange
        var moq = new Mock<IAuthService>();
        var vm = new LoginViewModel(moq.Object);
        vm.Username = "truls";
        vm.Password = "password";
        
        // Act
        vm.RegisterCommand.Execute().Subscribe();
        
        // Assert
        moq.Verify(m => m.RegisterAsync("truls", "password"), Times.Once);
    }
}