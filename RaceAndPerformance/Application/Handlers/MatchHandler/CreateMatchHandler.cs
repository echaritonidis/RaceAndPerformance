using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RaceAndPerformance.Application.Commands.MatchCommand;
using RaceAndPerformance.Application.Services.Contracts;

namespace RaceAndPerformance.Application.Handlers.MatchHandler
{
    public class CreateMatchHandler : IRequestHandler<CreateMatchCommand, long>
    {
        private readonly ILogger<CreateMatchHandler> _logger;
        private readonly IMatchService _matchService;

        public CreateMatchHandler(ILogger<CreateMatchHandler> logger, IMatchService matchService)
        {
            _logger = logger;
            _matchService = matchService;
        }

        public async Task<long> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var insertedId = await _matchService.InsertMatch(request.Match, cancellationToken);

            _logger.LogInformation($"Created match with id {insertedId}");

            return insertedId;
        }
    }
}