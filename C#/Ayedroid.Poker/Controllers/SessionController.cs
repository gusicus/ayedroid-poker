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

        /// <summary>
        /// Create a new pointing session
        /// </summary>
        /// <param name="startSessionDto">Details required to create a session</param>
        /// <returns>Id of the new session, to be used for all further session interaction</returns>
        [Route("")]
        [HttpPost]
        public IActionResult StartNewSession([FromBody] StartSessionDto startSessionDto)
        {
            Guid guid = _sessionContainer.AddSession(startSessionDto.SessionName);

            return Ok(guid.ToString());
        }

        /// <summary>
        /// Retrieve an existing session
        /// </summary>
        /// <param name="sessionId">Id of the session to get</param>
        /// <returns><see cref="Session"/> if it exists</returns>
        [Route("{sessionId}")]
        [HttpPost]
        public IActionResult GetSession(string sessionId)
        {
            Session? session = _sessionContainer.GetSession(sessionId);

            return Ok(session);
        }

        /// <summary>
        /// Register a new user in <paramref name="sessionId"/>. Their username does not have to be unique.
        /// </summary>
        /// <param name="sessionId">Id of the session to join</param>
        /// <param name="joinSessionDto">Details required to join a session</param>
        /// <returns></returns>
        [Route("{sessionId}/join")]
        [HttpPost]
        public IActionResult JoinSession(string sessionId, [FromBody] JoinSessionDto joinSessionDto)
        {
            Session? session = _sessionContainer.GetSession(sessionId);

            _logger.LogInformation("New user {UserName} joined {Name} ({Id})", joinSessionDto.UserName, session.Name, session.Id);

            session.Participants.Add(new Participant(joinSessionDto.UserName, Models.Enums.ParticipantType.Voter));

            return Ok();
        }
        
        /// <summary>
        /// End <paramref name="sessionId"/>. Once this is done all users will be kicked out and the session will be gone forever.
        /// </summary>
        /// <param name="sessionId">Id of the session to end</param>
        /// <returns></returns>
        [Route("{sessionId}")]
        [HttpDelete]
        public IActionResult EndSession(string sessionId)
        {
            _sessionContainer.EndSession(sessionId);

            return Ok();
        }
    }
}