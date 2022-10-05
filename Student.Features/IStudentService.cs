namespace Student.Services;
using Student.Model;
public interface IStudentService
{
    public Task<Student> GetStudentByAdmissionNumber(string admissionNumber);
}