using CampBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CampBooking.Data.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Stars)
               .IsRequired();

        builder.Property(r => r.Review)
               .HasMaxLength(1000);

        // Booking → Rating (One-to-One)
        builder.HasOne(r => r.Booking)
               .WithOne(b => b.Rating)
               .HasForeignKey<Rating>(r => r.BookingId)
               .OnDelete(DeleteBehavior.Cascade);

        // Camp → Ratings (One-to-Many)
        builder.HasOne(r => r.Camp)
               .WithMany(c => c.Ratings)
               .HasForeignKey(r => r.CampId)
               .OnDelete(DeleteBehavior.Restrict);

        // User → Ratings (One-to-Many)
        builder.HasOne(r => r.User)
               .WithMany(u => u.Ratings)
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}