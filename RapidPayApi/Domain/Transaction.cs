namespace RapidPayApi.Domain;

public record Transaction
{
    public required Guid TransactionId { get; init; }
    public required string CardNumber { get; init; }
    public required decimal Amount { get; init; }
    public required decimal Fee { get; init; }
    public DateTime TransactionDateUtc { get; init; } = DateTime.UtcNow;
    public required bool IsCharge { get; init; }
    public required string EstablishmentId { get; init; }

    public decimal FeeAmount => Amount * Fee;
    public decimal TotalAmount => Amount + FeeAmount;
}