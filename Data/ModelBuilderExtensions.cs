using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>().HasData( 
                new IdentityUser
                {
                    Id="1",
                     Email="Admin@gmail.com",
                     NormalizedEmail= "Admin@gmail.com",
                     UserName ="Admin",
                      PasswordHash="Admin@123"
                });
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id="1",
                    Name = "Admin"
                });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1"
                });

        }
    }
}
