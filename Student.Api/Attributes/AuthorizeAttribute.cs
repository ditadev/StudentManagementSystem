using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Student.Model.Enums;

namespace StudentAPI.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly List<Role> _roles;

    public AuthorizeAttribute(Role firstRole, params Role[] otherRoles)
    {
        _roles = new List<Role>();
        _roles.Add(firstRole);
        _roles.AddRange(otherRoles);
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous) return;

        var parsedRoles = context.HttpContext.User.Claims
            .Where(x => x.Type == "role")
            .Select(y => Enum.Parse<Role>(y.Value))
            .ToList();


        foreach (var role in _roles)
            if (!parsedRoles.Contains(role))
            {
                context.Result = new JsonResult(new { message = "Missing Privileges" })
                    { StatusCode = StatusCodes.Status403Forbidden };
                break;
            }
    }
}