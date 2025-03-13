using Microsoft.EntityFrameworkCore;
using RapidPayApi.Domain;
using RapidPayApi.Dto;
using RapidPayApi.Exceptions;
using RapidPayApi.Extensions;

namespace RapidPayApi.Repositories;

public class CardRepository : ICardRepository
{
    public async Task<Card> AddCardAsync(RapidPayDbContext dbContext, Card card)
    {
        var entity = card.ToEntity();
        var result = await dbContext.Cards.AddAsync(entity);

        await dbContext.SaveChangesAsync();

        return result.Entity.ToDomain();
    }

    public async Task<BalanceDto> GetBalanceAsync(RapidPayDbContext dbContext, string cardNumber)
    {
        var cardEntity = await dbContext.Cards
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.CardNumber.Equals(cardNumber));

        if (cardEntity == null)
        {
            throw new CardNotFoundException();
        }

        return new BalanceDto
        {
            CardNumber = cardEntity.CardNumber.MaskCardNumber(),
            CardHolderFirstName = cardEntity.CardHolderFirstName,
            CardHolderLastName = cardEntity.CardHolderLastName,
            Balance = cardEntity.Balance,
            Transactions = cardEntity.Transactions.Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                EstablishmentId = t.EstablishmentId,
                TotalAmount = t.TotalAmount,
                TransactionDateUtc = t.TransactionDateUtc
            }).ToArray()
        };
    }

    public async Task<Card?> GetCardByNumberAsync(RapidPayDbContext dbContext, string cardNumber)
    {
        var cardEntity = await dbContext.Cards.FirstOrDefaultAsync(c => c.CardNumber.Equals(cardNumber));
        return cardEntity?.ToDomain();
    }

    public async Task UpdateBalanceAsync(RapidPayDbContext dbContext, Card card)
    {
        var cardEntity = await dbContext.Cards.FirstOrDefaultAsync(c => c.CardNumber.Equals(card.CardNumber));
        cardEntity!.Balance = card.Balance;
        cardEntity.UpdatedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
    }
}