using RapidPayApi.Domain;
using RapidPayApi.Dto;
using RapidPayApi.Engines;
using RapidPayApi.Exceptions;

namespace RapidPayApi.Managers;

public class CardManagementManager(
    ICardEngine cardEngine,
    IPaymentEngine paymentEngine,
    IUniversalFeesExchangeEngine universalFeesExchangeEngine) : ICardManagementManager
{
    public Task<Card> AddCardAsync(Card newCard)
    {
        return cardEngine.AddCardAsync(newCard);
    }

    public Task<BalanceDto> GetBalanceAsync(string cardNumber)
    {
        return cardEngine.GetBalanceAsync(cardNumber);
    }

    public async Task<Transaction> PayAsync(Payment payment)
    {
        // Review if payment is expired
        if (payment.IsExpired)
        {
            throw new CardExpiredException();
        }

        // First check if the card exists
        var card = await cardEngine.GetCardByNumber(payment.CardNumber);
        if (card == null)
        {
            throw new CardNotFoundException();
        }

        // Check if the card corresponds against records
        if (!card.IsSameCard(payment))
        {
            throw new CardNotMatchException();
        }

        // Get the fee for the payment
        var fee = universalFeesExchangeEngine.GetFee();
        var totalAmount = card.Balance + payment.Amount + payment.Amount * fee;

        // Check if the card has enough balance
        if (totalAmount > card.Limit)
        {
            throw new InsufficientBalanceException();
        }

        return await paymentEngine.ProcessPayment(payment, card, fee);
    }
}