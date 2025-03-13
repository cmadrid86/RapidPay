namespace RapidPayApi.Dto;

public class Settings
{
    public string? Username { get; set; }
    public string? Password { get; set; }

    public string? JwksKeySet { get; set; }
    public bool IsHttps { get; set; } = false;
}