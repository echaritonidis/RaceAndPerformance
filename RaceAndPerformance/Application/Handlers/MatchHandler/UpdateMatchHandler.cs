using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RaceAndPerformance.Application.Commands.MatchCommand;
using RaceAndPerformance.Application.Services.Contracts;

namespace RaceAndPerformance.Application.Handlers.MatchHandler
{
    public class UpdateMatchHandler : IRequestHandler<UpdateMatchCommand, long>
    {
        private readonly ILogger<UpdateMatchHandler> _logger;
        private readonly IMatchService _matchService;

        public UpdateMatchHandler(ILogger<UpdateMatchHandler> logger, IMatchService matchService)
        {
            _logger = logger;
            _matchService = matchService;
        }

        public async Task<long> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
        {
            var updatedId = await _matchService.UpdateMatch(request.Match, cancellationToken);

            if (updatedId > 0) _logger.LogInformation($"Match with id {updatedId} updated");

            return updatedId;
        }
    }
}