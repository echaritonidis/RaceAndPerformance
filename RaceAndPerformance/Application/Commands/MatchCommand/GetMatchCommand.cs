using MediatR;
using RaceAndPerformance.Application.Respones.MatchResponse;

namespace RaceAndPerformance.Application.Commands.MatchCommand
{
    public class GetMatchCommand : IRequest<MatchResponse>
    {
        public long Id { get; set; }
    }
}