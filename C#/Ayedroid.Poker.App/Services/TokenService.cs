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
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly TokenAuthOptions _tokenOptions;

        public TokenService(ILogger<TokenService> logger, TokenAuthOptions tokenOptions)
        {
            _logger = logger;
            _tokenOptions = tokenOptions;
        }

        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new(new GenericIdentity(user.Name, "TokenAuth"), new[] { new Claim(ClaimTypes.NameIdentifier, user.Id, ClaimValueTypes.String) });

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
    }
}
