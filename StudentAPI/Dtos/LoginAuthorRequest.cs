using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Dtos;

public class LoginAuthorRequest
{
    [Required] [EmailAddress] public string EmailAddress { get; set; }
    [Required] public string Password { get; set; }
}