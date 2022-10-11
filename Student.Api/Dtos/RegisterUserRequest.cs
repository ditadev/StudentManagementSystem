using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Dtos;

public class RegisterUserRequest
{
    [Required] [EmailAddress] public string EmailAddress { get; set; }

    public string IdentificationNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Required(ErrorMessage = "Phone number is required!")]
    [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Please enter valid phone number!")]
    public string PhoneNumber { get; set; }

    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; }
}