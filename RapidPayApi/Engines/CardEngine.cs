using RapidPayApi.Domain;
using RapidPayApi.Dto;
using RapidPayApi.Repositories;

namespace RapidPayApi.Engines;

public class CardEngine(ICardRepository cardRepository, RapidPayDbContext dbContext) : ICardEngine
{
    public Task<Card> AddCardAsync(Card card)
    {
        return cardRepository.AddCardAsync(dbContext, card);
    }

    public Task<BalanceDto> GetBalanceAsync(string cardNumber)
    {
        return cardRepository.GetBalanceAsync(dbContext, cardNumber);
    }

    public Task<Card?> GetCardByNumber(string cardNumber)
    {
        return cardRepository.GetCardByNumberAsync(dbContext, cardNumber);
    }
}