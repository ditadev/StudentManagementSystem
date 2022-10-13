using System.Text.Json.Serialization;

namespace Student.Model;

public class User
{
    public long UserId { get; set; }
    public string IdentificationNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    [JsonIgnore] public string? PasswordHash { get; set; }
    [JsonIgnore] public string Password { get; set; }
    public Department? Department { get; set; }
    [JsonIgnore] public string DepartmentId { get; set; }
    [JsonIgnore] public List<Role?> Roles { get; set; }
    public List<Course> Courses { get; set; }
}