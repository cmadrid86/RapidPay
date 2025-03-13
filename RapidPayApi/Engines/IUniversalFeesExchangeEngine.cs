namespace RapidPayApi.Engines;

public interface IUniversalFeesExchangeEngine
{
    decimal GetExchangeRate();
    decimal GetFee();
}