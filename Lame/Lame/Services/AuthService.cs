using System;
using System.Threading.Tasks;

namespace Lame.Services;

public class AuthService : IAuthService
{

    public event EventHandler<bool>? IsLoggedInChanged;

    public bool TryLogin(string username, string password)
    {
        if (username == "truls")
        {
            IsLoggedInChanged?.Invoke(this, true);
            return true;
        } 
        IsLoggedInChanged?.Invoke(this, false);
        return false;
    }

    public Task<bool> RegisterAsync(string username, string password)
    {
        return Task.FromResult(true);
    }

    public void Logout()
    {
        IsLoggedInChanged?.Invoke(this, false);
    }
}