namespace Ayedroid.Poker.App.Interfaces
{
    public interface INotificationClient
    {
        Task ParticipantJoined();
        Task ParticipantLeft();
        Task SessionEnded();
    }
}
