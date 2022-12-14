using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Dtos;

public class LoginUserRequest
{
    [Required] public string IdentificationNumber { get; set; }
    [Required] public string Password { get; set; }
}