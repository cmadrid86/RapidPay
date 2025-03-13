namespace RapidPayApi.Engines;

public interface ITokenEngine
{
    public string GenerateToken(string userName);
}