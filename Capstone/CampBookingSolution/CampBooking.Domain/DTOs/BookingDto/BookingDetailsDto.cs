using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Domain.DTOs.BookingDto
{
    public class BookingDetailsDto
    {
        public int BookingId { get; set; }
        public string BookingReference { get; set; } = string.Empty;

        public string CampName { get; set; } = string.Empty;

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string BillingAddress { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
    }
}
