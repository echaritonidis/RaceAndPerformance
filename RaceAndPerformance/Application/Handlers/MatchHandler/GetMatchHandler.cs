using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RaceAndPerformance.Application.Commands.MatchCommand;
using RaceAndPerformance.Application.Respones.MatchResponse;
using RaceAndPerformance.Application.Services.Contracts;

namespace RaceAndPerformance.Application.Handlers.MatchHandler
{
    public class GetMatchHandler : IRequestHandler<GetMatchCommand, MatchResponse>
    {
        private readonly ILogger<GetMatchHandler> _logger;
        private readonly IMatchService _matchService;

        public GetMatchHandler(ILogger<GetMatchHandler> logger, IMatchService matchService)
        {
            _logger = logger;
            _matchService = matchService;
        }

        public async Task<MatchResponse> Handle(GetMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchService.GetMatch(request.Id, cancellationToken);

            if (match is not null) _logger.LogInformation($"Get customer with id {match.Id}");

            return new MatchResponse { Match = match };
        }
    }
}