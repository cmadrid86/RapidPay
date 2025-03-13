using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RapidPayApi.Repositories.Entities;

public class CardEntity
{
    public required string CardNumber { get; set; }
    public required string CardHolderFirstName { get; set; }
    public required string CardHolderLastName { get; set; }
    public required byte ExpiryMonth { get; set; }
    public required short ExpiryYear { get; set; }
    public required short Cvv { get; set; }
    public required decimal Balance { get; set; }
    public required decimal Limit { get; set; }
    public required DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }

    public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}

public class CardEntityConfig : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(EntityTypeBuilder<CardEntity> builder)
    {
        builder.ToTable("Cards");

        builder.HasKey(e => e.CardNumber);
        builder.Property(e => e.CardNumber).IsRequired().HasMaxLength(15);
        builder.Property(e => e.CardHolderFirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.CardHolderLastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.ExpiryMonth).IsRequired().HasConversion<byte>();
        builder.Property(e => e.ExpiryYear).IsRequired().HasConversion<short>();
        builder.Property(e => e.Cvv).IsRequired().HasConversion<short>();
        builder.Property(e => e.Balance).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.Limit).IsRequired().HasPrecision(18, 4);
        builder.Property(e => e.CreatedAtUtc)
            .IsRequired()
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(e => e.UpdatedAtUtc)
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v!.Value, DateTimeKind.Utc));

        builder.HasMany(e => e.Transactions)
            .WithOne(t => t.Card)
            .HasForeignKey(t => t.CardNumber);
    }
}