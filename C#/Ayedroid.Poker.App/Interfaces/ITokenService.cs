using Ayedroid.Poker.App.Models;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface ITokenService
    {
        TokenDto GenerateToken(User user);

        TokenDto RefreshToken(string refreshToken);
    }
}
