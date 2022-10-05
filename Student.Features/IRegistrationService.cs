using Student.Model;

namespace Student.Services;

public interface IRegistrationService
{
    public Task<string?> CreatPasswordHash(string password);
    public bool VerifyPasswordHash(string password, string? passwordHash);
    public Task<string?> CreateStudentToken(User user);
    public Task<string?> CreateAdminToken(Admin admin);
}