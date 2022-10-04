using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Student.Model;

public class Student
{
    [Key]
    public int AdmissionNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonIgnore]
    public Department Department { get; set; }
    public int DepartmentId { get; set; }
    public List<Course> Courses { get; set; }
}