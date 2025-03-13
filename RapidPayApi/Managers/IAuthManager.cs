namespace RapidPayApi.Managers;

public interface IAuthManager
{
    public string Login(string userName, string password);
}