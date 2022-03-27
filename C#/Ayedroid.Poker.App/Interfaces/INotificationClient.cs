using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface INotificationClient
    {
        Task ParticipantJoined(string sessionId, ParticipantDto participant);
        Task ParticipantLeft(string sessionId, ParticipantDto participant);
        Task NewTopic(string sessionId, Topic topic);
        Task NewTopicVote(string sessionId, string topicId, string userId, string sizeId);
        Task SessionEnded();
    }
}
