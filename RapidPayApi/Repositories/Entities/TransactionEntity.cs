using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RapidPayApi.Repositories.Entities;

public class TransactionEntity
{
    public required Guid TransactionId { get; set; }
    public required decimal Amount { get; set; }
    public required string CardNumber { get; set; }
    public required string EstablishmentId { get; set; }
    public required decimal Fee { get; set; }
    public required decimal FeeAmount { get; set; }
    public required bool IsCharge { get; set; }
    public required decimal TotalAmount { get; set; }
    public required DateTime TransactionDateUtc { get; set; }

    public CardEntity? Card { get; set; }
}

public class TransactionEntityConfig : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(e => e.TransactionId);
        builder.Property(e => e.Amount).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.CardNumber).IsRequired().HasMaxLength(15);
        builder.Property(e => e.EstablishmentId).IsRequired().HasMaxLength(5);
        builder.Property(e => e.Fee).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.FeeAmount).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.IsCharge).IsRequired().HasDefaultValue(true);
        builder.Property(e => e.TotalAmount).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.TransactionDateUtc)
            .IsRequired()
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.HasOne(t => t.Card)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CardNumber);
    }
}