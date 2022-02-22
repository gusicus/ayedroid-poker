using Ayedroid.Poker.App.Models;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface ISessionService
    {
        Guid AddSession(string sessionName);
        Session GetSession(string sessionId);
        void EndSession(string sessionId);
    }
}
