using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RapidPayApi.Dto;
using RapidPayApi.Helper;

namespace RapidPayApi.Engines;

public class TokenEngine : ITokenEngine
{
    private readonly SigningCredentials _signingCredentials;

    private const string Issuer = "RapidPay";

    public TokenEngine(IOptions<Settings> settings)
    {
        var settings1 = settings.Value;
        var jwks = JsonWebKeySetHelper.GetJsonWebKeySet(settings1.JwksKeySet!);
        _signingCredentials = JsonWebKeySetHelper.GetSigningCredentials(jwks);
    }

    public string GenerateToken(string userName)
    {
        var now = DateTime.UtcNow;
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Iss, Issuer),
            new(JwtRegisteredClaimNames.Sub, userName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var handler = new JwtSecurityTokenHandler();

        var token = new JwtSecurityToken
        (
            Issuer,
            null,
            claims,
            now.AddMilliseconds(-30),
            now.AddMinutes(5),
            _signingCredentials
        );

        return handler.WriteToken(token);
    }
}