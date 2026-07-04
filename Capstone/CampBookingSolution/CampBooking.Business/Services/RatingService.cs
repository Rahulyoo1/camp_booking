using CampBooking.Business.Interfaces;
using CampBooking.Data.Repositories.Interfaces;
using CampBooking.Domain.DTOs.RatingDto;
using CampBooking.Domain.Entities;
using CampBooking.Domain.Enums;

namespace CampBooking.Business.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository ratingRepository;
    private readonly IBookingRepository bookingRepository;

    public RatingService(
        IRatingRepository ratingRepository,
        IBookingRepository bookingRepository)
    {
        this.ratingRepository = ratingRepository;
        this.bookingRepository = bookingRepository;
    }

    public async Task<RatingResponseDto> AddOrUpdateRatingAsync(
     string userId,
     CreateRatingDto dto)
    {
        // 1. Validate Stars
        if (dto.Stars < 1 || dto.Stars > 5)
        {
            throw new Exception("Rating must be between 1 and 5.");
        }

        // 2. Check Booking Exists
        var booking = await bookingRepository.GetByIdAsync(dto.BookingId);

        if (booking == null)
        {
            throw new Exception("Booking not found.");
        }

        // 3. Check Booking Owner
        if (booking.UserId != userId)
        {
            throw new Exception("You are not authorized to rate this booking.");
        }

        // 4. Booking must be Confirmed
        if (booking.Status != BookingStatus.Confirmed)
        {
            throw new Exception("Only confirmed bookings can be rated.");
        }

        // 5. Stay must be completed
        if (booking.CheckOutDate >= DateOnly.FromDateTime(DateTime.Today))
        {
            throw new Exception("You can rate only after your stay is completed.");
        }

        // 6. Check Existing Rating
        var existingRating = await ratingRepository.GetByBookingIdAsync(dto.BookingId);

        if (existingRating == null)
        {
            // Create Rating
            var rating = new Rating
            {
                Stars = dto.Stars,
                Review = dto.Review,
                BookingId = booking.Id,
                CampId = booking.CampId,
                UserId = userId,
                RatedOn = DateTime.UtcNow
            };

            await ratingRepository.AddAsync(rating);
            await ratingRepository.SaveChangesAsync();

            return new RatingResponseDto
            {
                Id = rating.Id,
                Stars = rating.Stars,
                Review = rating.Review,
                RatedOn = rating.RatedOn,
                CampName = booking.Camp.Name
            };
        }

        // Update Existing Rating
        existingRating.Stars = dto.Stars;
        existingRating.Review = dto.Review;
        existingRating.RatedOn = DateTime.UtcNow;

        await ratingRepository.UpdateAsync(existingRating);
        await ratingRepository.SaveChangesAsync();

        return new RatingResponseDto
        {
            Id = existingRating.Id,
            Stars = existingRating.Stars,
            Review = existingRating.Review,
            RatedOn = existingRating.RatedOn,
            CampName = existingRating.Camp.Name
        };
    }
}