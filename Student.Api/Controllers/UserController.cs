using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Model;
using Student.Services;
using StudentAPI.Attributes;

namespace StudentAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Microsoft.AspNetCore.Authorization.Authorize]
public class UserController : AbstractController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<User>> GetCurrentUser()
    {
        var userIdNumber = GetContextUserIdentificationNumber();
        var students = await _userService.GetStudentByAdmissionNumber(userIdNumber);
        if (students == null) return NotFound(new {error ="Not Found"});

        return Ok(students);
    }

    [HttpDelete]
    [Attributes.Authorize(Role.HeadOfDepartment)]
    public async Task<ActionResult> DeleteUser(string admissionNumber)
    {
        var user = await _userService.GetStudentByAdmissionNumber(admissionNumber);
        if (user==null)
        {
            return NotFound(new {error ="Not Found"});
        }
        await _userService.DeleteUser(admissionNumber);
        return Ok(new {message ="Student Deleted"});
    }
}