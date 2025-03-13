namespace RapidPayApi.Dto;

public record BalanceDto
{
    public string? CardNumber { get; init; }
    public string? CardHolderFirstName { get; init; }
    public string? CardHolderLastName { get; init; }
    public decimal Balance { get; init; }

    public TransactionDto[]? Transactions { get; init; }
}

public record TransactionDto
{
    public Guid? TransactionId { get; init; }
    public string? EstablishmentId { get; init; }
    public DateTime TransactionDateUtc { get; init; }
    public decimal TotalAmount { get; init; }
}