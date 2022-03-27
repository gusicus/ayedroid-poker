using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.App.App.Controllers
{
    [ApiController]
    [Route("api/v1/Session/{sessionId}/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly ILogger<TopicController> _logger;
        private readonly ISessionService _sessionService;

        public TopicController(ILogger<TopicController> logger, ISessionService sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }

        [Route("")]
        [HttpPost]
        public IActionResult CreateTopic(string sessionId, [FromBody] CreateTopicDto createTopicDto)
        {
            Topic topic = _sessionService.CreateTopic(sessionId, createTopicDto.Title, createTopicDto.Description);

            return Ok(topic.Id);
        }
    }
}