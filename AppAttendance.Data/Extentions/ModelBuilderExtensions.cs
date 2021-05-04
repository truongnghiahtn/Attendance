using AppAttendance.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Class>().HasData(
                new Class { Id_Class = 1, Name = "A101", Description = "Phòng tòa nhà A", DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) },
                new Class { Id_Class = 2, Name = "A102", Description = "Phòng tòa nhà A", DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) },
                new Class { Id_Class = 3, Name = "B101", Description = "Phòng tòa nhà B", DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) },
                new Class { Id_Class = 4, Name = "B102", Description = "Phòng tòa nhà B", DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) },
                new Class { Id_Class = 5, Name = "C101", Description = "Phòng tòa nhà C", DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) },
                new Class { Id_Class = 6, Name = "C101", Description = "Phòng tòa nhà C", DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) }
                );

            // Subject 
            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id_Subject = 1, Name = "IT004",Description="Cơ sở dữ liệu",NumberOfCredits=3,Lesson=12, DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) },
                new Subject { Id_Subject = 2, Name = "IE303", Description = "Công nghệ Java", NumberOfCredits = 3, Lesson = 12, DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) },
                new Subject { Id_Subject = 3, Name = "IT001", Description = "Nhập môn lập trình", NumberOfCredits = 3, Lesson = 12, DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) },
                new Subject { Id_Subject = 4, Name = "MA003", Description = "Đại số tuyến tính", NumberOfCredits = 3, Lesson = 12, DateCreate = DateTime.UtcNow.AddHours(7), DateUpdate = DateTime.UtcNow.AddHours(7) }
                );

            //
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(

            new AppRole{Id = roleId,Name = "admin",NormalizedName = "admin",Description = "Administrator role"},
            new AppRole { Id = new Guid("7bbc55c5-6ea6-43e1-7581-08d8ed00ec81"), Name = "student", NormalizedName = "student", Description = "Student Role" },
            new AppRole { Id = new Guid("8125c97d-6048-4d57-7582-08d8ed00ec81"), Name = "teacher", NormalizedName = "teacher", Description = "Teacher Role" }
            );

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "admin123"),
                SecurityStamp = string.Empty,
                FullName= "Administrator",
                Type ="admin"

            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
