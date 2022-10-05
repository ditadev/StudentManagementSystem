namespace Student.Services;

public interface IStudentService
{
    public Task<Model.Student> GetStudentByAdmissionNumber(string admissionNumber);
}