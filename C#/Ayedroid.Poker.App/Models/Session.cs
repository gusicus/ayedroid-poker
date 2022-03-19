﻿using Ayedroid.Poker.App.Models.Enums;

namespace Ayedroid.Poker.App.Models
{
    public class Session : UniqueEntity
    {
        private Dictionary<string, Participant> _participants = new();

        public Session(string sessionName) : base(sessionName)
        {

        }

        public void AddParticipant(User user, ParticipantType participantType)
        {
            ArgumentNullException.ThrowIfNull(user);

            if (_participants.ContainsKey(user.Id.ToString()))
            {
                throw new ArgumentException($"{user.Id} is already a participant in {Name}");
            }

            _participants[user.Id.ToString()] = new Participant(user, participantType);
        }
    }
}