namespace RapidPayApi.Domain;

public record Card : BaseCard
{
    public required string CardHolderFirstName { get; init; }
    public required string CardHolderLastName { get; init; }
    public decimal Balance { get; set; }
    public decimal Limit { get; set; }
}