using AppAttendance.Application.AttendanceStudent;
using AppAttendance.Application.Common;
using AppAttendance.Application.Notifications;
using AppAttendance.Application.Schedules;
using AppAttendance.Application.Students;
using AppAttendance.Application.System.Classes;
using AppAttendance.Application.System.Courses;
using AppAttendance.Application.System.Roles;
using AppAttendance.Application.System.Subjects;
using AppAttendance.Application.System.Users;
using AppAttendance.Application.Teachers;
using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()));
            services.AddDbContext<AppAttendanceDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AppAttendanceDb")));


            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppAttendanceDbContext>()
                .AddDefaultTokenProviders();

            //Service
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<ICoursesService, CourseService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IAttendanceStudentService, AttendanceStudentService>();
            services.AddTransient<INotificationService, NotificationService>();
            //

            services.AddControllersWithViews();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
