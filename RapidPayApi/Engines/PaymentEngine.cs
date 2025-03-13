using RapidPayApi.Domain;
using RapidPayApi.Repositories;

namespace RapidPayApi.Engines;

public class PaymentEngine(
    ICardRepository cardRepository,
    ITransactionRepository transactionRepository,
    RapidPayDbContext dbContext)
    : IPaymentEngine
{
    public async Task<Transaction> ProcessPayment(Payment payment, Card card, decimal fee)
    {
        var transaction = new Transaction
        {
            Amount = payment.Amount,
            Fee = fee,
            TransactionDateUtc = payment.TransactionDateUtc,
            TransactionId = Guid.NewGuid(),
            CardNumber = card.CardNumber,
            IsCharge = true,
            EstablishmentId = payment.EstablishmentId
        };

        card = card with { Balance = card.Balance + transaction.TotalAmount };

        var dbTransaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await transactionRepository.AddTransactionAsync(dbContext, transaction);
            await cardRepository.UpdateBalanceAsync(dbContext, card);

            await dbTransaction.CommitAsync();

            return transaction;
        }
        catch
        {
            await dbTransaction.RollbackAsync();
            throw;
        }
    }
}