using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Model;
using Student.Services;
using StudentAPI.Dtos;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentAPIController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<ActionResult<Student.Model.Student>> AddStudent(NewStudentDTO student)
        {
            var add= await _studentService.AddStudent(student);
            if (add==null)
            {
                return BadRequest("No Student with such Admission number");
            }

            return Ok(add);
        }

        [HttpPost]
        public async Task<ActionResult<Student.Model.Student>> AddStudentCourses(NewCourseDTO courseDto)
        {
            var add =  await _studentService.AddStudentCourses(courseDto);
            if (add==null)
            {
                return BadRequest("No Student with such Admission number");
            }

            return Ok(add);
        }

        [HttpGet("departmentId")]

        public async Task<ActionResult<List<Student.Model.Student>>> GetStudentsByDepartment(int departmentId)
        {
            var res=  await _studentService.GetStudentsByDepartment(departmentId);
            // if (res==null)
            // {
            //     return BadRequest("No Student with such Admission number");
            // }

            return Ok(res);
        }

        [HttpGet("admissionNumber")]
        public async Task<ActionResult<Student.Model.Student>> GetStudentByAdmissionNumber(int admissionNumber)
        {
            var res =await _studentService.GetStudentByAdmissionNumber(admissionNumber);
            if (res==null)
            {
                return BadRequest("No Student with such Admission number");
            }

            return Ok(res);
        }
    }
}
