using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;
using System.Net;

namespace capsaicin_events_sharp.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class AuthenticateController : Controller
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
            throw new HttpError("Missing data", HttpStatusCode.BadRequest);
        }

        User? user = null;
        using (var context = new AppContext())
        {
            try {
              user = context.Users.Where(row => row.username == authenticationBody.username && row.password == authenticationBody.password).First();
            } catch {
                throw new HttpError("User does not exist", HttpStatusCode.Unauthorized);
            }   
        }

        if(user == null) {
            throw new HttpError("Unathorized", HttpStatusCode.Unauthorized);
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
