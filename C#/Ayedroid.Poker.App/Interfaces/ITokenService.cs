using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface ITokenService
    {
        TokenDto GenerateToken(User user);

        TokenDto RefreshToken(string refreshToken);
    }
}
