using AppAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Configuarations 
{
    public class DetailHAConfiguaration : IEntityTypeConfiguration<DetailHA>
    {
        public void Configure(EntityTypeBuilder<DetailHA> builder)
        {
            builder.ToTable("DetailHA");
            builder.HasKey(x => x.Id_DetailHA);
            builder.Property(x => x.Id_DetailHA).UseIdentityColumn();
            builder.HasOne(x => x.HistoryAttendance).WithMany(x => x.DetailHAs).HasForeignKey(x => x.Id_HistoryAttendance);
            builder.HasOne(x => x.Student).WithMany(x => x.DetailHAs).HasForeignKey(x => x.Id_Student);
        }
    }
}
