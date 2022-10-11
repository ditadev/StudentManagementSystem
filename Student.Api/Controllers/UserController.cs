using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Model;
using Student.Services;

namespace StudentAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class UserController : AbstractController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("admissionNumber")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<User>> GetStudentByAdmissionNumber(string admissionNumber)
    {
        var students = await _userService.GetStudentByAdmissionNumber(admissionNumber);
        if (students == null) return BadRequest("No Student with such Admission number");

        return Ok(students);
    }
}