using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace StudentAPI.Controllers;

public abstract class AbstractController : ControllerBase
{
    protected long GetContextUserId()
    {
        return long.Parse(HttpContext.User.Claims.First(x => x.Type == "sub").Value);
    }

    protected string GetContextUserIdentificationNumber()
    {
        return HttpContext.User.Claims
            .First(x => x.Type == "userAdmissionNumber").Value;
    }
}