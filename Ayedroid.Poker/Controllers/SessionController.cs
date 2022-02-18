using Ayedroid.Poker.Interfaces;
using Ayedroid.Poker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        private readonly ISessionContainer _sessionContainer;

        public SessionController(ILogger<SessionController> logger, ISessionContainer sessionContainer)
        {
            _logger = logger;
            _sessionContainer = sessionContainer;
        }

        [Route("")]
        [HttpPost]
        public IActionResult StartNewSession([FromBody] StartSessionDto startSessionDto)
        {
            Guid guid = _sessionContainer.AddSession(startSessionDto.SessionName);

            _logger.LogInformation("New session started: {SessionName} ({guid})", startSessionDto.SessionName, guid);

            return Ok(guid.ToString());
        }

        [Route("{sessionId}")]
        [HttpPost]
        public IActionResult GetSession(string sessionId)
        {
            Session? session = _sessionContainer.GetSession(sessionId);

            if (session == null)
            {
                return NotFound();
            }

            return Ok(session);
        }

        [Route("{sessionId}/join")]
        [HttpPost]
        public IActionResult JoinSession(string sessionId, [FromBody] JoinSessionDto joinSessionDto)
        {
            Session? session = _sessionContainer.GetSession(sessionId);

            if (session == null)
            {
                return NotFound();
            }

            _logger.LogInformation("New user {UserName} joined {Name} ({Id})", joinSessionDto.UserName, session.Name, session.Id);

            session.Participants.Add(new Participant() { Name = joinSessionDto.UserName });

            return Ok();
        }

        [Route("{sessionId}")]
        [HttpDelete]
        public IActionResult EndSession(string sessionId)
        {
            Session? session = _sessionContainer.GetSession(sessionId);

            if (session == null)
            {
                return NotFound();
            }

            _logger.LogInformation("Session ended: {SessionName} ({Id})", session.Name, session.Id);

            _sessionContainer.EndSession(sessionId);

            return Ok();
        }
    }
}