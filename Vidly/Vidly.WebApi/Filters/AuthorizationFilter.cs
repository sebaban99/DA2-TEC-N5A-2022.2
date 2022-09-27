using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vidly.IBusinessLogic;

namespace Vidly.WebApi.Filters;

public class AuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly ISessionLogic _sessionLogic;

    public AuthorizationFilter(ISessionLogic sessionLogic)
    {
        _sessionLogic = sessionLogic;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"];

        
        if (String.IsNullOrEmpty(token) && _sessionLogic.ValidateToken())
        {
            // Corto la ejecucion de la request cuando asigno un result
            context.Result = new JsonResult(new { Message = "Please send your authorization token" })
                { StatusCode = 401 };
        }
    }
}