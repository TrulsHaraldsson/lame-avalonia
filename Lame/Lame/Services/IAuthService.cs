using System;
using System.Threading.Tasks;

namespace Lame.Services;

public interface IAuthService
{
    event EventHandler<bool> IsLoggedInChanged;
    
    bool TryLogin(string username, string password);
    
    Task<bool> RegisterAsync(string username, string password);
    void Logout();
}