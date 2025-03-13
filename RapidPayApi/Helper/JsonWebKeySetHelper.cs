using Microsoft.IdentityModel.Tokens;
using RapidPayApi.Exceptions;

namespace RapidPayApi.Helper;

public static class JsonWebKeySetHelper
{
    private const string AllowedAlgorithm = "RSA";

    public static JsonWebKeySet GetJsonWebKeySet(string? jwksKeySet)
    {
        if (string.IsNullOrWhiteSpace(jwksKeySet))
        {
            throw new InvalidJwksException("Empty JSON web key set ");
        }

        var jwks = new JsonWebKeySet(jwksKeySet);

        if (!jwks.Keys.Any())
        {
            throw new InvalidJwksException("The JSON web key set does not contain any key");
        }

        var jwk = jwks.Keys[0];
        if (!jwk.HasPrivateKey)
        {
            throw new InvalidJwksException("The JSON web key set does not contain a private key");
        }

        if (!jwk.Kty.Equals(AllowedAlgorithm, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidJwksException("The JSON web key set does not contain a RSA key");
        }

        return jwks;
    }

    public static SigningCredentials GetSigningCredentials(JsonWebKeySet jwks)
    {
        var signingKeys = jwks.GetSigningKeys();
        if (!signingKeys.Any())
        {
            throw new InvalidJwksException("The JSON web key set does not contain any signing key");
        }

        var securityKey = signingKeys[0];
        return new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);
    }
}