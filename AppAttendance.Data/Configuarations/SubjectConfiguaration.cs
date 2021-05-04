using AppAttendance.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAttendance.Data.Configuarations
{
    public class SubjectConfiguaration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject");
            builder.HasKey(x => x.Id_Subject);
            builder.Property(x => x.Id_Subject).UseIdentityColumn();
        }
    }
}
