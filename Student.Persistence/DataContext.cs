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
    public DbSet<Model.Student> Students { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var student = modelBuilder.Entity<Model.Student>();
        var department = modelBuilder.Entity<Department>();
        var courses = modelBuilder.Entity<Course>();
        var user = modelBuilder.Entity<User>();
        var administrator = modelBuilder.Entity<Administrator>();
        var admin = modelBuilder.Entity<Admin>();

        user.HasKey(u => u.AdmissionNumber);
        admin.HasKey(a => a.AdminId);
        courses.HasKey(c => c.CourseCode);
        student.HasKey(s => s.AdmissionNumber);
        department.HasKey(d => d.DepartmentId);
        administrator.HasKey(a => a.AdminId);

        courses.Ignore(x => x.AdmissionNumber);
        user.Ignore(x => x.Password);
        admin.Ignore(x => x.Password);
        student.Property(s => s.Firstname).IsRequired();
        student.Property(s => s.Lastname).IsRequired();
        student.Property(s => s.AdmissionNumber).IsRequired();
        student.Property(s => s.DepartmentId).IsRequired();
        administrator.Property(s => s.Firstname).IsRequired();
        administrator.Property(s => s.Lastname).IsRequired();
        administrator.Property(s => s.AdminId).IsRequired();
        administrator.Property(s => s.DepartmentId).IsRequired();
        administrator.Property(s => s.Position).IsRequired();
        department.HasMany(d => d.Admins)
            .WithOne(a => a.Department)
            .HasForeignKey("DepartmentId").IsRequired();
        department.HasMany(d => d.Students)
            .WithOne(s => s.Department)
            .HasForeignKey("DepartmentId").IsRequired();
        department.HasMany(d => d.Courses)
            .WithMany(c => c.Departments);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=StudentManagementSystemV2;UserId=postgres;");
    }
}