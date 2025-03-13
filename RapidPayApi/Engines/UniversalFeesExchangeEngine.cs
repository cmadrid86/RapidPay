namespace RapidPayApi.Engines;

public class UniversalFeesExchangeEngine : IUniversalFeesExchangeEngine
{
    private decimal _exchangeRate = 1.0m;
    private DateTime _lastExchangeRateDateUtc = DateTime.MinValue;
    private decimal _lastFee;

    private readonly Random _random = new();

    public decimal GetExchangeRate()
    {
        if (_lastExchangeRateDateUtc >= DateTime.Now.AddHours(-1))
        {
            return _exchangeRate;
        }

        _exchangeRate = (decimal)(_random.NextDouble() + _random.NextDouble());
        _lastExchangeRateDateUtc = DateTime.UtcNow;

        return _exchangeRate;
    }

    public decimal GetFee()
    {
        var exchangeRate = GetExchangeRate();

        if (_lastFee == 0.0m)
        {
            _lastFee = exchangeRate;
            return _lastFee;
        }

        _lastFee *= exchangeRate;
        return _lastFee;
    }
}