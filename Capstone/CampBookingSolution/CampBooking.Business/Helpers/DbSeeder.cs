using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
namespace CampBooking.Business.Helpers
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            string[] roles= { "Admin", "User" };

            foreach (var role in roles)
            { 
            if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        
            String adminEmail = "admin@campbooking.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {                                   
                admin = new ApplicationUser         //creating Admin User
                {
                    FullName = "System Administration",
                    UserName = adminEmail,
                    Email = adminEmail,
                    PhoneNumber = "9999999999"

                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }


        }
    }
}

