using Ayedroid.Poker.App.Exceptions;
using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Ayedroid.Poker.App.Services
{
    /// <summary>
    /// Handler for all <see cref="Session"/> interaction. A single list is maintained so all controllers can access the same sessions.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly INotificationService _notificationService;

        private readonly Dictionary<string, User> _users = new();

        public UserService(ILogger<UserService> logger, INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        public User AddUser(string userName)
        {
            var user = new User(userName);
            _users[user.Id] = user;

            return user;
        }

        public bool DoesUserExist(string userId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userId)
        {
            return _users[userId];
        }
    }
}
