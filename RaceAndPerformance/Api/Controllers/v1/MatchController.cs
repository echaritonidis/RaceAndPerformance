using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaceAndPerformance.Application.Commands.MatchCommand;
using System.Threading.Tasks;

namespace RaceAndPerformance.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MatchController : ControllerBase
    {
        private readonly ILogger<MatchController> _logger;
        private readonly IMediator _mediator;

        public MatchController(ILogger<MatchController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("get-all", Name = "GetAllMatches")]
        public async Task<IActionResult> GetAll(GetMatchesCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result.Matches);
        }

        [HttpGet("get-match", Name = "GetMatch")]
        public async Task<IActionResult> Get(GetMatchCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result.Match);
        }

        [HttpPost("create", Name = "CreateMatch")]
        public async Task<IActionResult> Create(CreateMatchCommand command)
        {
            if (command.Match is null) return NoContent();

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("update", Name = "UpdateMatch")]
        public async Task<IActionResult> Update(UpdateMatchCommand command)
        {
            if (command.Match is null) return NoContent();

            var modifiedId = await _mediator.Send(command);

            if (modifiedId is 0) return NoContent();

            return Ok(modifiedId);
        }

        [HttpDelete("delete", Name = "DeleteMatch")]
        public async Task<IActionResult> Delete(DeleteMatchCommand command)
        {
            var deleted = await _mediator.Send(command);

            return Ok(deleted);
        }
    }
}