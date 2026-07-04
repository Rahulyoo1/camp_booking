using AutoMapper;
using CampBooking.Business.Mappings;
using CampBooking.Business.Services;
using CampBooking.Data.Repositories.Interfaces;
using CampBooking.Domain.DTOs.BookingDto;
using CampBooking.Domain.Entities;
using CampBooking.Domain.Enums;
using Moq;

namespace CampBooking.Tests;

public class BookingServiceTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task CreateBookingAsync_ShouldThrow_WhenCheckInDateIsNotBeforeCheckOutDate()
    {
        var bookingRepository = new Mock<IBookingRepository>();
        var campRepository = new Mock<ICampRepository>();
        var service = new BookingService(bookingRepository.Object, campRepository.Object, CreateMapper());

        var dto = new CreateBookingDto
        {
            CampId = 1,
            CheckInDate = new DateOnly(2026, 7, 10),
            CheckOutDate = new DateOnly(2026, 7, 10),
            BillingAddress = "Address",
            PhoneNumber = "9999999999"
        };

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateBookingAsync("user-1", dto));
    }

    [Fact]
    public async Task CreateBookingAsync_ShouldThrow_WhenCampDoesNotExist()
    {
        var bookingRepository = new Mock<IBookingRepository>();
        var campRepository = new Mock<ICampRepository>();
        campRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Camp?)null);

        var service = new BookingService(bookingRepository.Object, campRepository.Object, CreateMapper());

        var dto = new CreateBookingDto
        {
            CampId = 1,
            CheckInDate = new DateOnly(2026, 7, 10),
            CheckOutDate = new DateOnly(2026, 7, 12),
            BillingAddress = "Address",
            PhoneNumber = "9999999999"
        };

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateBookingAsync("user-1", dto));
    }

    [Fact]
    public async Task CreateBookingAsync_ShouldCreateBooking_WhenDataIsValid()
    {
        var bookingRepository = new Mock<IBookingRepository>();
        var campRepository = new Mock<ICampRepository>();

        var camp = new Camp
        {
            Id = 1,
            Name = "Hills Camp",
            WeekdayRate = 100,
            WeekendRate = 200
        };

        campRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(camp);
        bookingRepository.Setup(x => x.IsCampAvailableAsync(1, It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).ReturnsAsync(true);

        Booking? createdBooking = null;
        bookingRepository.Setup(x => x.AddAsync(It.IsAny<Booking>())).Callback<Booking>(b => createdBooking = b).Returns(Task.CompletedTask);
        bookingRepository.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        var service = new BookingService(bookingRepository.Object, campRepository.Object, CreateMapper());

        var dto = new CreateBookingDto
        {
            CampId = 1,
            CheckInDate = new DateOnly(2026, 7, 3),
            CheckOutDate = new DateOnly(2026, 7, 6),
            BillingAddress = "Address",
            PhoneNumber = "9999999999"
        };

        var result = await service.CreateBookingAsync("user-1", dto);

        Assert.NotNull(createdBooking);
        Assert.Equal("user-1", createdBooking!.UserId);
        Assert.Equal(BookingStatus.Confirmed, createdBooking.Status);
        Assert.Equal(8, createdBooking.BookingReference.Length);

        decimal expectedTotal = 0;
        for (var date = dto.CheckInDate; date < dto.CheckOutDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                expectedTotal += camp.WeekendRate;
            }
            else
            {
                expectedTotal += camp.WeekdayRate;
            }
        }

        Assert.Equal(expectedTotal, result.TotalAmount);
        bookingRepository.Verify(x => x.AddAsync(It.IsAny<Booking>()), Times.Once);
        bookingRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task CancelBookingAsync_ShouldThrow_WhenBookingIsNotFutureBooking()
    {
        var bookingRepository = new Mock<IBookingRepository>();
        var campRepository = new Mock<ICampRepository>();

        var booking = new Booking
        {
            BookingReference = "BKTEST01",
            Status = BookingStatus.Confirmed,
            CheckInDate = DateOnly.FromDateTime(DateTime.Today)
        };

        bookingRepository.Setup(x => x.GetByReferenceAsync("BKTEST01")).ReturnsAsync(booking);

        var service = new BookingService(bookingRepository.Object, campRepository.Object, CreateMapper());

        await Assert.ThrowsAsync<Exception>(() => service.CancelBookingAsync("BKTEST01"));
        bookingRepository.Verify(x => x.UpdateAsync(It.IsAny<Booking>()), Times.Never);
    }
}
