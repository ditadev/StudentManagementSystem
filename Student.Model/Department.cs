using System.Text.Json.Serialization;

namespace Student.Model;

public class Department
{
    public string DepartmentId { get; set; }
    public string Name { get; set; }

    [JsonIgnore] public List<Student> Students { get; set; }

    [JsonIgnore] public List<Course> Courses { get; set; }
}