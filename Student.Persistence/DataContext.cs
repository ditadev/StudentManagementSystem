using Microsoft.EntityFrameworkCore;
using Student.Model;

namespace Student.Persistence;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }


    public DbSet<Department> Departments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<User?> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntityTypeConfigurations.User());
        modelBuilder.ApplyConfiguration(new EntityTypeConfigurations.Course());
        modelBuilder.ApplyConfiguration(new EntityTypeConfigurations.Department());
    }
}