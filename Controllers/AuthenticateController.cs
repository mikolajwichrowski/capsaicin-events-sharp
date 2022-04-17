using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public AuthenticateController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public ActionResult<UserResponseType?> Post([FromBody] AuthenticationRequestType? AuthenticationRequestType)
    {
        if(AuthenticationRequestType == null) {
            return BadRequest();
        }

        User? user = null;
        using (var context = new AppContext())
        {
            try {
              user = context.Users.Where(row => row.username == AuthenticationRequestType.username && row.password == AuthenticationRequestType.password).First();
            } catch {}   
        }

        if(user == null) {
            return Unauthorized();
        }

        
        Response.Cookies.Append(
            "logged_in",
            "yes",
            new Microsoft.AspNetCore.Http.CookieOptions()
        );
        Response.Cookies.Append(
            "user_id",
            user.id.ToString(),
            new Microsoft.AspNetCore.Http.CookieOptions()
        );

        return new UserResponseType{ id = user.id, username = user.username };
    }
}
