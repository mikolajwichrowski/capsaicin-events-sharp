using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;

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
            throw new BadHttpRequestException("Missing data");
        }

        User user;
        using (var context = new AppContext())
        {
            user = new User{username=authenticationBody.username, password=authenticationBody.password};
            context.Users.Add(user);
            context.SaveChanges();
        }

        if(user == null){
            throw new Exception("Cannot create user");
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
