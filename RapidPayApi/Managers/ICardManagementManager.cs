using RapidPayApi.Domain;
using RapidPayApi.Dto;

namespace RapidPayApi.Managers;

public interface ICardManagementManager
{
    Task<Card> AddCardAsync(Card newCard);
    Task<BalanceDto> GetBalanceAsync(string cardNumber);
    Task<Transaction> PayAsync(Payment payment);
}