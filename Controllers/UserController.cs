using Microsoft.AspNetCore.Mvc;
using capsaicin_events_sharp.Entities;

namespace capsaicin_events_sharp.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<UserResponseType> List()
    {
        IEnumerable<UserResponseType> users;
        using (var context = new AppContext())
        {
            users = context.Users.ToList().ConvertAll<UserResponseType>(user => new UserResponseType{ id = user.id, username = user.username });
        }
        return users;
    }
}
