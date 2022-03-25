using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public TokenController(ILogger<TokenController> logger, IUserService userService, ITokenService tokenService)
        {
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [Route("")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            User user = _userService.AddUser(loginDto.UserName);
            TokenDto token = _tokenService.GenerateToken(user);

            return Ok(token);
        }

        [AllowAnonymous]
        [Route("Refresh")]
        [HttpPost]
        public IActionResult RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            TokenDto token = _tokenService.RefreshToken(refreshTokenDto.RefreshToken);

            return Ok(token);
        }
    }
}