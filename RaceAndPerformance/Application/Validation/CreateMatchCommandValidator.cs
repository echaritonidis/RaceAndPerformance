using FluentValidation;
using RaceAndPerformance.Application.Commands.MatchCommand;
using RaceAndPerformance.Application.Helpers;

namespace MediatorApiExample.Validation
{
    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        public CreateMatchCommandValidator()
        {
            RuleFor(x => x.Match.Description).NotNull().MinimumLength(12).MaximumLength(256).WithMessage("Description should be between 12 to 256 characters long");
            RuleFor(x => x.Match.MatchDate).Must(ValidatorHelpers.IsValidDate).WithMessage("MatchDate should be a valid format date of dd-MM-yyyy");
            RuleFor(x => x.Match.MatchTime).Must(ValidatorHelpers.IsValidTime).WithMessage("MatchTime should be a valid format time of HH:mm");
            RuleFor(x => x.Match.TeamA).NotNull().MinimumLength(3).MaximumLength(20).WithMessage("TeamA should be between 3 to 20 characters long");
            RuleFor(x => x.Match.TeamB).NotNull().MinimumLength(3).MaximumLength(20).WithMessage("TeamB should be between 3 to 20 characters long");
            RuleFor(x => x.Match.Sport).IsInEnum().WithMessage("Sport should have a valid enum value");
            RuleFor(x => x.Match.MatchOdds).Must(x => x?.Count > 0).WithMessage("Odds should exist and have at least one record");
        }
    }
}