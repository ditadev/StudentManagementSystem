using Student.Model;

namespace Student.Services;

public interface IStudentService
{
    public Task<Model.Student> AddStudent(NewStudentDTO studentDTO);
    public Task<List<Model.Student>> GetStudentsByDepartment(int departmentId);
    public Task<List<Model.Student>> GetStudentByAdmissionNumber(int admissionNumber);
    public Task<Model.Student> AddStudentCourses(NewCourseDTO courseDto);

}