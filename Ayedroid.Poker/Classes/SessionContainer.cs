using Ayedroid.Poker.Exceptions;
using Ayedroid.Poker.Interfaces;
using Ayedroid.Poker.Models;

namespace Ayedroid.Poker.Classes
{
    /// <summary>
    /// Handler for all <see cref="Session"/> interaction. A single list is maintained so all controllers can access the same sessions.
    /// </summary>
    public class SessionContainer : ISessionContainer
    {
        private readonly ILogger<SessionContainer> _logger;
        private readonly List<Session> _sessions;

        public SessionContainer(ILogger<SessionContainer> logger)
        {
            _logger = logger;
            _sessions = new();
        }

        /// <summary>
        /// Add a brand new session. 
        /// </summary>
        /// <param name="sessionName">Display name of the session. Does not have to be unique</param>
        /// <returns>Guid of the new session. Required for any further session interaction</returns>
        public Guid AddSession(string sessionName)
        {
            ArgumentNullException.ThrowIfNull(sessionName);

            var session = new Session(sessionName);

            _sessions.Add(session);

            _logger.LogInformation("New session started: {SessionName} ({Id})", session.Name, session.Id);

            return session.Id;
        }

        /// <summary>
        /// End a session (delete it)
        /// </summary>
        /// <param name="sessionId">Guid of the session to end</param>
        public void EndSession(string sessionId)
        {
            ArgumentNullException.ThrowIfNull(sessionId);

            Session session = GetSession(sessionId);

            _logger.LogInformation("Session ended: {Name} ({Id})", session.Name, session.Id);

            _sessions.RemoveAll(s => s.Id.ToString() == session.Id.ToString());
        }

        /// <summary>
        /// Get a session by guid
        /// </summary>
        /// <param name="sessionId">Guid of the session to retrieve</param>
        /// <returns>Session if it exists, null if not</returns>
        public Session GetSession(string sessionId)
        {
            ArgumentNullException.ThrowIfNull(sessionId);

            Session? session = _sessions.FirstOrDefault(s => s.Id.ToString() == sessionId);

            if (session == null)
                throw new SessionNotFoundException();

            return session;
        }
    }
}
