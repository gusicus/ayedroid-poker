using Ayedroid.Poker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly ILogger<VoteController> _logger;

        public VoteController(ILogger<VoteController> logger)
        {
            _logger = logger;
        }
    }
}