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
    public class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)  // we only configure property we added , rest is configured by Identity framework.
        {
            builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);
        }
    }
}
