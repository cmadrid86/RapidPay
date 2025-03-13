using RapidPayApi.Domain;

namespace RapidPayApi.Engines;

public interface IPaymentEngine
{
    Task<Transaction> ProcessPayment(Payment payment, Card card, decimal fee);
}