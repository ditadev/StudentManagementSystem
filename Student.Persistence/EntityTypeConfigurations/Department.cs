using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Student.Persistence.EntityTypeConfigurations;

public class Department : IEntityTypeConfiguration<Model.Department>
{
    public void Configure(EntityTypeBuilder<Model.Department> builder)
    {
        builder.HasKey(d => d.DepartmentId);
        builder.Property(d => d.DepartmentName).IsRequired();
        builder.HasIndex(d => d.DepartmentName).IsUnique();
        builder.HasMany(d => d.Users)
            .WithOne(s => s.Department)
            .HasForeignKey("DepartmentId").IsRequired();
        builder.HasMany(d => d.Courses)
            .WithMany(c => c.Departments);
    }
}