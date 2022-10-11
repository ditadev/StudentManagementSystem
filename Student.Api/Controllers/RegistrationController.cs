using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Model;
using Student.Persistence;
using Student.Services;
using StudentAPI.Dtos;

namespace StudentAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RegistrationController : AbstractController
{
    private readonly DataContext _dataContext;
    private readonly IUserService _userService;

    public RegistrationController(IUserService userService, DataContext dataContext)
    {
        _userService = userService;
        _dataContext = dataContext;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Register(RegisterUserRequest request)
    {
        var passwordHash = _userService.CreatPasswordHash(request.Password);
        var User = await _dataContext.Users.FirstOrDefaultAsync(x =>
            x.IdentificationNumber == request.IdentificationNumber);
        var checkUser = await _dataContext.Users.Where(x => x.IdentificationNumber == request.IdentificationNumber)
            .FirstOrDefaultAsync();
        if (User == null && checkUser != null)
        {
            var res = new User
            {
                IdentificationNumber = request.IdentificationNumber,
                PasswordHash = await passwordHash
            };

            _dataContext.Users.Add(res);
            await _dataContext.SaveChangesAsync();
            return Ok(res);
        }

        return BadRequest("Try again with a valid/unused Admission Number");
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login(LoginUserRequest request)
    {
        var User = await _dataContext.Users.FirstOrDefaultAsync(x =>
            x.IdentificationNumber == request.IdentificationNumber);

        if (User == null) return BadRequest("Wrong username/password");

        if (_userService.VerifyPasswordHash(request.Password, User.PasswordHash) == false)
            return BadRequest("Wrong username/password");

        var token = await _userService.CreateJwt(User);
        return Ok(token);
    }
}