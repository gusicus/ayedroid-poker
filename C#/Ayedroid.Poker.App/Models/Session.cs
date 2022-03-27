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

        public Participant AddParticipant(string userId, ParticipantType participantType)
        {
            if (_participants.ContainsKey(userId))
                throw new ArgumentException($"{userId} is already a participant in {Name}");

            Participant participant = new()
            {
                Type = participantType,
                UserId = userId
            };

            _participants[userId] = participant;

            return participant;
        }

        public Participant GetParticipant(string userId)
        {
            if (!HasParticipant(userId))
                throw new KeyNotFoundException($"{userId} is not part of {Name}");

            return _participants[userId];
        }

        public bool HasParticipant(string userId)
        {
            return _participants.ContainsKey(userId);
        }
    }
}
