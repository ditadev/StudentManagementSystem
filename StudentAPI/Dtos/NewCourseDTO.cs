using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Dtos;

public class NewCourseDTO
{
    [Key]
    public string CourseCode { get; set; }
    public int AdmissionNumber { get; set; }
}