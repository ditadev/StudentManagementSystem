using Microsoft.AspNetCore.Mvc;
using Student.Model;
using Student.Persistence;
using Student.Services;
using StudentAPI.Dtos;

namespace StudentAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RegistrationController : AbstractController
{
    private readonly IUserService _userService;

    public RegistrationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterUserRequest request)
    {
        var passwordHash = await _userService.CreatPasswordHash(request.Password);
        var department = await _userService.GetDepartmentById(request.DepartmentId);
        var courses = await _userService.GetCoursesByDepartment(request.DepartmentId);
        var studentByAdmissionNumber = await _userService.GetStudentByAdmissionNumber(request.IdentificationNumber);
        var userByEmail = await _userService.GetUserByEmail(request.EmailAddress);
        if (studentByAdmissionNumber == null)
        {
            if (userByEmail == null)
            {
                var res = new User
                {
                    IdentificationNumber = request.IdentificationNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    DepartmentId = request.DepartmentId,
                    Department = department,
                    PasswordHash = passwordHash,
                    Courses = courses,
                    EmailAddress = request.EmailAddress
                };
                await _userService.AddUser(res);
                return Ok(new { message = "Successful :)" });
            }

            return BadRequest(new { error = "User Already Exist :(" });
        }

        return BadRequest(new { error = "User Already Exist :(" });
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login(LoginUserRequest request)
    {
        var user = await _userService.GetStudentByAdmissionNumber(request.IdentificationNumber);
        if (user == null)
            return BadRequest(new { error = "Wrong username/password" });
        if (_userService.VerifyPasswordHash(request.Password, user.PasswordHash) == false)
            return BadRequest(new { error = "Wrong username/password" });

        return Ok(new JwtDto { AccessToken = await _userService.CreateJwt(user) });
    }
}