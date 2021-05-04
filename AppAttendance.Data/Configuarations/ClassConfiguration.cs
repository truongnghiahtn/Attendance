using AppAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAttendance.Data.Configuarations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Class");
            builder.HasKey(x => x.Id_Class);
            builder.Property(x => x.Id_Class).UseIdentityColumn();

        }
    }
}
