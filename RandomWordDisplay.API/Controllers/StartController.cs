using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomWordDisplay.API.Services;
using System;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RandomWordDisplay.API.Controllers
{
    [Route("api/Start")]
    [ApiController]
    public class StartController : ControllerBase
    {
        private readonly ILogger<StartController> _logger;
        private readonly IRandomWordService _randomWordService;

        public StartController(ILogger<StartController> logger, IRandomWordService randomWordService)
        {
            _logger = logger;
            _randomWordService = randomWordService;
        }

        // POST api/<StartController>
        [HttpPost]
        public IActionResult Post([FromBody] string[] wordList)
        {
            bool success = _randomWordService.Configure(wordList);

            if (success)
            {
                _randomWordService.StartAsync(new CancellationToken());
                return Ok();
            }

            return BadRequest("Command is still running.");
        }
    }
}