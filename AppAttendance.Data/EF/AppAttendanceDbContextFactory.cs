using AppAttendance.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NvPShop.Data.EF
{
    public class AppAttendanceDbContextFactory : IDesignTimeDbContextFactory<AppAttendanceDbContext>
    {
        public AppAttendanceDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("AppAttendanceDb");

            var optionsBuilder = new DbContextOptionsBuilder<AppAttendanceDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppAttendanceDbContext(optionsBuilder.Options);
        }
    }
}
