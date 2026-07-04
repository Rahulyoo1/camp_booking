using CampBooking.Domain.DTOs.RatingDto;
using FluentValidation;

namespace CampBooking.API.Validators;

public class CreateRatingValidator : AbstractValidator<CreateRatingDto>
{
    public CreateRatingValidator()
    {
        RuleFor(x => x.BookingId)
            .GreaterThan(0);

        RuleFor(x => x.Stars)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.Review)
            .MaximumLength(1000);
    }
}