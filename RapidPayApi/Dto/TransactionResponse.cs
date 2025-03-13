namespace RapidPayApi.Dto;

public record TransactionResponse
{
    public required Guid TransactionId { get; init; }
    public required string CardNumber { get; init; }
    public required decimal Amount { get; init; }
    public required decimal Fee { get; init; }
    public required decimal FeeAmount { get; init; }
    public DateTime TransactionDateUtc { get; init; } = DateTime.UtcNow;
    public required bool IsCharge { get; init; }
}