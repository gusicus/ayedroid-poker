using Ayedroid.Poker.Interfaces;
using Ayedroid.Poker.Models;

namespace Ayedroid.Poker.Classes
{
    public class SessionContainer : ISessionContainer
    {
        private readonly List<Session> _sessions;

        public SessionContainer()
        {
            _sessions = new();
        }

        public Guid AddSession(string sessionName)
        {
            var session = new Session(sessionName);

            _sessions.Add(session);

            return session.Id;
        }

        public void EndSession(string sessionId)
        {
            Session? session = GetSession(sessionId);

            if (session != null)
                _sessions.RemoveAll(s => s.Id.ToString() == sessionId);
        }

        public Session? GetSession(string sessionId)
        {
            return _sessions.FirstOrDefault(s => s.Id.ToString() == sessionId);
        }
    }
}
