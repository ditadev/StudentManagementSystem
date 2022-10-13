using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Student.Model;
using Student.Persistence;
using Role = Student.Model.Enums.Role;

namespace Student.Services;

public class UserService : IUserService
{
    private readonly AppSettings _appSettings;
    private readonly DataContext _dataContext;

    public UserService(DataContext dataContext, AppSettings appSettings)
    {
        _dataContext = dataContext;
        _appSettings = appSettings;
    }

    public async Task<User?> GetUserByEmail(string emailAddress)
    {
        return await _dataContext.Users.Where(u => u.EmailAddress == emailAddress)
            .Include(u => u.Department)
            .Include(u => u.Courses)
            .Include(u => u.Roles)
            .SingleOrDefaultAsync();
    }

    public async Task AddUser(User user)
    {
        user.Roles = new List<Model.Role?> { await _dataContext.Roles.SingleOrDefaultAsync(x => x.Id == Role.Student) };
        _dataContext.Add(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteUser(string admissionNumber)
    {
        var user = await GetStudentByAdmissionNumber(admissionNumber);
        if (user != null) _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Department?> GetDepartmentById(string departmentId)
    {
        return await _dataContext.Departments.Where(d => d.DepartmentId == departmentId)
            .Include(x => x.Courses)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Course>> GetCoursesByDepartment(string departmentId)
    {
        return await _dataContext.Courses.Where(c => c.DepartmentId == departmentId)
            .ToListAsync();
    }

    public async Task<string> CreatPasswordHash(string password)
    {
        return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
    }

    public bool VerifyPasswordHash(string password, string? passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public Task<string> CreateJwt(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var claims = new List<Claim>
        {
            new("userAdmissionNumber", user.IdentificationNumber),
            new("sub", user.UserId.ToString()),
            new("role", Role.Default.ToString())
        };

        claims.AddRange(user.Roles.Select(role => new Claim("role", role.Id.ToString())));

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            )));
    }

    public async Task<User?> GetStudentByAdmissionNumber(string admissionNumber)
    {
        return await _dataContext.Users
            .Where(x => x.IdentificationNumber == admissionNumber)
            .Include(x => x.Courses)
            .Include(x => x.Department)
            .Include(x => x.Roles)
            .SingleOrDefaultAsync();
        ;
    }
}