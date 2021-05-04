using AppAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Configuarations
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.ToTable("Equipment");
            builder.HasKey(x => x.Id_BLE);
            builder.HasOne(x => x.Student).WithMany(x => x.Equipment).HasForeignKey(x => x.Id_Student);

        }
    }
}

