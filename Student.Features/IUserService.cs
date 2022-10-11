using Student.Model;

namespace Student.Services;

public interface IUserService
{
    public Task<Model.User> GetStudentByAdmissionNumber(string admissionNumber);
    public Task<string?> CreatPasswordHash(string password);
    public bool VerifyPasswordHash(string password, string? passwordHash);
    public Task<string?> CreateJwt(User user);
    
}