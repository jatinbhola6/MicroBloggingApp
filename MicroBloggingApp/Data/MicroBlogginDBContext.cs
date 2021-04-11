using MicroBloggingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroBloggingApp.VIewModels;

namespace MicroBloggingApp.Data
{
    public class MicroBlogginDBContext : DbContext
    {
        public DbSet<BlogModel> Blogs
        {
            get;
            set;
        }

        public DbSet<UserModel> Users
        {
            get;
            set;
        }

        public MicroBlogginDBContext(DbContextOptions<MicroBlogginDBContext> options) : base(options)
        {
        }

        public DbSet<MicroBloggingApp.VIewModels.LoginModel> LoginModel { get; set; }

        public DbSet<MicroBloggingApp.VIewModels.RegistrationModel> RegistrationModel { get; set; }
    }
}
