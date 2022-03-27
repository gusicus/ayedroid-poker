using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.App.App.Controllers
{
    [ApiController]
    [Route("api/v1/Session/{sessionId}/{topicId}/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly ILogger<VoteController> _logger;
        private readonly ISessionService _sessionService;

        public VoteController(ILogger<VoteController> logger, ISessionService sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }

        [Route("")]
        [HttpPost]
        public IActionResult StartVote(string sessionId)
        {
            throw new NotImplementedException();
        }

        [Route("")]
        [HttpPut]
        public IActionResult CastVote(string sessionId, string topicId, string sizeId)
        {
            _sessionService.CastVote(sessionId, topicId, User.GetUserId(), sizeId);
            return Ok();
        }
    }
}