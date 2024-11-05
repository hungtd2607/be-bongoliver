using BongOliver.Models;

namespace BongOliver.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(User user);
        string GetTokenData(string type);
    }
}
