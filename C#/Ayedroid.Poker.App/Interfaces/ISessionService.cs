using Ayedroid.Poker.Models;

namespace Ayedroid.Poker.Interfaces
{
    public interface ISessionService
    {
        Guid AddSession(string sessionName);
        Session GetSession(string sessionId);
        void EndSession(string sessionId);
    }
}
