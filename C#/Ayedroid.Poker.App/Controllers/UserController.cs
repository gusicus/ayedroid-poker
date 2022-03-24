using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.App.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Route("")]
        [HttpPost]
        public IActionResult NewUser([FromBody] NewUserDto newUserDto)
        {
            User user = _userService.AddUser(newUserDto.UserName);
            string token = _userService.GenerateToken(user);
 
            return Ok(token);
        }
    }
}