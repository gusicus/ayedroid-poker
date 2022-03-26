using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Enums;
using System.Collections.ObjectModel;

namespace Ayedroid.Poker.App.Models
{
    public class Session : UniqueEntity
    {
        private readonly Dictionary<string, Participant> _participants = new();

        public Session(string sessionName, string id) : base(sessionName, id)
        {

        }

        public ReadOnlyCollection<Participant> Participants => _participants.Values.ToList().AsReadOnly();

        public void AddParticipant(User user, ParticipantType participantType)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (_participants.ContainsKey(user.Id))
            {
                throw new ArgumentException($"{user.Id} is already a participant in {Name}");
            }

            _participants[user.Id] = new Participant(user, participantType);
        }

        public bool HasParticipant(string userId)
        {
            return _participants.ContainsKey(userId);
        }
    }
}
