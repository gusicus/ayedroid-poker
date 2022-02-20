using Ayedroid.Poker.Models.Enums;

namespace Ayedroid.Poker.Models
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
