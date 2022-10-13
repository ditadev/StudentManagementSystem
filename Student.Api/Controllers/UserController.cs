using Microsoft.AspNetCore.Mvc;
using Student.Model;
using Student.Services;
using StudentAPI.Attributes;
using Role = Student.Model.Enums.Role;

namespace StudentAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Role.Default)]
public class UserController : AbstractController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Role.Student)]
    public async Task<ActionResult<User>> GetCurrentUser()
    {
        var userIdNumber = GetContextUserIdentificationNumber();
        var students = await _userService.GetStudentByAdmissionNumber(userIdNumber);
        if (students == null) return NotFound(new { error = "Not Found" });

        return Ok(students);
    }

    [HttpDelete]
    [Authorize(Role.HeadOfDepartment)]
    public async Task<ActionResult> DeleteUser(string admissionNumber)
    {
        var user = await _userService.GetStudentByAdmissionNumber(admissionNumber);
        if (user == null) return NotFound(new { error = "Not Found" });
        await _userService.DeleteUser(admissionNumber);
        return Ok(new { message = "Student Deleted" });
    }
}