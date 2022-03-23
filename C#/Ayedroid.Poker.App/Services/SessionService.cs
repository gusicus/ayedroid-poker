using Ayedroid.Poker.App.Exceptions;
using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Enums;
using RandomFriendlyNameGenerator;

namespace Ayedroid.Poker.App.Services
{
    /// <summary>
    /// Handler for all <see cref="Session"/> interaction. A single list is maintained so all controllers can access the same sessions.
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly ILogger<SessionService> _logger;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        private readonly List<Session> _sessions = new();

        public SessionService(ILogger<SessionService> logger, INotificationService notificationService, IUserService userService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _userService = userService;
        }

        /// <summary>
        /// Add a brand new session. 
        /// </summary>
        /// <param name="sessionName">Display name of the session. Does not have to be unique</param>
        /// <returns>Guid of the new session. Required for any further session interaction</returns>
        public string AddSession(string sessionName)
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

            _sessions.RemoveAll(s => s.Id == session.Id);

            _notificationService.SessionEnded();
        }

        /// <summary>
        /// Get a session by guid
        /// </summary>
        /// <param name="sessionId">Guid of the session to retrieve</param>
        /// <returns>Session if it exists, null if not</returns>
        public Session GetSession(string sessionId)
        {
            ArgumentNullException.ThrowIfNull(sessionId);

            Session? session = _sessions.FirstOrDefault(s => s.Id == sessionId);

            if (session == null)
                throw new SessionNotFoundException();

            return session;
        }

        public void JoinSession(string sessionId, string userId, ParticipantType participantType)
        {
            ArgumentNullException.ThrowIfNull(sessionId);
            ArgumentNullException.ThrowIfNull(userId);

            var session = GetSession(sessionId);
            var user = _userService.GetUser(userId);
            session.AddParticipant(user, participantType);
        }
    }
}
