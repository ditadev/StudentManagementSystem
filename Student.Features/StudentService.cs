using Microsoft.EntityFrameworkCore;
using Student.Persistence;
using Student.Model;
namespace Student.Services;

public class StudentService : IStudentService
{
    private readonly DataContext _dataContext;


    public StudentService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Model.Student?> GetStudentByAdmissionNumber(string admissionNumber)
    {
        var student = await _dataContext.Students
            .Where(x => x.AdmissionNumber == admissionNumber)
            .Include(x => x.Courses)
            .Include(x => x.Department)
            .FirstOrDefaultAsync();
        if (student == null) return null;
        return student;
    }
}