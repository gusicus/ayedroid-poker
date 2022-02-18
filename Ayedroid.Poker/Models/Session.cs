namespace Ayedroid.Poker.Models
{
    public class Session
    {
        public Session(string sessionName)
        {
            Name = sessionName;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; private set; }
        public List<Participant> Participants { get; } = new List<Participant>();
    }
}
