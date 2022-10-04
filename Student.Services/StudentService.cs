using System.Net;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Student.Model;
using Student.Persistence;

namespace Student.Services;

public class StudentService : IStudentService
{
    private readonly DataContext _dataContext;
    

    public StudentService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<List<Model.Student?>> GetStudentsByDepartment(int departmentId)
    {
        var student = await _dataContext.Students
            .Where(x => x.DepartmentId == departmentId)
            .Include(x=>x.Courses)
            .ToListAsync();
        // if (student.Count==0)
        // {
        //     return null;
        // }
        return student;
    }

    public  async Task<List<Model.Student?>> GetStudentByAdmissionNumber(int admissionNumber)
    {
        var student = await _dataContext.Students.Where(x => x.AdmissionNumber == admissionNumber)
            .Include(x => x.Courses).ToListAsync();
        if (student==null)
        {
            return null;
        }
        return student;
    }

    public async Task<Model.Student?> AddStudentCourses(NewCourseDTO courseDto)
    {
        var student = await _dataContext.Students
            .Where(x => x.AdmissionNumber == courseDto.AdmissionNumber)
            .Include(x => x.Courses)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            return null;
        }

        var newCourse = await _dataContext.Courses.FindAsync(courseDto.CourseCode);

        if (newCourse == null)
        {
            return null;
        }

        student.Courses.Add(newCourse);
        await _dataContext.SaveChangesAsync();

        return student;
    }
    
    public async Task<Model.Student?> AddStudent(Model.NewStudentDTO studentDTO)
    {
        var department = await _dataContext.Departments.FindAsync(studentDTO.DepartmentId);
        if (department == null)
        {
            return null;
        }
        var newStudent = new Model.Student
            {
                AdmissionNumber = studentDTO.AdmissionNumber,
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Department = department,
            };
        
            _dataContext.Students.Add(newStudent);
            await _dataContext.SaveChangesAsync();

            return newStudent;
        }
    }


