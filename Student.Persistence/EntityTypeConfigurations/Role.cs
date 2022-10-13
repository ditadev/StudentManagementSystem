using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Student.Persistence.EntityTypeConfigurations;

public class Role : IEntityTypeConfiguration<Model.Role>
{
    public void Configure(EntityTypeBuilder<Model.Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Users)
            .WithMany(y => y.Roles);
        builder.HasData(new object[]
        {
            new Model.Role { Id = Model.Enums.Role.HeadOfDepartment },
            new Model.Role { Id = Model.Enums.Role.Lecturer },
            new Model.Role { Id = Model.Enums.Role.Student }
        });
    }
}