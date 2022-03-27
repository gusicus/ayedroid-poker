using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;
using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface ISessionService
    {
        string AddSession(string sessionName, IEnumerable<string> sizes);
        SessionDto GetSessionDto(string sessionId);
        void EndSession(string sessionId);
        void JoinSession(string sessionId, string userId, ParticipantType participantType);
        Topic CreateTopic(string sessionId, string title, string description);
        void CastVote(string sessionId, string topicId, string userId, string sizeId);
    }
}
