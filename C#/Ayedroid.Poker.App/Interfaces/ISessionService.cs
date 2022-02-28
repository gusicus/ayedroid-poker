using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface ISessionService
    {
        Guid AddSession(string sessionName);
        Session GetSession(string sessionId);
        void EndSession(string sessionId);
        void JoinSession(string sessionId, string userId, ParticipantType participantType);
    }
}
