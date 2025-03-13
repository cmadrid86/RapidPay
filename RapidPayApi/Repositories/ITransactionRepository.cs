using RapidPayApi.Domain;

namespace RapidPayApi.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> AddTransactionAsync(RapidPayDbContext dbContext, Transaction transaction);
}