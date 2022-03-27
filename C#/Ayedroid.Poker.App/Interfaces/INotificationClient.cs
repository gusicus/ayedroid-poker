using Ayedroid.Poker.App.Models.Dto;

namespace Ayedroid.Poker.App.Interfaces
{
    public interface INotificationClient
    {
        Task ParticipantJoined(string sessionId, ParticipantDto participant);
        Task ParticipantLeft(string sessionId, ParticipantDto participant);
        Task SessionEnded();
    }
}
