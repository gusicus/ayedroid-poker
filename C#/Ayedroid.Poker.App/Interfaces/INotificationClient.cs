namespace Ayedroid.Poker.Interfaces
{
    public interface INotificationClient
    {
        Task ParticipantJoined();
        Task ParticipantLeft();
        Task SessionEnded();
    }
}
