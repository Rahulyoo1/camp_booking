namespace CampBooking.Domain.DTOs.BookingDto;

public class MyBookingDto
{
    public int BookingId { get; set; }

    public string BookingReference { get; set; } = string.Empty;

    public string CampName { get; set; } = string.Empty;

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
}