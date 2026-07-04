using CampBooking.Domain.DTOs.CampDto;
using FluentValidation;

namespace CampBooking.API.Validators;

public class CreateCampValidator : AbstractValidator<CreateCampDto>
{
    public CreateCampValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Location)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Capacity)
            .GreaterThan(0);

        RuleFor(x => x.WeekdayRate)
            .GreaterThan(0);

        RuleFor(x => x.WeekendRate)
            .GreaterThan(0);
    }
}