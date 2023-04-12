using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OCOP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OCOP.Data.Seeding
{
    public static class ModelBuilderSeeding
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
                               
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "hpnama18099@cusc.ctu.edu.vn",
                NormalizedEmail = "hpnama18099@cusc.ctu.edu.vn",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty,               
                ImagePath = "avatar.jpg",               
            });

            modelBuilder.Entity<ThanhVien>().HasData(new ThanhVien
            {               
                AppUserId=adminId,
                DonViCongTac="OCOP Administrator co.ltd",
                MaNhanVien="ABCD1234",
                Ten="Nguyen Van Admin"
                });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
