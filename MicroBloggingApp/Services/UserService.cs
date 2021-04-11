using MicroBloggingApp.Data;
using MicroBloggingApp.Models;
using MicroBloggingApp.VIewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MicroBloggingApp.Services
{
    public class UserService
    {
        private MicroBlogginDBContext _context;

        private IConfiguration _configuration;

        public UserService(MicroBlogginDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserModel> AddUser(RegistrationModel model)
        {
            UserModel userModel = new UserModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password
            };
            await _context.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public ClaimsPrincipal GetClaims(UserModel user)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[] { new Claim("Email", user.Email), new Claim("FirstName", user.FirstName), new Claim("LastName", user.LastName) }, "User Identity");
            return new ClaimsPrincipal(new[] { claimsIdentity });
        }

        public string GetEmail(ClaimsPrincipal claims)
        {
            return claims.Claims.FirstOrDefault(c => c.Type == "Email").Value;
        }

        public async Task<UserModel> GetUser(LoginModel model)
        {
            UserModel user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.email);
            return user;
        }
    }
}
