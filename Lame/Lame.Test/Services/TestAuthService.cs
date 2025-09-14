using Lame.Services;

namespace Lame.Test.Services;

public class TestAuthService
{
    [Test]
    public void Logout_Raises_Event()
    {
        // Arrange
        var authService = new AuthService();
        var loggedIn = true;
        
        authService.IsLoggedInChanged += (sender, b) =>
        {
            loggedIn = b;
        };
        
        // Act
        authService.Logout();
        
        // Assert
        Assert.That(loggedIn, Is.False);
    }
}