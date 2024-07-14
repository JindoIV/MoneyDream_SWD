namespace WebApi.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoneyDreamClassLibrary.DataAccess;
using MoneyDreamClassLibrary.IRepository;
using MoneyDreamClassLibrary.Repository;
using System.Linq;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{


    private readonly IList<string> _accessRole;
    private readonly IRoleRepositoty roleRepo;

    public AuthorizeAttribute(params string[] accessRole)
    {
        roleRepo = new RoleRepository();
        _accessRole = accessRole ?? new string[] {};
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {

        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var user = (Account)context.HttpContext.Items["User"];
 
      
        if (user != null)
        {
            var userCurrentRole = roleRepo.GetRole(user.RoleId);

            if ((_accessRole.Any() && !_accessRole.Contains(userCurrentRole.RoleName)))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }  else
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

    }
}