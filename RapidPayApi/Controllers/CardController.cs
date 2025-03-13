using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Dto;
using RapidPayApi.Extensions;
using RapidPayApi.Managers;

namespace RapidPayApi.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
[Authorize(JwtBearerDefaults.AuthenticationScheme)]
public class CardController(ICardManagementManager manager) : ControllerBase
{
    [HttpPost]
    public async Task<CardResponse> AddCard([FromBody][Required] CardRequest request)
    {
        var card = request.ToDomain();
        var result = await manager.AddCardAsync(card);
        return result.ToResponse();
    }

    [HttpGet("{cardNumber}/balance")]
    public Task<BalanceDto> Pay(
        [FromRoute]
        [Required]
        [RegularExpression(@"^[0-9]{15}$|^[0-9]{3}(-|\s)[0-9]{4}(-|\s)[0-9]{4}(-|\s)[0-9]{4}$", ErrorMessage = "Invalid card number")]
        string cardNumber)
    {
        return manager.GetBalanceAsync(cardNumber);
    }

    [HttpPatch("{cardNumber}/pay")]
    public async Task<TransactionResponse> Pay([Required] PayRequest request)
    {
        var payment = request.ToDomain();
        var result = await manager.PayAsync(payment);
        return result.ToResponse();
    }
}