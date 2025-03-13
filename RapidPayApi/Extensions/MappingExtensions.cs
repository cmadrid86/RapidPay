using RapidPayApi.Domain;
using RapidPayApi.Dto;
using RapidPayApi.Repositories.Entities;

namespace RapidPayApi.Extensions;

public static class MappingExtensions
{
    public static Card ToDomain(this CardRequest request) =>
        new()
        {
            CardNumber = request.CardNumber!,
            CardHolderFirstName = request.CardHolderFirstName!,
            CardHolderLastName = request.CardHolderLastName!,
            ExpiryMonth = request.ExpiryMonth,
            ExpiryYear = request.ExpiryYear,
            Cvv = request.Cvv,
            Balance = 0,
            Limit = request.Limit
        };

    public static Card ToDomain(this CardEntity entity) =>
        new()
        {
            CardNumber = entity.CardNumber,
            CardHolderFirstName = entity.CardHolderFirstName,
            CardHolderLastName = entity.CardHolderLastName,
            ExpiryMonth = entity.ExpiryMonth,
            ExpiryYear = entity.ExpiryYear,
            Cvv = entity.Cvv,
            Balance = entity.Balance,
            Limit = entity.Limit
        };

    public static Payment ToDomain(this PayRequest request) =>
        new()
        {
            Amount = request.Amount,
            CardNumber = request.CardNumber!,
            Cvv = request.Cvv,
            EstablishmentId = request.EstablishmentId,
            ExpiryMonth = request.ExpiryMonth,
            ExpiryYear = request.ExpiryYear,
            TransactionDateUtc = DateTime.UtcNow,
        };

    public static Transaction ToDomain(this TransactionEntity entity) =>
        new()
        {
            TransactionId = entity.TransactionId,
            Amount = entity.Amount,
            CardNumber = entity.CardNumber,
            EstablishmentId = entity.EstablishmentId,
            Fee = entity.Fee,
            IsCharge = entity.IsCharge,
            TransactionDateUtc = entity.TransactionDateUtc
        };

    public static CardEntity ToEntity(this Card card) =>
        new()
        {
            CardNumber = card.CardNumber,
            CardHolderFirstName = card.CardHolderFirstName,
            CardHolderLastName = card.CardHolderLastName,
            ExpiryMonth = card.ExpiryMonth,
            ExpiryYear = card.ExpiryYear,
            Cvv = card.Cvv,
            CreatedAtUtc = DateTime.UtcNow,
            Balance = card.Balance,
            Limit = card.Limit
        };

    public static TransactionEntity ToEntity(this Transaction transaction) =>
        new()
        {
            TransactionId = transaction.TransactionId,
            Amount = transaction.Amount,
            CardNumber = transaction.CardNumber,
            EstablishmentId = transaction.EstablishmentId,
            Fee = transaction.Fee,
            FeeAmount = transaction.FeeAmount,
            IsCharge = transaction.IsCharge,
            TotalAmount = transaction.TotalAmount,
            TransactionDateUtc = transaction.TransactionDateUtc
        };

    public static CardResponse ToResponse(this Card card) =>
        new()
        {
            CardNumber = MaskCardNumber(card.CardNumber),
            CardHolderFirstName = card.CardHolderFirstName,
            CardHolderLastName = card.CardHolderLastName,
            ExpiryMonth = card.ExpiryMonth,
            ExpiryYear = card.ExpiryYear,
            Cvv = card.Cvv,
        };

    public static TransactionResponse ToResponse(this Transaction transaction) =>
        new()
        {
            TransactionId = transaction.TransactionId,
            CardNumber = MaskCardNumber(transaction.CardNumber),
            Amount = transaction.Amount,
            Fee = transaction.Fee,
            FeeAmount = transaction.FeeAmount,
            TransactionDateUtc = transaction.TransactionDateUtc,
            IsCharge = transaction.IsCharge
        };

    public static string MaskCardNumber(this string cardNumber) =>
        $"{new string('*', 3)}-{new string('*', 4)}-{new string('*', 4)}-{cardNumber.Substring(11, 4)}";
}