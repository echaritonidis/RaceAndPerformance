using MediatR;
using RaceAndPerformance.Application.Models.Update;

namespace RaceAndPerformance.Application.Commands.MatchCommand
{
    public class UpdateMatchCommand : IRequest<long>
    {
        public UpdateMatch Match { get; set; }
    }
}