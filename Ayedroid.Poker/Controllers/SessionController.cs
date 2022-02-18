using Ayedroid.Poker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;

        private readonly static List<Session> _sessions = new();

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> StartNewSession(string sessionName)
        {
            Guid guid = Guid.NewGuid();

            _sessions.Add(new Session()
            {
                Id = guid,
                Name = sessionName,
                Participants = new List<Participant>()
            });

            return Ok(guid.ToString());
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> GetSession(string id)
        {
            Session session = FindSession(id);

            if (session == null)
            {
                return NotFound();
            }

            return Ok(session);
        }

        [Route("{id}/join")]
        [HttpPost]
        public async Task<IActionResult> JoinSession(string id, [FromBody] JoinSessionDto joinSessionDto)
        {
            Session session = FindSession(id);

            if (session == null)
            {
                return NotFound();
            }

            session.Participants?.Add(new Participant() { Name = joinSessionDto.UserName });

            return Ok();
        }

        private static Session FindSession(string id)
        {
            return _sessions.FirstOrDefault(s => s.Id.ToString() == id);
        }
    }
}