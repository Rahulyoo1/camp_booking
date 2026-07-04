using CampBooking.Domain.DTOs.RatingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Business.Interfaces
{
    public interface IRatingService
    {
        Task<RatingResponseDto> AddOrUpdateRatingAsync(string userId, CreateRatingDto dto);
    }
}
