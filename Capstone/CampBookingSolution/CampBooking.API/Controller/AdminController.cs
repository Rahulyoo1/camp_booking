using CampBooking.Business.Interfaces;
using CampBooking.Domain.DTOs.Admin;
using CampBooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CampBooking.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IBookingService bookingService;
        private readonly ICampService campService;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IBookingService bookingService, ICampService campService, UserManager<ApplicationUser> userManager)
        {
            this.bookingService = bookingService;
            this.campService = campService;
            this.userManager = userManager;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var (total, active, cancelled, avgRating) = await bookingService.GetBookingStatsAsync();
            var camps = await campService.GetAllAsync();
            var totalUsers = userManager.Users.Count();

            return Ok(new AdminStatsDto
            {
                TotalCamps = camps.Count(),
                TotalBookings = total,
                ActiveBookings = active,
                CancelledBookings = cancelled,
                AverageRating = avgRating,
                TotalUsers = totalUsers
            });
        }
    }
}
