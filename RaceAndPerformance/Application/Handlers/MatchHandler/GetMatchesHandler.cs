using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RaceAndPerformance.Application.Commands.MatchCommand;
using RaceAndPerformance.Application.Respones.MatchResponse;
using RaceAndPerformance.Application.Services.Contracts;

namespace RaceAndPerformance.Application.Handlers.MatchHandler
{
    public class GetMatchesHandler : IRequestHandler<GetMatchesCommand, MatchesResponse>
    {
        private readonly ILogger<GetMatchesHandler> _logger;
        private readonly IMatchService _matchService;

        public GetMatchesHandler(ILogger<GetMatchesHandler> logger, IMatchService matchService)
        {
            _logger = logger;
            _matchService = matchService;
        }

        public async Task<MatchesResponse> Handle(GetMatchesCommand request, CancellationToken cancellationToken)
        {
            var matches = await _matchService.GetAllMatches(cancellationToken);

            return new MatchesResponse { Matches = matches };
        }
    }
}