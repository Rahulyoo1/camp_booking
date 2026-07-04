using CampBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Data.Configurations
{
    public class BookingConfiguration :IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
    {
            //property configurations
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BookingReference).IsRequired().HasMaxLength(8);

            builder.HasIndex(b => b.BookingReference).IsUnique();

            builder.Property(b => b.TotalAmount).HasPrecision(10, 2);

            builder.Property(b => b.BillingAddress).IsRequired().HasMaxLength(200);

            builder.Property(b => b.PhoneNumber).IsRequired().HasMaxLength(15);

            // User → Bookings (One-to-Many)  : relationship between User and Booking
            builder.HasOne(b=>b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Camp → Bookings (One-to-Many)
            builder.HasOne(b => b.Camp)
                   .WithMany(c => c.Bookings)
                   .HasForeignKey(b => b.CampId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
