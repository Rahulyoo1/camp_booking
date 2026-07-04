using CampBooking.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampBooking.Business.Interfaces;
using CampBooking.Domain.Entities;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
namespace CampBooking.Business.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public JwtService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }
        public async Task<string> GenerateToken(ApplicationUser user)           // Here you would typically generate the JWT token using the claims and configuration settings.

        {   // Get user roles
            var roles = await userManager.GetRolesAsync(user);


            var claims = new List<Claim>                          
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add roles to claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));   //This converts your secret key into a format used to sign the token.

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  //This specifies the algorithm used to sign the token.

            var token = new JwtSecurityToken(             // Token creation
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials

                );

            return new JwtSecurityTokenHandler().WriteToken(token);  // This generates the final token string that can be returned to the client.
        }

    }
    }

