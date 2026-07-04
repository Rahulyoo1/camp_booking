using CampBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Data.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);     // to create booking

        Task<Booking?> GetByIdAsync(int id);     // used for rating purpose

        Task<Booking?> GetByReferenceAsync(string bookingReference);       // used of ssearching booking by reference

        Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);   

        Task<bool> IsCampAvailableAsync(           // to prevent overlapping bookings for the same camp
            int campId,
            DateOnly checkIn,
            DateOnly checkOut);

        Task UpdateAsync(Booking booking);               // needed for cancel booking

        Task<List<Booking>> GetBookingsByUserIdAsync(string userId);

        Task<(int total, int active, int cancelled, double avgRating)> GetStatsAsync();

        Task SaveChangesAsync();
    }
}
