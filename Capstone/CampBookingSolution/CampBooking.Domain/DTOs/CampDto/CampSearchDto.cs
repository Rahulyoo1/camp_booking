using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Domain.DTOs.CampDto
{
    public class CampSearchDto
    {
        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public int? Capacity { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
