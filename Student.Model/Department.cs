using System.Text.Json.Serialization;

namespace Student.Model;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
}