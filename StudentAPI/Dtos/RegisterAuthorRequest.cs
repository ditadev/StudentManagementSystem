using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Dtos;

public class RegisterAuthorRequest
{
    [Required] [EmailAddress] public string EmailAddress { get; set; }

    public string Username { get; set; }

    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; }

    [Required] public string Description { get; set; }
}