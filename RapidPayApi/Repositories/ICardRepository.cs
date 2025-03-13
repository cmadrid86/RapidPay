using RapidPayApi.Domain;
using RapidPayApi.Dto;

namespace RapidPayApi.Repositories;

public interface ICardRepository
{
    Task<Card> AddCardAsync(RapidPayDbContext dbContext, Card card);
    Task<BalanceDto> GetBalanceAsync(RapidPayDbContext dbContext, string cardNumber);
    Task<Card?> GetCardByNumberAsync(RapidPayDbContext dbContext, string cardNumber);
    Task UpdateBalanceAsync(RapidPayDbContext dbContext, Card card);
}