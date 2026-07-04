using CampBooking.Domain.Enums;

namespace CampBooking.Domain.Entities;

public class Booking
{
    public int Id { get; set; }

    public string BookingReference { get; set; } = string.Empty;

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string BillingAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

    public DateTime BookedOn { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public string UserId { get; set; } = string.Empty;

    public int CampId { get; set; }

    // Navigation Properties
    public ApplicationUser User { get; set; } = null!;

    public Camp Camp { get; set; } = null!;

    public Rating? Rating { get; set; }
}