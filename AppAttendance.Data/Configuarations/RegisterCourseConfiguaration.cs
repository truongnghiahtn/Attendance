using AppAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Configuarations
{
    public class RegisterCourseConfiguaration : IEntityTypeConfiguration<RegisterCourse>
    {
        public void Configure(EntityTypeBuilder<RegisterCourse> builder)
        {
            builder.ToTable("RegisterCourse");
            builder.HasKey(x => x.Id_RegisterCourse);
            builder.Property(x => x.Id_RegisterCourse).UseIdentityColumn();
            builder.HasOne(x => x.Course).WithMany(x => x.RegisterCourses).HasForeignKey(x => x.Id_Course);
            builder.HasOne(x => x.Student).WithMany(x => x.RegisterCourses).HasForeignKey(x => x.Id_Student);
        }
    }
}
