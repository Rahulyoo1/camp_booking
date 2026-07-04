using CampBooking.Data.Context;
using CampBooking.Data.Repositories.Interfaces;
using CampBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampBooking.Data.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly CampBookingDb context;

    public RatingRepository(CampBookingDb context)
    {
        this.context = context;
    }

    public async Task<Rating?> GetByBookingIdAsync(int bookingId)
    {
        return await context.Ratings
            .Include(r => r.Camp)
            .FirstOrDefaultAsync(r => r.BookingId == bookingId);
    }

    public async Task AddAsync(Rating rating)
    {
        await context.Ratings.AddAsync(rating);
    }

    public async Task UpdateAsync(Rating rating)
    {
        context.Ratings.Update(rating);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}