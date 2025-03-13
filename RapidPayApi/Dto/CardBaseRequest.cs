using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RapidPayApi.Dto;

public record CardBaseRequest
{
    [FromBody]
    [Required]
    [Range(1, 12)]
    public byte ExpiryMonth { get; set; }

    [FromBody]
    [Required]
    [Range(2025, 2100)]
    public short ExpiryYear { get; set; }

    [FromBody]
    [Required]
    [Range(100, 999)]
    public short Cvv { get; set; }
}