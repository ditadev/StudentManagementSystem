using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Model;
using Student.Persistence;
using Student.Services;

namespace StudentAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly DataContext _dataContext;
    private readonly IRegistrationService _registrationService;

    public RegistrationController(IRegistrationService registrationService, DataContext dataContext)
    {
        _registrationService = registrationService;
        _dataContext = dataContext;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Register(User user)
    {
        var passwordHash = _registrationService.CreatPasswordHash(user.Password);
        var User = await _dataContext.Users.FirstOrDefaultAsync(x => x.AdmissionNumber == user.AdmissionNumber);
        var student = await _dataContext.Students.Where(x => x.AdmissionNumber == user.AdmissionNumber)
            .FirstOrDefaultAsync();
        if (User == null && student != null)
        {
            var res = new User
            {
                AdmissionNumber = user.AdmissionNumber,
                PasswordHash = await passwordHash
            };

            _dataContext.Users.Add(res);
            await _dataContext.SaveChangesAsync();
            return Ok(res);
        }

        return BadRequest("Try again with a valid/unused Admission Number");
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login(User user)
    {
        var User = await _dataContext.Users.FirstOrDefaultAsync(x => x.AdmissionNumber == user.AdmissionNumber);

        if (User == null) return BadRequest("Wrong username/password");

        if (_registrationService.VerifyPasswordHash(user.Password, User.PasswordHash) == false)
            return BadRequest("Wrong username/password");

        var token = await _registrationService.CreateToken(User);
        return Ok(token);
    }
}