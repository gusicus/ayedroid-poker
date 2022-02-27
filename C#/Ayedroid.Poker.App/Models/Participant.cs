using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Models
{
    public class Participant
    {
        public Participant(User user, ParticipantType participantType)
        {
            User = user;
            Type = participantType;
        }

        public ParticipantType Type { get; private set; }

        public User User { get; private set; }
    }
}
