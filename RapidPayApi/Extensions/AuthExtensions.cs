using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using RapidPayApi.Dto;
using RapidPayApi.Middleware;

namespace RapidPayApi.Extensions;

public static class AuthExtensions
{
    private const string Issuer = "RapidPay";

    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        var settingsSection = builder.Configuration.GetSection("Settings");
        var settings = settingsSection.Get<Settings>();

        var authBuilder = builder.Services.AddAuthentication();

        // Add authentication methods
        authBuilder.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BasicAuthenticationHandler.AuthenticationScheme, null);
        authBuilder.AddBearer(settings!);

        builder.Services.AddAuthorization(options =>
        {
            var bearerPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .Build();
            options.DefaultPolicy = bearerPolicy;
            options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, bearerPolicy);

            options.AddPolicy(BasicAuthenticationHandler.AuthenticationScheme, configurePolicy =>
                configurePolicy
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(BasicAuthenticationHandler.AuthenticationScheme)
                    .Build());
        });
    }

    private static void AddBearer(this AuthenticationBuilder builder, Settings settings)
    {
        builder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.RequireHttpsMetadata = settings.IsHttps;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidIssuer = Issuer,
                IssuerSigningKeyResolver = (s, _, _, _) =>
                {
                    var jsonWebKeySet = new JsonWebKeySet(settings.JwksKeySet);

                    return jsonWebKeySet.Keys.Select(key =>
                        new RsaSecurityKey(new RSAParameters
                        {
                            Exponent = Base64UrlEncoder.DecodeBytes(key.E),
                            Modulus = Base64UrlEncoder.DecodeBytes(key.N),
                        }));
                },
            };
        });
    }
}