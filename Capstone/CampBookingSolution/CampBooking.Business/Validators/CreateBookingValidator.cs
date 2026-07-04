using CampBooking.Domain.DTOs.BookingDto;
using FluentValidation;

namespace CampBooking.API.Validators;

public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
{
    public CreateBookingValidator()
    {
        RuleFor(x => x.CampId)
            .GreaterThan(0);

        RuleFor(x => x.CheckInDate)
            .LessThan(x => x.CheckOutDate)
            .WithMessage("Check-in date must be before check-out date.");

        RuleFor(x => x.BillingAddress)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d{10}$");
    }
}