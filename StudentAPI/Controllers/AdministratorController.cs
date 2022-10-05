using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Model;
using Student.Persistence;
using Student.Services;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IRegistrationService _registrationService;

        public AdministratorController(IRegistrationService registrationService, DataContext dataContext)
        {
            _registrationService = registrationService;
            _dataContext = dataContext;
        }
        
        [HttpPost]
        public async Task<ActionResult<Admin>> RegisterAdmin(Admin admin)
        {
            var passwordHash = _registrationService.CreatPasswordHash(admin.Password);
            var Admin = await _dataContext.Admins.FirstOrDefaultAsync(x => x.AdminId == admin.AdminId);
            var administrator = await _dataContext.Administrators.Where(x => x.AdminId == admin.AdminId)
                .FirstOrDefaultAsync();
            if (Admin == null && administrator != null)
            {
                var res = new Admin
                {
                    AdminId = admin.AdminId,
                    PasswordHash = await passwordHash
                };

                _dataContext.Admins.Add(res);
                await _dataContext.SaveChangesAsync();
                return Ok(res);
            }

            return BadRequest("Try again with a valid/unused Administrator Number");
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> Login(Admin admin)
        {
            var Admin = await _dataContext.Admins.FirstOrDefaultAsync(x => x.AdminId == admin.AdminId);

            if (Admin == null) return BadRequest("Wrong username/password");

            if (_registrationService.VerifyPasswordHash(admin.Password, Admin.PasswordHash) == false)
                return BadRequest("Wrong username/password");

            var token = await _registrationService.CreateAdminToken(Admin);
            return Ok(token);
        }
    }
}
