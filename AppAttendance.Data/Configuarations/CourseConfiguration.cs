using AppAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAttendance.Data.Configuarations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.HasKey(x => x.Id_Course);
            builder.Property(x => x.Id_Course).UseIdentityColumn();
            builder.HasOne(x => x.Subject).WithMany(x => x.Courses).HasForeignKey(x => x.Id_Subject);
            builder.HasOne(x => x.Teacher).WithMany(x => x.Courses).HasForeignKey(x => x.Id_Teacher);

        }
    }
}
