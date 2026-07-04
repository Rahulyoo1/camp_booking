using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Domain.Entities
{
    public class Camp
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int Capacity { get; set; }

        public decimal WeekdayRate { get; set; }

        public decimal WeekendRate { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}

