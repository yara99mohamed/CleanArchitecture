using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class InstructorSubjectConfiguration : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(x => new { x.InstructorId, x.SubID });

            builder.HasOne(ins => ins.Instructor)
                      .WithMany(s => s.InstructorSubjects)
                      .HasForeignKey(ins => ins.InstructorId);

            builder.HasOne(ins => ins.Subject)
                    .WithMany(s => s.InstructorSubjects)
                    .HasForeignKey(ins => ins.SubID);
        }
    }
}
