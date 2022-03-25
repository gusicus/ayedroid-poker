using Ayedroid.Poker.App.Interfaces;
using Ayedroid.Poker.App.Models;
using Ayedroid.Poker.App.Models.Dto;
using Ayedroid.Poker.App.Models.Enums;
using Ayedroid.Poker.App.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        private readonly ISessionService _sessionService;

        public SessionController(ILogger<SessionController> logger, ISessionService sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
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
            string sessionId = _sessionService.AddSession(startSessionDto.SessionName);

            return Ok(sessionId);
        }

        /// <summary>
        /// Retrieve an existing session
        /// </summary>
        /// <param name="sessionId">Id of the session to get</param>
        /// <returns><see cref="Session"/> if it exists</returns>
        [Route("{sessionId}")]
        [HttpGet]
        public IActionResult GetSession(string sessionId)
        {
            Session? session = _sessionService.GetSession(sessionId);

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
            Session? session = _sessionService.GetSession(sessionId);

            _logger.LogInformation("New user {UserName} joined {Name} ({Id})", joinSessionDto.UserName, session.Name, session.Id);

            _sessionService.JoinSession(sessionId, User.GetUserId(), ParticipantType.Voter);

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
            _sessionService.EndSession(sessionId);

            return Ok();
        }
    }
}