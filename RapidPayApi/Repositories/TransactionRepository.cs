using RapidPayApi.Domain;
using RapidPayApi.Extensions;

namespace RapidPayApi.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public async Task<Transaction> AddTransactionAsync(RapidPayDbContext dbContext, Transaction transaction)
    {
        var transactionEntity = transaction.ToEntity();
        var result = await dbContext.Transactions.AddAsync(transactionEntity);
        await dbContext.SaveChangesAsync();

        return result.Entity.ToDomain();
    }
}