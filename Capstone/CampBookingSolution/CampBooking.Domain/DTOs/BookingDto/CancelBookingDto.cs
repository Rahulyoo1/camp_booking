using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Domain.DTOs.BookingDto
{
    public class CancelBookingDto
    {
        public string BookingReference { get; set; } = string.Empty;
    }
}
