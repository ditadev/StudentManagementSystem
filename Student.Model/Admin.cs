using System.Text.Json.Serialization;

namespace Student.Model;

public class Admin
{
    public string AdminId { get; set; }
    [JsonIgnore] public string? PasswordHash { get; set; }
    public string Password { get; set; }
}