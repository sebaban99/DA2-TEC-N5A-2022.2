using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vidly.IBusinessLogic;

namespace Vidly.WebApi.Filters;

public class AuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly ISessionManager _sessionManager;

    public AuthorizationFilter(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"];

        
        if (String.IsNullOrEmpty(token) || !_sessionManager.ValidateToken())
        {
            // Corto la ejecucion de la request cuando asigno un result
            context.Result = new JsonResult(new { Message = "Please send your authorization token" })
                { StatusCode = 401 };
        }
    }
}