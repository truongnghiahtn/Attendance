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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace AppAttendance.BE
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger AppAttendance", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });

            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
     .AddJwtBearer(options =>
     {
         options.RequireHttpsMetadata = false;
         options.SaveToken = true;
         options.TokenValidationParameters = new TokenValidationParameters()
         {
             ValidateIssuer = true,
             ValidIssuer = issuer,
             ValidateAudience = true,
             ValidAudience = issuer,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ClockSkew = System.TimeSpan.Zero,
             IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
         };
     });
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
            app.UseAuthentication();

            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
