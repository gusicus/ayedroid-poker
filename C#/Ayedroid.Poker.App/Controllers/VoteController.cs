using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.App.App.Controllers
{
    [ApiController]
    [Route("api/v1/{sessionId}/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly ILogger<VoteController> _logger;

        public VoteController(ILogger<VoteController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpPost]
        public IActionResult StartVote(string sessionId)
        {
            throw new NotImplementedException();
        }

        [Route("{size}")]
        [HttpPost]
        public IActionResult CastVote(string sessionId, string size)
        {
            throw new NotImplementedException();
        }
    }
}