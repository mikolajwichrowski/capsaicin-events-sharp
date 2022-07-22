using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;
using System.Net;

namespace capsaicin_events_sharp.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class RegisterController : Controller
{
    private readonly ILogger<UserController> _logger;

    public RegisterController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

  
    [HttpPost]
    public UserResponseType Post([FromBody] AuthenticationRequestType? authenticationBody)
    {
        if(authenticationBody == null) {
            throw new HttpError("Missing data", HttpStatusCode.BadRequest);
        }

        User user;
        using (var context = new AppContext())
        {
            user = new User{username=authenticationBody.username, password=authenticationBody.password};
            context.Users.Add(user);
            context.SaveChanges();
        }

        if(user == null){
            throw new HttpError("Cannot create user", HttpStatusCode.InternalServerError);
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
