using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Domain.DTOs.CampDto
{
    public class CreateCampDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public decimal WeekdayRate { get; set; }

        public decimal WeekendRate { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}
