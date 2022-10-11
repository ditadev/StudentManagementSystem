using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Student.Persistence.EntityTypeConfigurations;

public class Course : IEntityTypeConfiguration<Model.Course>
{
    public void Configure(EntityTypeBuilder<Model.Course> builder)
    {
        builder.HasKey(c => c.CourseCode);
        builder.Property(c => c.CourseTitle).IsRequired();
        builder.Property(c => c.CreditLoad).IsRequired();
        builder.Property(c => c.DepartmentId).IsRequired();
    }
}