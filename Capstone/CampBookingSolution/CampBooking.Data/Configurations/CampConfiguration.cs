using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CampBooking.Domain.Entities;

namespace CampBooking.Data.Configurations
{
    public class CampConfiguration : IEntityTypeConfiguration<Camp>  //EF Core automatically finds every class implementing , No need to register them one by one.
    {
        public void Configure(EntityTypeBuilder<Camp> builder)
        {
            builder.HasKey(c => c.Id);
         
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        
            builder.Property(c=>c.Description).IsRequired().HasMaxLength(500);
            
            builder.Property(c => c.Location).IsRequired().HasMaxLength(200);

            builder.Property(c => c.ImageUrl).IsRequired();


            builder.Property(c => c.WeekdayRate).HasPrecision(10, 2);


            builder.Property(c => c.WeekendRate).HasPrecision(10, 2);


            builder.Property(c => c.Capacity).IsRequired();

        }
    }
}
