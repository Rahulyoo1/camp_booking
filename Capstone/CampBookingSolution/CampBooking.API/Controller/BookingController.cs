using CampBooking.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CampBooking.Domain.DTOs.BookingDto;
namespace CampBooking.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var bookingResponse = await bookingService.CreateBookingAsync(userId, dto);
            return Ok(bookingResponse);
        }

        [HttpGet("{bookingReference}")]
        public async Task<IActionResult> GetBooking(string bookingReference)
        {
            var booking = await bookingService.GetBookingByReferenceAsync(bookingReference);
            if(booking == null)
            {
                return NotFound("Booking not found");
            }

            if (!User.IsInRole("Admin"))
            {
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (booking.UserId != loggedInUserId)
                {
                    return Forbid();
                }
            }

            return Ok(booking);
        }

        [HttpPut("Cancel/{bookingReference}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CancelBooking (string bookingReference)
        {
            await bookingService.CancelBookingAsync(bookingReference);
            return Ok(new { Message = "Booking cancelled successfully" });
        }

        [HttpGet("MyBookings")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var bookings = await bookingService.GetMyBookingsAsync(userId);

            return Ok(bookings);
        }

    }
}
