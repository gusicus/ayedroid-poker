namespace Ayedroid.Poker.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Participant>? Participants { get; set; }
    }
}
