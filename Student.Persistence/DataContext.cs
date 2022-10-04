using Microsoft.EntityFrameworkCore;
using Student.Model;

namespace Student.Persistence;

public class DataContext : DbContext
{
    public DbSet<Model.Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Course> Courses { get; set; }

    public DataContext()
    {
        
    }
    
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Student;UserId=postgres;");
    }
}