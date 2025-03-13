using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Dto;
using RapidPayApi.Managers;

namespace RapidPayApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class LoginController(IAuthManager authManager) : ControllerBase
{
    [HttpPost]
    public string Login([FromBody] CredentialsRequest credentials)
    {
        return authManager.Login(credentials.Username!, credentials.Password!);
    }
}