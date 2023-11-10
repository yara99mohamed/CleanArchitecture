using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.DID);
            builder.Property(x => x.DNameAr).HasMaxLength(200);
            builder.Property(x => x.DNameEn).HasMaxLength(200);

            // Relation one to many
            builder.HasMany(x => x.Students)
                   .WithOne(x => x.Department)
                   .HasForeignKey(d => d.StudID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relation one to one 
            builder.HasOne(x => x.InstructorManager)
                   .WithOne(x => x.DepartmentManager)
                   .HasForeignKey<Department>(x => x.InstructorManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
