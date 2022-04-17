using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;



[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public RegisterController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

  
    [HttpPost]
    public ActionResult<UserReturnType?> Post([FromBody] AuthenticationBody? authenticationBody)
    {
        if(authenticationBody == null) {
            return BadRequest();
        }

        User user;
        using (var context = new AppContext())
        {
            user = new User(authenticationBody.username, authenticationBody.password);
            context.Users.Add(user);
            context.SaveChanges();
        }

        if(user == null){
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
