using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RapidPayApi.Dto;

public record CardRequest : CardBaseRequest
{
    [FromBody]
    [Required]
    [RegularExpression(@"^[0-9]{15}$|^[0-9]{3}(-|\s)[0-9]{4}(-|\s)[0-9]{4}(-|\s)[0-9]{4}$", ErrorMessage = "Invalid card number")]
    public string? CardNumber { get; set; }

    [FromBody]
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string? CardHolderFirstName { get; set; }

    [FromBody]
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string? CardHolderLastName { get; set; }

    [FromBody]
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Limit { get; set; }
}