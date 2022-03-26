using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Models
{
    public class Participant
    {
        public ParticipantType Type { get; set; } = ParticipantType.None;

        public string UserId { get; init; } = string.Empty;
    }
}
