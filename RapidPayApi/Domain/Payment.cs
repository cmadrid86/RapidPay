namespace RapidPayApi.Domain;

public record Payment : BaseCard
{
    public required decimal Amount { get; init; }

    public required string EstablishmentId { get; init; }

    public DateTime TransactionDateUtc { get; init; } = DateTime.UtcNow;
}