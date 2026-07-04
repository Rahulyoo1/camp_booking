using CampBooking.Business.Interfaces;
using CampBooking.Domain.DTOs.RatingDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CampBooking.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateRating(CreateRatingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await ratingService.AddOrUpdateRatingAsync(userId, dto);

            return Ok(result);
        }
    }
}