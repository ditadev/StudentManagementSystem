using System.Text.Json.Serialization;
namespace Student.Model;

public class Administrator
{
    public string AdminId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Position { get; set; }
    public Department Department { get; set; }
    [JsonIgnore] public string DepartmentId { get; set; }
    
}