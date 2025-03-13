using RapidPayApi.Domain;
using RapidPayApi.Dto;

namespace RapidPayApi.Engines;

public interface ICardEngine
{
    Task<Card> AddCardAsync(Card card);
    Task<BalanceDto> GetBalanceAsync(string cardNumber);
    Task<Card?> GetCardByNumber(string cardNumber);
}