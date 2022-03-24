using Ayedroid.Poker.App.Models;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface IUserService
    {
        User AddUser(string userName);
        User GetUser(string userId);
        bool DoesUserExist(string userId);
    }
}
