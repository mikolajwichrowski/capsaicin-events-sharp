using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;

public class AuthenticationBody {

    public string username { get; set; } = "";
    public string password { get; set; } = "";
}


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
    public ActionResult<UserReturnType?> Post([FromBody] AuthenticationBody? authenticationBody)
    {
        if(authenticationBody == null) {
            return BadRequest();
        }

        User? user = null;
        using (var context = new AppContext())
        {
            try {
              user = context.Users.Where(row => row.username == authenticationBody.username && row.password == authenticationBody.password).First();
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

        return new UserReturnType{ id = user.id, username = user.username };
    }
}
