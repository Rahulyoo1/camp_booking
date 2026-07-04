namespace CampBooking.Domain.DTOs.Admin
{
    public class AdminStatsDto
    {
        public int TotalCamps { get; set; }
        public int TotalBookings { get; set; }
        public int ActiveBookings { get; set; }
        public int CancelledBookings { get; set; }
        public double AverageRating { get; set; }
        public int TotalUsers { get; set; }
    }
}
