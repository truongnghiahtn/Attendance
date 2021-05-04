using AppAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Configuarations
{
    public class ScheduleConfiguaration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedule");
            builder.HasKey(x => x.Id_Schedule);
            builder.Property(x => x.Id_Schedule).UseIdentityColumn();
            builder.HasOne(x => x.Class).WithMany(x => x.Schedules).HasForeignKey(x => x.Id_Class);
            builder.HasOne(x => x.Course).WithMany(x => x.Schedules).HasForeignKey(x => x.Id_Course);
        }
    }
}
