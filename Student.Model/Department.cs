using System.Text.Json.Serialization;

namespace Student.Model;

public class Department
{
    public string DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    [JsonIgnore] public List<User> Users { get; set; }
    [JsonIgnore] public List<Course> Courses { get; set; }
}