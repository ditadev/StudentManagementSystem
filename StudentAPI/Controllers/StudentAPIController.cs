using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Services;

namespace StudentAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class StudentApiController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentApiController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("admissionNumber"),Authorize(Roles = "Student")]
    public async Task<ActionResult<Student.Model.Student>> GetStudentByAdmissionNumber(string admissionNumber)
    {
        var students = await _studentService.GetStudentByAdmissionNumber(admissionNumber);
        if (students == null) return BadRequest("No Student with such Admission number");

        return Ok(students);
    }
}