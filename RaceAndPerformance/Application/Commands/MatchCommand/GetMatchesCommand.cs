using MediatR;
using RaceAndPerformance.Application.Respones.MatchResponse;

namespace RaceAndPerformance.Application.Commands.MatchCommand
{
    public class GetMatchesCommand : IRequest<MatchesResponse>
    {
    }
}

