using MediatR;

namespace RaceAndPerformance.Application.Commands.MatchCommand
{
    public class DeleteMatchCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}

