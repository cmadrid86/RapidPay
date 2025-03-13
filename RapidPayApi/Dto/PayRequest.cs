using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RapidPayApi.Dto;

public record PayRequest : CardBaseRequest
{
    [FromRoute]
    [Required]
    [RegularExpression(@"^[0-9]{15}$|^[0-9]{3}(-|\s)[0-9]{4}(-|\s)[0-9]{4}(-|\s)[0-9]{4}$", ErrorMessage = "Invalid card number")]
    public string? CardNumber { get; set; }

    [FromBody]
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; init; }

    [FromBody]
    [Required]
    [StringLength(5, ErrorMessage = "EstablishmentId must be 5 characters")]
    public string EstablishmentId { get; init; }
}