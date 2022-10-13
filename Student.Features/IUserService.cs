using Student.Model;

namespace Student.Services;

public interface IUserService
{
    public Task<User?> GetStudentByAdmissionNumber(string identificationNumber);
    public Task<User?> GetUserByEmail(string emailAddress);
    public Task AddUser(User user);
    public Task DeleteUser(string admissionNumber);
    public Task<Department?> GetDepartmentById(string departmentId);
    public Task<List<Course>> GetCoursesByDepartment(string departmentId);
    public Task<string> CreatPasswordHash(string password);
    public bool VerifyPasswordHash(string password, string? passwordHash);
    public Task<string> CreateJwt(User user);
}