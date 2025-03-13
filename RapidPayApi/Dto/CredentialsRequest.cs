using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RapidPayApi.Dto;

public class CredentialsRequest
{
    [FromBody]
    [Required]
    public string? Username { get; set;}

    [FromBody]
    [Required]
    public string? Password { get; set;}
}