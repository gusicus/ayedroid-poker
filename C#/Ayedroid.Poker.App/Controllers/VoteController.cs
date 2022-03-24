using Microsoft.AspNetCore.Mvc;

namespace Ayedroid.Poker.App.App.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly ILogger<VoteController> _logger;

        public VoteController(ILogger<VoteController> logger)
        {
            _logger = logger;
        }
    }
}