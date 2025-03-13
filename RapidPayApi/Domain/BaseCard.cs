namespace RapidPayApi.Domain;

public abstract record BaseCard
{
    private readonly string _cardNumber = string.Empty;

    public required string CardNumber
    {
        get => _cardNumber;
        init => _cardNumber = GetOnlyNumbers(value);
    }

    public required byte ExpiryMonth { get; init; }
    public required short ExpiryYear { get; init; }
    public required short Cvv { get; init; }

    public bool IsExpired => DateTime.UtcNow > new DateTime(ExpiryYear, ExpiryMonth, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMonths(1).AddDays(-1);

    public bool IsSameCard(BaseCard? card) =>
        card is not null &&
        CardNumber == card.CardNumber &&
        ExpiryMonth == card.ExpiryMonth &&
        ExpiryYear == card.ExpiryYear &&
        Cvv == card.Cvv;

    private static string GetOnlyNumbers(string cardNumber) => cardNumber.Replace("-", string.Empty).Replace(" ", string.Empty);
}