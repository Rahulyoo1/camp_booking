using AutoMapper;
using CampBooking.Business.Interfaces;
using CampBooking.Data.Repositories.Interfaces;
using CampBooking.Domain.DTOs.BookingDto;
using CampBooking.Domain.Entities;
using CampBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampBooking.Domain.DTOs.BookingDto;
namespace CampBooking.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly ICampRepository campRepository;
        private readonly IMapper mapper;

        public BookingService(IBookingRepository bookingRepository, ICampRepository campRepository, IMapper mapper)
        {
            this.bookingRepository = bookingRepository;
            this.campRepository = campRepository;
            this.mapper = mapper;
        }

        public async Task<BookingResponseDto> CreateBookingAsync( string userId,CreateBookingDto dto)
        {
            //Validate dates
            if(dto.CheckInDate >= dto.CheckOutDate)
            {
                throw new ArgumentException("Check-in date must be before check-out date.");                
            }

            //Check if camp exists
            var camp = await campRepository.GetByIdAsync(dto.CampId);
            if(camp == null)
            {
                throw new ArgumentException("Camp does not exist.");

            }

            //check avaialability
            var isAvailable = await bookingRepository.IsCampAvailableAsync(dto.CampId, dto.CheckInDate, dto.CheckOutDate);
            if (!isAvailable)
            {
                throw new ArgumentException("Camp is not available for the selected dates.");
            }

            //calculate total amount (dynamic pricing based on dates)
            decimal totalAmount = 0;
            
            for (var date = dto.CheckInDate; date < dto.CheckOutDate; date = date.AddDays(1))
            {
            if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    totalAmount+= camp.WeekendRate;

                }
                else
                {
                    totalAmount += camp.WeekdayRate;

                }
            }

            //generate booking refernce
            var bookingReference = Guid.NewGuid().ToString("N").Substring(0,8).ToUpper();

            //create booking entity

            var booking = mapper.Map<Booking>(dto);
            booking.BookingReference = bookingReference;
            booking.TotalAmount = totalAmount;
            booking.Status = BookingStatus.Confirmed;
            booking.UserId = userId;

            //save booking
            await bookingRepository.AddAsync(booking);
            await bookingRepository.SaveChangesAsync();

            //return response
            return new BookingResponseDto
            {
                BookingReference = booking.BookingReference,

                CampName = camp.Name,

                CheckInDate = booking.CheckInDate,

                CheckOutDate = booking.CheckOutDate,

                TotalAmount = booking.TotalAmount,

                Status = booking.Status.ToString()
            };
        }



        public async Task<BookingDetailsDto?> GetBookingByReferenceAsync(string bookingReference)
        {
            var booking = await bookingRepository.GetByReferenceAsync(bookingReference);
                if(booking == null)
            {
                return null;
            }
            return new BookingDetailsDto
            {

                BookingId = booking.Id,
                BookingReference = booking.BookingReference,
                CampName = booking.Camp.Name,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalAmount = booking.TotalAmount,
                BillingAddress = booking.BillingAddress,
                PhoneNumber = booking.PhoneNumber,
                Status = booking.Status.ToString(),
                UserId = booking.UserId
            };

        }

        public async Task CancelBookingAsync(string bookingReference)
        {
            var booking = await bookingRepository.GetByReferenceAsync(bookingReference);
            if(booking == null)
            {
                throw new Exception("Booking not found");

            }

            if(booking.Status== BookingStatus.Cancelled)
            {
                throw new Exception("Booking already cancelled");
            }
            //cannot cancel past or ongoing booking
            if(booking.CheckInDate <= DateOnly.FromDateTime(DateTime.Today))
            {
                throw new Exception("Only future bookings can be cancelled");

            }

            booking.Status= BookingStatus.Cancelled;
            await bookingRepository.UpdateAsync(booking);
            await bookingRepository.SaveChangesAsync();
        }

        public async Task<List<MyBookingDto>> GetMyBookingsAsync(string userId)
        {
            var bookings = await bookingRepository.GetBookingsByUserIdAsync(userId);

            return bookings.Select(b => new MyBookingDto
            {
                BookingId = b.Id,
                BookingReference = b.BookingReference,
                CampName = b.Camp.Name,
                ImageUrl = b.Camp.ImageUrl,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                TotalAmount = b.TotalAmount,
                Status = b.Status.ToString()
            }).ToList();
        }

        public async Task<(int total, int active, int cancelled, double avgRating)> GetBookingStatsAsync()
        {
            return await bookingRepository.GetStatsAsync();
        }
    }
}
