using Microsoft.Extensions.Options;
using RapidPayApi.Dto;
using RapidPayApi.Engines;

namespace RapidPayApi.Managers;

public class AuthManager(IOptions<Settings> settings, ITokenEngine tokenEngine) : IAuthManager
{
    public string Login(string userName, string password)
    {
        // Mock implementation of an authentication process
        if (!settings.Value.Username!.Equals(userName, StringComparison.OrdinalIgnoreCase) ||
            !settings.Value.Password!.Equals(password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        return tokenEngine.GenerateToken(userName);
    }
}