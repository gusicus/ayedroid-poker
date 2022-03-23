using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Models
{
    public class Session : UniqueEntity
    {
        private readonly Dictionary<string, Participant> _participants = new();

        public Session(string sessionName, string id) : base(sessionName, id)
        {

        }

        public void AddParticipant(User user, ParticipantType participantType)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (_participants.ContainsKey(user.Id))
            {
                throw new ArgumentException($"{user.Id} is already a participant in {Name}");
            }

            _participants[user.Id] = new Participant(user, participantType);
        }
    }
}
