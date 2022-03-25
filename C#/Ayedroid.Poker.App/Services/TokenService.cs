using Ayedroid.Poker.App.Exceptions;
using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Ayedroid.Poker.App.Services
{
    /// <summary>
    /// Handler for all token interaction
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly TokenAuthOptions _tokenOptions;
        private Dictionary<string, User> _refreshTokens;

        public TokenService(ILogger<TokenService> logger, TokenAuthOptions tokenOptions)
        {
            _logger = logger;
            _tokenOptions = tokenOptions;
            _refreshTokens = new();
        }

        public TokenDto GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new(new GenericIdentity(user.Name, "TokenAuth"), new[] { new Claim(ClaimTypes.NameIdentifier, user.Id, ClaimValueTypes.String) });

            DateTime tokenExpiry = DateTime.UtcNow.AddMinutes(30);
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                SigningCredentials = _tokenOptions.SigningCredentials,
                Subject = identity,
                Expires = tokenExpiry
            });

            string refreshToken = Guid.NewGuid().ToString("N");
            _refreshTokens.Add(refreshToken, user);

            return new TokenDto()
            {
                Expires = tokenExpiry,
                RefreshToken = refreshToken,
                Token = handler.WriteToken(securityToken)
            };
        }

        public TokenDto RefreshToken(string refreshToken)
        {
            if (!_refreshTokens.ContainsKey(refreshToken))
                throw new InvalidOperationException("Refresh token is invalid");

            User user = _refreshTokens[refreshToken];
            _refreshTokens.Remove(refreshToken);
            return GenerateToken(user);
        }
    }
}
