using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

            return Ok(user.Id.ToString());
        }
    }
}