using CampBooking.Business.Services;
using CampBooking.Data.Repositories.Interfaces;
using CampBooking.Domain.DTOs.RatingDto;
using CampBooking.Domain.Entities;
using CampBooking.Domain.Enums;
using Moq;

namespace CampBooking.Tests;

public class RatingServiceTests
{
    [Fact]
    public async Task AddOrUpdateRatingAsync_ShouldCreateRating_WhenBookingIsValid()
    {
        var ratingRepository = new Mock<IRatingRepository>();
        var bookingRepository = new Mock<IBookingRepository>();

        var booking = new Booking
        {
            Id = 1,
            UserId = "user-1",
            CampId = 2,
            Status = BookingStatus.Confirmed,
            CheckOutDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
            Camp = new Camp { Name = "Lake Camp" }
        };

        bookingRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(booking);
        ratingRepository.Setup(x => x.GetByBookingIdAsync(1)).ReturnsAsync((Rating?)null);
        ratingRepository.Setup(x => x.AddAsync(It.IsAny<Rating>())).Returns(Task.CompletedTask);
        ratingRepository.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        var service = new RatingService(ratingRepository.Object, bookingRepository.Object);

        var dto = new CreateRatingDto
        {
            BookingId = 1,
            Stars = 5,
            Review = "Great stay"
        };

        var result = await service.AddOrUpdateRatingAsync("user-1", dto);

        Assert.Equal(5, result.Stars);
        Assert.Equal("Great stay", result.Review);
        Assert.Equal("Lake Camp", result.CampName);

        ratingRepository.Verify(x => x.AddAsync(It.IsAny<Rating>()), Times.Once);
        ratingRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
