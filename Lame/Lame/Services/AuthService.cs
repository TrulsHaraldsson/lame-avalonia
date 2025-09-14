using System;
using System.Threading.Tasks;
using Splat;

namespace Lame.Services;

public class AuthService : IAuthService, IEnableLogger
{

    public event EventHandler<bool>? IsLoggedInChanged;

    public bool TryLogin(string username, string password)
    {
        if (username == "truls")
        {
            this.Log().Info("Login succesful");
            IsLoggedInChanged?.Invoke(this, true);
            return true;
        } 
        
        this.Log().Info("Login failed");
        IsLoggedInChanged?.Invoke(this, false);
        return false;
    }

    public Task<bool> RegisterAsync(string username, string password)
    {
        return Task.FromResult(true);
    }

    public void Logout()
    {
        this.Log().Info("Logout succesful");
        IsLoggedInChanged?.Invoke(this, false);
    }
}