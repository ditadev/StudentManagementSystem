using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Student.Persistence.EntityTypeConfigurations;

public class User : IEntityTypeConfiguration<Model.User>
{
    public void Configure(EntityTypeBuilder<Model.User> builder)
    {
        builder.HasKey(u => u.UserId);
        builder.Property(u => u.IdentificationNumber).IsRequired();
        builder.HasIndex(u => u.IdentificationNumber).IsUnique();
        builder.Property(u => u.FirstName).IsRequired();
        builder.Property(u => u.LastName).IsRequired();
        builder.Property(u => u.EmailAddress).IsRequired();
        builder.HasIndex(u => u.EmailAddress).IsUnique();
        builder.Property(u => u.Roles).IsRequired();
       
    }
}