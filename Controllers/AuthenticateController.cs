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
    public UserResponseType Post([FromBody] AuthenticationRequestType? authenticationBody)
    {
        if(authenticationBody == null) {
            throw new BadHttpRequestException("Missing data");
        }

        User? user = null;
        using (var context = new AppContext())
        {
            try {
              user = context.Users.Where(row => row.username == authenticationBody.username && row.password == authenticationBody.password).First();
            } catch {
                throw new Exception("Cannot find user");
            }   
        }

        if(user == null) {
            throw new UnauthorizedAccessException();
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
