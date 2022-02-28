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
        private readonly TokenAuthOptions _tokenOptions;

        private readonly Dictionary<string, User> _users = new();

        public UserService(ILogger<UserService> logger, INotificationService notificationService, TokenAuthOptions tokenOptions)
        {
            _logger = logger;
            _notificationService = notificationService;
            _tokenOptions = tokenOptions;
        }

        public User AddUser(string userName)
        {
            var user = new User(userName);
            _users[user.Id.ToString()] = user;

            return user;
        }

        public bool DoesUserExist(string userId)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new(new GenericIdentity(user.Name, "TokenAuth"), new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String) });

            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                SigningCredentials = _tokenOptions.SigningCredentials,
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(3)
            });

            return handler.WriteToken(securityToken);
        }

        public User GetUser(string userId)
        {
            return _users[userId];
        }
    }
}
