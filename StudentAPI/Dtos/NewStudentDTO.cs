namespace StudentAPI.Dtos;

public class NewStudentDTO
{
    public int AdmissionNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int DepartmentId { get; set; }
}