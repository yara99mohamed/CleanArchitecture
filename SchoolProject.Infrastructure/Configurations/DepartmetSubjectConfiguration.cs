using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class DepartmetSubjectConfiguration : IEntityTypeConfiguration<DepartmetSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmetSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.DID });

            builder.HasOne(ds => ds.Department)
                      .WithMany(d => d.DepartmentSubjects)
                      .HasForeignKey(ds => ds.DID)
                      .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ds => ds.Subject)
                    .WithMany(s => s.DepartmentSubjects)
                    .HasForeignKey(ds => ds.SubID)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
