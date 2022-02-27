using Ayedroid.Poker.App.Exceptions;
using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;

namespace Ayedroid.Poker.App.Services
{
    /// <summary>
    /// Handler for all <see cref="Session"/> interaction. A single list is maintained so all controllers can access the same sessions.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly INotificationService _notificationService;
        private readonly ISessionService _sessionService;

        private readonly Dictionary<string, User> _users = new();

        public UserService(ILogger<UserService> logger, INotificationService notificationService, ISessionService sessionService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _sessionService = sessionService;
        }

        public User AddUser(string userName)
        {
            var user = new User(userName);
            _users[user.Id.ToString()] = user;

            return user;
        }

        public User GetUser(string userId)
        {
            return _users[userId];
        }
    }
}
