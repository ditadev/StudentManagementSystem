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

    public DbSet<Model.Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var student = modelBuilder.Entity<Model.Student>();
        var department = modelBuilder.Entity<Department>();
        var courses = modelBuilder.Entity<Course>();
        var user = modelBuilder.Entity<User>();

        user.HasKey(u => u.AdmissionNumber);
        courses.HasKey(c => c.CourseCode);
        student.HasKey(s => s.AdmissionNumber);
        department.HasKey(d => d.DepartmentId);
        courses.Ignore(x => x.AdmissionNumber);
        user.Ignore(x => x.Password);
        student.Property(s => s.Firstname).IsRequired();
        student.Property(s => s.Lastname).IsRequired();
        student.Property(s => s.AdmissionNumber).IsRequired();
        student.Property(s => s.DepartmentId).IsRequired();
        department.HasMany(d => d.Students)
            .WithOne(s => s.Department)
            .HasForeignKey("DepartmentId").IsRequired();
        department.HasMany(d => d.Courses)
            .WithMany(c => c.Departments);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=StudentManagementSystem;UserId=postgres;");
    }
}