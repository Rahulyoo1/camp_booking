using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Domain.DTOs.RatingDto
{
    public class CreateRatingDto
    {
        public int BookingId { get; set; }

        public int Stars { get; set; }

        public string? Review { get; set; }
    }
}

