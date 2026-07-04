using CampBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Data.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        Task<Rating?> GetByBookingIdAsync(int bookingId);

        Task AddAsync(Rating rating);

        Task UpdateAsync(Rating rating);

        Task SaveChangesAsync();
    }
}
