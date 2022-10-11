using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Student.Model;
using Student.Persistence;

namespace Student.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _dataContext;

    public UserService(DataContext dataContext, IConfiguration configuration)
    {
        _dataContext = dataContext;
        _configuration = configuration;
    }

    public async Task<string?> CreatPasswordHash(string password)
    {
        return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
    }

    public bool VerifyPasswordHash(string password, string? passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public async Task<string?> CreateJwt(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.IdentificationNumber),
            new(ClaimTypes.Role, "Student")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return await Task.FromResult(jwt);
    }

    public async Task<User?> GetStudentByAdmissionNumber(string admissionNumber)
    {
        var student = await _dataContext.Users
            .Where(x => x.IdentificationNumber == admissionNumber)
            .Include(x => x.Courses)
            .Include(x => x.Department)
            .FirstOrDefaultAsync();
        if (student == null) return null;
        return student;
    }
}