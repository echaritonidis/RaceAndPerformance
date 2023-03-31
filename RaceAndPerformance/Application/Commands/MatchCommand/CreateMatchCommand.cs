using MediatR;
using RaceAndPerformance.Application.Models.Create;

namespace RaceAndPerformance.Application.Commands.MatchCommand
{
    public class CreateMatchCommand : IRequest<long>
    {
        public CreateMatch Match { get; set; }
    }
}

