using Microsoft.EntityFrameworkCore;
using RapidPayApi.Repositories.Entities;

namespace RapidPayApi.Repositories;

public class RapidPayDbContext(DbContextOptions<RapidPayDbContext> options) : DbContext(options)
{
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardEntityConfig());
        modelBuilder.ApplyConfiguration(new TransactionEntityConfig());
    }
}