using Ayedroid.Poker.App.Models;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
