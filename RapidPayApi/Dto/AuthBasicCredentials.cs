using System.Text;

namespace RapidPayApi.Dto;

public record AuthBasicCredentials
{
    public string Username { get; }
    public string Password { get; }

    private AuthBasicCredentials(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public bool Equals(string? username, string? password) =>
        !string.IsNullOrWhiteSpace(username) &&
        !string.IsNullOrWhiteSpace(password) &&
        Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
        Password == password;

    public virtual bool Equals(AuthBasicCredentials? other) =>
        other != null &&
        Username.Equals(other.Username, StringComparison.OrdinalIgnoreCase) &&
        Password == other.Password;

    public override int GetHashCode() => HashCode.Combine(Username, Password);

    public static bool TryParse(string input, out AuthBasicCredentials credentials)
    {
        credentials = null!;
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        try
        {
            var values = Encoding.UTF8.GetString(Convert.FromBase64String(input)).Split(':', 2);
            credentials = new AuthBasicCredentials(values[0], values[1]);
            return true;
        }
        catch
        {
            return false;
        }
    }
}