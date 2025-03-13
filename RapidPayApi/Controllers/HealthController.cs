using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Middleware;

namespace RapidPayApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public string HealthCheck()
    {
        return DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
    }

    [HttpGet("basic")]
    [Authorize(BasicAuthenticationHandler.AuthenticationScheme)]
    public string BasicHealthCheck()
    {
        return DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
    }

    [HttpGet("bearer")]
    [Authorize]
    public string BearerHealthCheck()
    {
        return DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
    }
}