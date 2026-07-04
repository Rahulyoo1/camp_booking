using CampBooking.Data.Context;
using CampBooking.Data.Repositories.Interfaces;
using CampBooking.Domain.Common;
using CampBooking.Domain.DTOs.CampDto;
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
    public class CampRepository: ICampRepository
    {
        private readonly CampBookingDb context;

        public CampRepository(CampBookingDb context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Camp>> GetAllAsync()
        {
            return await context.Camps.Include(c => c.Ratings).AsNoTracking().ToListAsync();

        }

        public async Task<Camp?> GetByIdAsync(int id)
        {
            return await context.Camps
                .Include(c => c.Ratings)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Camp camp)
        {
            await context.Camps.AddAsync(camp);
        }

        public Task UpdateAsync(Camp camp)
        {
            context.Camps.Update(camp);
            return  Task.CompletedTask;
        }

        public Task DeleteAsync(Camp camp)
        {
            context.Camps.Remove(camp);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Camps.AnyAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();

        }

        public async Task<PagedResponse<Camp>> SearchAvailableCampsAsync(CampSearchDto dto)
        {
            var query = context.Camps
                .Include(c => c.Bookings)
                .Include(c => c.Ratings)
                .Where(c => c.Capacity >= dto.Capacity);

            query = query.Where(c =>
                !c.Bookings.Any(b =>
                    b.Status == BookingStatus.Confirmed &&
                    dto.CheckInDate < b.CheckOutDate &&
                    dto.CheckOutDate > b.CheckInDate));

            var totalRecords = await query.CountAsync();

            var camps = await query
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            return new PagedResponse<Camp>
            {
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / dto.PageSize),
                Data = camps
            };
        }
    }
}
