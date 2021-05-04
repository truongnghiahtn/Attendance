using AppAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Configuarations
{
    public class HistoryAttendanceConfiguration : IEntityTypeConfiguration<HistoryAttendance>
    {
        public void Configure(EntityTypeBuilder<HistoryAttendance> builder)
        {
            builder.ToTable("HistoryAttendance");
            builder.HasKey(x => x.Id_HistoryAttendace);
            builder.HasOne(x => x.Course).WithMany(x => x.HistoryAttendances).HasForeignKey(x => x.Id_Course);
        }
    }
}
