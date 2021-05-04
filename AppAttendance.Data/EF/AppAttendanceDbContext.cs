using AppAttendance.Data.Configuarations;
using AppAttendance.Data.Entities;
using AppAttendance.Data.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;

namespace AppAttendance.Data.EF
{
    public class AppAttendanceDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppAttendanceDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //configure using Fluent API

            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new DetailHAConfiguaration());
            modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryAttendanceConfiguration());
            modelBuilder.ApplyConfiguration(new RegisterCourseConfiguaration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguaration());
            modelBuilder.ApplyConfiguration(new StudentConfiguaration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguaration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());


            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            // Data seeding


            modelBuilder.Seed();

            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<Class> Classes { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<DetailHA> DetailHAs { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<HistoryAttendance> HistoryAttendances { get; set; }
        public DbSet<RegisterCourse> RegisterCourses { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
