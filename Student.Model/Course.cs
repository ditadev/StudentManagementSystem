using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Student.Model;

public class Course
{
    [Key]
    public string CourseCode { get; set; }
    public string CourseTitle { get; set; }
    [Range(1,3)]
    public int CreditLoad { get; set; }
    [JsonIgnore]
    public List<Student> Students { get; set; }
}