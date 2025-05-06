

using WebAPI.Entities;

namespace WebAPI.Service.Token
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}