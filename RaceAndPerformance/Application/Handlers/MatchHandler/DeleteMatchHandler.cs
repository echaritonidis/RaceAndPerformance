using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RaceAndPerformance.Application.Commands.MatchCommand;
using RaceAndPerformance.Application.Services.Contracts;

namespace RaceAndPerformance.Application.Handlers.MatchHandler
{
    public class DeleteMatchHandler : IRequestHandler<DeleteMatchCommand, bool>
    {
        private readonly ILogger<DeleteMatchHandler> _logger;
        private readonly IMatchService _matchService;

        public DeleteMatchHandler(ILogger<DeleteMatchHandler> logger, IMatchService matchService)
        {
            _logger = logger;
            _matchService = matchService;
        }

        public async Task<bool> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var deleteResult = await _matchService.DeleteMatch(request.Id, cancellationToken);

            _logger.LogInformation($"Match deleted with id {request.Id}");

            return deleteResult;
        }
    }
}