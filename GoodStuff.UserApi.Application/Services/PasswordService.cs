using System.Security.Cryptography;
using System.Text;
using GoodStuff.UserApi.Application.Services.Interfaces;

namespace GoodStuff.UserApi.Application.Services;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var hash = HashPassword(password);
        return hash == hashedPassword;
    }
}