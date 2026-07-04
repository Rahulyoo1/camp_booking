namespace CampBooking.Domain.Entities;

public class Rating
{
    public int Id { get; set; }

    public int Stars { get; set; }

    public string? Review { get; set; }

    public DateTime RatedOn { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public int BookingId { get; set; }

    public int CampId { get; set; }

    public string UserId { get; set; } = string.Empty;

    // Navigation Properties
    public Booking Booking { get; set; } = null!;

    public Camp Camp { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;
}