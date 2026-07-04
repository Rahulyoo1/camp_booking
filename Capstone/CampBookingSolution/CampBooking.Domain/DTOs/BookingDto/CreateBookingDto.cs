using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Domain.DTOs.BookingDto
{
    public class CreateBookingDto
    {
        public int CampId { get; set; }

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public string BillingAddress { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
