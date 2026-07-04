using CampBooking.Domain.Common;
using CampBooking.Domain.DTOs.CampDto;
using CampBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Data.Repositories.Interfaces
{
    public interface ICampRepository
    {
        Task<IEnumerable<Camp>> GetAllAsync();
        Task<Camp?> GetByIdAsync(int id);

        Task AddAsync(Camp camp);
        Task UpdateAsync(Camp camp);
            
        Task DeleteAsync(Camp camp);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
        Task<PagedResponse<Camp>> SearchAvailableCampsAsync(CampSearchDto dto);
    }
}
