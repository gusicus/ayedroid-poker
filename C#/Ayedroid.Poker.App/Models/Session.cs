using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Enums;
using System.Collections.ObjectModel;

namespace Ayedroid.Poker.App.Models
{
    public class Session : UniqueEntity
    {
        private readonly Dictionary<string, Participant> _participants = new();
        public ReadOnlyCollection<Participant> Participants => _participants.Values.ToList().AsReadOnly();

        private readonly Dictionary<string, Topic> _topics = new();
        public ReadOnlyCollection<Topic> Topics => _topics.Values.OrderByDescending(t => t.Order).ToList().AsReadOnly();

        private readonly Dictionary<string, Size> _sizes = new();
        public ReadOnlyCollection<Size> Sizes => _sizes.Values.ToList().AsReadOnly();

        public Session(string sessionName, string id, IEnumerable<string> sizes) : base(sessionName, id)
        {
            foreach (string sizeName in sizes)
            {
                Size size = new(sizeName);
                _sizes[size.Id] = size;
            }
        }

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

        public Topic CreateTopic(string title, string description)
        {
            Topic topic = new(title)
            {
                Description = description,
                Order = _topics.Count
            };

            _topics[topic.Id] = topic;

            return topic;
        }

        public void CastVote(string topicId, string userId, string sizeId)
        {
            Topic topic = _topics[topicId];

            topic.Votes[userId] = _sizes[sizeId];
        }
    }
}
