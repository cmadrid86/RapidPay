using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using RapidPayApi.Dto;

namespace RapidPayApi.Middleware;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IOptions<Settings> settings)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthenticationScheme = "BasicAuthentication";

    private const string DefaultAdminRole = "AdminRole";

    private readonly Settings _settings = settings.Value;
    private AuthenticationHeaderValue _authHeaderValue = null!;

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!AuthorizationIsRequired())
        {
            return AuthenticateResult.NoResult();
        }
        if (!HasAuthorizationHeader())
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }
        if (!IsAuthenticationHeaderValueValid())
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
        if (!_authHeaderValue.Scheme.Equals("Basic"))
        {
            return AuthenticateResult.Fail($"Invalid Authorization Type {_authHeaderValue.Scheme}");
        }
        if (_authHeaderValue.Parameter is null)
        {
            return AuthenticateResult.Fail("Missing Authorization Value");
        }
        if (!AuthBasicCredentials.TryParse(_authHeaderValue.Parameter, out var credentials))
        {
            return AuthenticateResult.Fail("Invalid credentials");
        }

        return credentials.Equals(_settings.Username, _settings.Password)
            ? await Task.FromResult(AuthenticateResult.Success(CreateTicket()))
            : await Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
    }

    private bool AuthorizationIsRequired()
    {
        var endpoint = Context.GetEndpoint();
        return endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() is not null && endpoint.Metadata.GetMetadata<IAllowAnonymous>() is null;
    }

    private AuthenticationTicket CreateTicket()
    {
        return new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity([
            new(ClaimTypes.NameIdentifier, _settings.Username!),
            new(ClaimTypes.Name, _settings.Username!),
            new(ClaimTypes.Role, DefaultAdminRole)
        ], Scheme.Name)), Scheme.Name);
    }

    private bool HasAuthorizationHeader()
    {
        return Request.Headers.ContainsKey("Authorization");
    }

    private bool IsAuthenticationHeaderValueValid()
    {
        return AuthenticationHeaderValue.TryParse(Request.Headers.Authorization, out _authHeaderValue!);
    }
}
