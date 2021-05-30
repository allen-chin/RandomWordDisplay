using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomWordDisplay.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RandomWordDisplay.API.Controllers
{
    [Route("api/State")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ILogger<StartController> _logger;
        private readonly IRandomWordService _randomWordService;

        public StateController(ILogger<StartController> logger, IRandomWordService randomWordService)
        {
            _logger = logger;
            _randomWordService = randomWordService;
        }

        // GET: api/<StateController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                _randomWordService.CommandRunning,
                _randomWordService.CommandTimeRemaining,
                _randomWordService.CurrentWordSelected
            });
        }
    }
}