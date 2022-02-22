using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Models
{
    public class Participant : UniqueEntity
    {
        public Participant(string userName, ParticipantType type) : base(userName)
        {
            Type = type;
        }

        public ParticipantType Type { get; private set; }
    }
}
