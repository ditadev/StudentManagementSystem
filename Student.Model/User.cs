using System.Text.Json.Serialization;

namespace Student.Model;

public class User
{
    public string AdmissionNumber { get; set; }
    [JsonIgnore] public string? PasswordHash { get; set; }
    public string Password { get; set; }
}