namespace RapidPayApi.Dto;

public record CardResponse
{
    public string? CardNumber { get; init; }

    public string? CardHolderFirstName { get; init; }

    public string? CardHolderLastName { get; init; }

    public byte ExpiryMonth { get; init; }

    public short ExpiryYear { get; init; }

    public short Cvv { get; init; }
}