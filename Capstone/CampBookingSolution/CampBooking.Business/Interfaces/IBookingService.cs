using CampBooking.Domain.DTOs.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Business.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(string userId, CreateBookingDto dto);

        Task<BookingDetailsDto?> GetBookingByReferenceAsync( string bookingReference);

        Task<List<MyBookingDto>> GetMyBookingsAsync(string userId);

        Task CancelBookingAsync(string bookingReference);

        Task<(int total, int active, int cancelled, double avgRating)> GetBookingStatsAsync();
    }
}
