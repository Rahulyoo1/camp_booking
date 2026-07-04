using CampBooking.Business.Interfaces;
using CampBooking.Domain.DTOs.AuthDto;
using CampBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Business.Services
{
    public class AuthService: IAuthService
    {
        private readonly IJwtService jwtService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(IJwtService jwtService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("User already exists with this email.");
            }

            var user = new ApplicationUser      //create new application user
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, request.Password);   //save user

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            }
                //Assign user role
                await userManager.AddToRoleAsync(user, "User");

            return result;

        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if(user == null)
            {
                throw new Exception("User not found.");
            }

            //verify password
            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid password.");
            }

            //Generate JWT token
            var token = await jwtService.GenerateToken(user);

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = role
            };
        }
    }
}
