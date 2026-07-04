using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CampBooking.Domain.Entities;
namespace CampBooking.Data.Context
{
    public class CampBookingDb : IdentityDbContext<ApplicationUser>
    {
        public CampBookingDb(DbContextOptions<CampBookingDb> options)
            : base(options)
        {
            
        }
        public DbSet<Camp> Camps { get; set; } 

        public DbSet<Booking> Bookings { get; set; }
        
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // fluent api configurations for Camp entity

            builder.ApplyConfigurationsFromAssembly(typeof(CampBookingDb).Assembly);
        }
    }

    }

