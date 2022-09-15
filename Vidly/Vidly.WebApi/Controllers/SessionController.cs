using Microsoft.AspNetCore.Mvc;
using Vidly.BusinessLogic;
using Vidly.Exceptions;
using Vidly.IBusinessLogic;
using Vidly.Models.In;
using Vidly.Models.Out;

namespace Vidly.WebApi.Controllers;

[Route("api/sessions")]
[ApiController]
public class SessionController : ControllerBase
{
    // Create - Log in (/api/sessions)
    [HttpPost]
    public ActionResult<Guid> Login([FromBody] SessionModel sessionModel)
    {
        try
        {
            //TODO: Actually check credentials in sessionLogic and generate token for client (Postman)
            //TODO: Actual token implementation may vary, Guid is used here as default, other could be encode username:password

            var token = new { AccessToken = Guid.NewGuid() };
            return Created(string.Empty, token);
        }
        catch (ResourceNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    // Delete - Log out (/api/sessions)
    [HttpDelete]
    public ActionResult Logout([FromHeader] string authorization)
    {
        try
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];

            if (authHeader is null)
                return BadRequest("Missing credentials. Cannot Logout");
            
            //TODO: Actually search for session and delete if found
            
            return NoContent();
        }
        catch (ResourceNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}