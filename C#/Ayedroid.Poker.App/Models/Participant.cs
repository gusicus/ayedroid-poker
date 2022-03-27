using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;
using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Models
{
    public class Participant
    {
        public ParticipantType Type { get; set; } = ParticipantType.None;

        public string UserId { get; init; } = string.Empty;

        public ParticipantDto ToDto(string name)
        {
            return new ParticipantDto()
            {
                Name = name,
                Type = Type,
                UserId = UserId
            };
        }
    }
}
