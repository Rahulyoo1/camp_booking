using CampBooking.Domain.Common;
using CampBooking.Domain.DTOs.CampDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Business.Interfaces
{
    public interface ICampService
    {
        Task<IEnumerable<CampResponseDto>> GetAllAsync();

        Task<CampResponseDto?> GetByIdAsync(int id);

        Task CreateAsync(CreateCampDto dto);

        Task UpdateAsync(UpdateCampDto dto);

        Task DeleteAsync(int id);
        Task<PagedResponse<CampResponseDto>> SearchAvailableCampsAsync(CampSearchDto dto);
    }
}
