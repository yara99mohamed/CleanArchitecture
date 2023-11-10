using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(x => new { x.StudID, x.SubID });

            builder.HasOne(ss => ss.Student)
                      .WithMany(st => st.studentSubjects)
                      .HasForeignKey(ins => ins.StudID);

            builder.HasOne(ss => ss.Subject)
                    .WithMany(sb => sb.StudentSubjects)
                    .HasForeignKey(ins => ins.SubID);
        }
    }
}
