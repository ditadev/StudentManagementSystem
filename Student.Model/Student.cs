using System.Text.Json.Serialization;

namespace Student.Model;

public class Student
{
    public string AdmissionNumber { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public Department Department { get; set; }
    [JsonIgnore] public string DepartmentId { get; set; }
    public List<Course> Courses { get; set; }
}