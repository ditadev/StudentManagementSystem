using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Student.Model;

public class Course
{
    public string CourseCode { get; set; }
    public string CourseTitle { get; set; }
    [Range(1, 3)] public int CreditLoad { get; set; }
    [JsonIgnore] public List<User> Students { get; set; }
    [JsonIgnore] public string AdmissionNumber { get; set; }
    [JsonIgnore] public List<Department> Departments { get; set; }
}