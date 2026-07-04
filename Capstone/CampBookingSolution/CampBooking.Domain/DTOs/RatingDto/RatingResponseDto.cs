using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Domain.DTOs.RatingDto
{
    public class RatingResponseDto
    {
        public int Id { get; set; }

        public int Stars { get; set; }

        public string? Review { get; set; }

        public DateTime RatedOn { get; set; }

        public string CampName { get; set; } = string.Empty;

    }
}
