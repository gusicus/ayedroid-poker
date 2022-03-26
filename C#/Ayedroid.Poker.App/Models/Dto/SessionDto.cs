namespace Ayedroid.Poker.App.Models.Dto
{
    public class SessionDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ParticipantDto[] Participants { get; set; } = Array.Empty<ParticipantDto>();
    }

    public class ParticipantDto : Participant
    {
        public string Name { get; set; } = string.Empty;
    }
}
