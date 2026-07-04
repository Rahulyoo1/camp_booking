using CampBooking.Data.Context;
using CampBooking.Data.Repositories.Interfaces;
using CampBooking.Domain.Entities;
using CampBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Data.Repositories
{
    public class BookingRepository: IBookingRepository
    {
        private readonly CampBookingDb context;

        public BookingRepository(CampBookingDb context)
        {
            this.context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await context.Bookings.AddAsync(booking);
        }
    
        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await context.Bookings
                .Include(b=>b.Camp)
                .Include(b=>b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking?> GetByReferenceAsync(string bookingReference)
        {
            return  await context.Bookings
                .Include(e=>e.Camp)
                .Include(e=>e.User)
                .FirstOrDefaultAsync(e=>e.BookingReference == bookingReference);
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {
            return await context.Bookings
                .Include(b => b.Camp)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

       
        public async Task UpdateAsync(Booking booking)
        {
            context.Bookings.Update(booking);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsCampAvailableAsync(int campId , DateOnly checkIn, DateOnly checkOut)
        {
            var hasOverlap = await context.Bookings
                .AnyAsync(b => b.CampId == campId && b.Status == BookingStatus.Confirmed && checkIn < b.CheckOutDate && checkOut > b.CheckInDate);

            return !hasOverlap;
        }

        public async Task<List<Booking>> GetBookingsByUserIdAsync(string userId)
        {
            return await context.Bookings
                .Include(b => b.Camp)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookedOn)
                .ToListAsync();
        }

        public async Task<(int total, int active, int cancelled, double avgRating)> GetStatsAsync()
        {
            var total = await context.Bookings.CountAsync();
            var active = await context.Bookings.CountAsync(b => b.Status == BookingStatus.Confirmed);
            var cancelled = await context.Bookings.CountAsync(b => b.Status == BookingStatus.Cancelled);
            var avgRating = await context.Ratings.AnyAsync()
                ? await context.Ratings.AverageAsync(r => (double)r.Stars)
                : 0;
            return (total, active, cancelled, Math.Round(avgRating, 1));
        }
    }
}

